using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace LabApi.SourceGenerators;

[Generator]
public class EventSourceGenerator : IIncrementalGenerator
{
    private static readonly DiagnosticDescriptor NonNullableEventHandlerRule = new (
        id: "EVT001",
        title: "Event handler should be nullable",
        messageFormat: "The event handler '{0}' should be declared as nullable",
        category: "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<(EventFieldDeclarationSyntax, string eventClass, string? eventArgsType, string eventName, Diagnostic? diagnostic)> eventFieldsProvider =
            context.SyntaxProvider.CreateSyntaxProvider(predicate: (s, _) => IsEventField(s), transform: (ctx, _) => GetEventField(ctx)).Where(t => t != null)
                .Select((t, _) => t!.Value);

        context.RegisterSourceOutput(context.CompilationProvider.Combine(eventFieldsProvider.Collect()), (ctx, t) => GenerateCode(ctx, t.Right));
    }

    private static bool IsEventField(SyntaxNode syntaxNode)
    {
        return syntaxNode is EventFieldDeclarationSyntax;
    }

    private static (EventFieldDeclarationSyntax, string eventClass, string? eventArgsType, string eventName, Diagnostic? diagnostic)? GetEventField(GeneratorSyntaxContext context)
    {
        if (context.Node is not EventFieldDeclarationSyntax fieldDeclaration)
            return null;

        VariableDeclaratorSyntax? variable = fieldDeclaration.Declaration.Variables.FirstOrDefault();
        if (variable == null)
            return null;

        if (fieldDeclaration.Parent is not ClassDeclarationSyntax classDeclaration)
            return null;

        if (classDeclaration.Parent is not BaseNamespaceDeclarationSyntax namespaceDeclaration)
            return null;

        if (namespaceDeclaration.Name.ToString() != Core.EventHandlerNamespace)
            return null;

        string className = classDeclaration.Identifier.Text;

        string? eventArgsType = null;
        Diagnostic? diagnostic = null;

        if (fieldDeclaration.Declaration.Type is NullableTypeSyntax {ElementType: GenericNameSyntax genericName})
            eventArgsType = genericName.TypeArgumentList.Arguments.FirstOrDefault()?.ToString();
        else if (fieldDeclaration.Declaration.Type is GenericNameSyntax)
        {
            // Create diagnostic if the event handler is not nullable
            diagnostic = Diagnostic.Create(NonNullableEventHandlerRule, fieldDeclaration.GetLocation(), variable.Identifier.Text);
            return (fieldDeclaration, className, eventArgsType, variable.Identifier.Text, diagnostic);
        }

        string eventName = variable.Identifier.Text;

        return (fieldDeclaration, className, eventArgsType, eventName, diagnostic);
    }

    private static void GenerateCode(SourceProductionContext context,
        ImmutableArray<(EventFieldDeclarationSyntax, string eventClass, string? eventArgsType, string eventName, Diagnostic? diagnostic)> eventFields)
    {
        foreach ((EventFieldDeclarationSyntax, string eventClass, string? eventArgsType, string eventName, Diagnostic? diagnostic) eventField in eventFields)
        {
            if (eventField.diagnostic != null)
                context.ReportDiagnostic(eventField.diagnostic);
        }
        List<IGrouping<string, (EventFieldDeclarationSyntax, string eventClass, string? eventArgsType, string eventName, Diagnostic?)>> eventGroups =
            eventFields.Where(e => e.diagnostic == null).GroupBy(e => e.eventClass).ToList();

        StringBuilder registerEventsBuilder = new ();
        registerEventsBuilder.AppendLine("using System;");
        registerEventsBuilder.AppendLine($"using {Core.EventHandlerNamespace};");
        registerEventsBuilder.AppendLine();
        registerEventsBuilder.AppendLine($"namespace {Core.CustomHandlersNamespace};");
        registerEventsBuilder.AppendLine();
        registerEventsBuilder.AppendLine("public static partial class CustomHandlersManager");
        registerEventsBuilder.AppendLine("{");
        registerEventsBuilder.AppendLine("    static partial void RegisterEvents<T>(T handler, Type handlerType) where T : CustomEventsHandler");
        registerEventsBuilder.AppendLine("    {");

        foreach (IGrouping<string, (EventFieldDeclarationSyntax, string eventClass, string? eventArgsType, string eventName, Diagnostic?)> group in eventGroups)
        {
            string eventType = group.Key;
            string trimmedEventType = eventType.Replace("Events", "");

            StringBuilder invokeEventsBuilder = new ();
            invokeEventsBuilder.AppendLine("using System;");
            invokeEventsBuilder.AppendLine($"using {Core.EventArgumentsNamespace}.{eventType};");
            invokeEventsBuilder.AppendLine();
            invokeEventsBuilder.AppendLine($"namespace {Core.EventHandlerNamespace};");
            invokeEventsBuilder.AppendLine();
            invokeEventsBuilder.AppendLine("/// <inheritdoc />");
            invokeEventsBuilder.AppendLine($"public static partial class {eventType}");
            invokeEventsBuilder.AppendLine("{");

            StringBuilder customEventHandlerBuilder = new ();
            customEventHandlerBuilder.AppendLine("using System;");
            customEventHandlerBuilder.AppendLine($"using {Core.EventArgumentsNamespace}.{eventType};");
            customEventHandlerBuilder.AppendLine($"using {Core.EventHandlerNamespace};");
            customEventHandlerBuilder.AppendLine();
            customEventHandlerBuilder.AppendLine($"namespace {Core.CustomHandlersNamespace};");
            customEventHandlerBuilder.AppendLine();
            customEventHandlerBuilder.AppendLine("/// <inheritdoc />");
            customEventHandlerBuilder.AppendLine("public abstract partial class CustomEventsHandler");
            customEventHandlerBuilder.AppendLine("{");

            bool isFirstMethod = true;
            foreach ((EventFieldDeclarationSyntax syntax, _, string? eventArgsType, string eventName, _) in group)
            {
                bool isObsolete = TryGetObsoleteAttribute(syntax, out string? obsoleteSyntax);
                
                if (isObsolete)
                {
                    registerEventsBuilder.AppendLine(Core.DisableObsoleteWarning);
                }

                registerEventsBuilder.AppendLine(
                    $"        CheckEvent(handler, handlerType, nameof(CustomEventsHandler.On{trimmedEventType}{eventName}), typeof({eventType}), nameof({eventType}.{eventName}));");

                if (isObsolete)
                {
                    registerEventsBuilder.AppendLine(Core.RestoreObsoleteWarning);
                }

                if (!isFirstMethod)
                {
                    invokeEventsBuilder.AppendLine();
                    customEventHandlerBuilder.AppendLine();
                }

                invokeEventsBuilder.AppendLine($"    /// <summary>");
                invokeEventsBuilder.AppendLine($"    /// Invokes the <see cref=\"{eventName}\"/> event.");
                invokeEventsBuilder.AppendLine($"    /// </summary>");

                customEventHandlerBuilder.AppendLine($"    /// <inheritdoc cref=\"{eventType}.{eventName}\"/>");

                if (eventArgsType != null)
                {
                    invokeEventsBuilder.AppendLine($"    /// <param name=\"{Core.EventArgsName}\">The <see cref=\"{eventArgsType}\"/> of the event.</param>");

                    if (isObsolete)
                    {
                        invokeEventsBuilder.Append("    ").AppendLine(obsoleteSyntax);

                        customEventHandlerBuilder.Append("    ").AppendLine(obsoleteSyntax);
                    }
                    
                    invokeEventsBuilder.AppendLine($"    public static void On{eventName}({eventArgsType} {Core.EventArgsName}) => {eventName}.InvokeEvent({Core.EventArgsName});");

                    customEventHandlerBuilder.AppendLine($"    public virtual void On{trimmedEventType}{eventName}({eventArgsType} {Core.EventArgsName}) {{ }}");
                }
                else
                {
                    if (isObsolete)
                    {
                        invokeEventsBuilder.Append("    ").AppendLine(obsoleteSyntax);

                        customEventHandlerBuilder.Append("    ").AppendLine(obsoleteSyntax);
                    }
                    
                    invokeEventsBuilder.AppendLine($"    public static void On{eventName}() => {eventName}.InvokeEvent();");

                    customEventHandlerBuilder.AppendLine($"    public virtual void On{trimmedEventType}{eventName}() {{ }}");
                }

                isFirstMethod = false;
            }

            invokeEventsBuilder.Append("}");
            customEventHandlerBuilder.Append("}");

            context.AddSource($"{eventType}.EventInvokers.g.cs", SourceText.From(invokeEventsBuilder.ToString(), Encoding.UTF8));
            context.AddSource($"CustomEventHandlers.{eventType}.g.cs", SourceText.From(customEventHandlerBuilder.ToString(), Encoding.UTF8));
        }

        registerEventsBuilder.AppendLine("    }");
        registerEventsBuilder.Append("}");

        context.AddSource("CustomHandlersManager.g.cs", SourceText.From(registerEventsBuilder.ToString(), Encoding.UTF8));
    }

    private static bool TryGetObsoleteAttribute(MemberDeclarationSyntax syntax, out string? obsoleteSyntax)
    {
        foreach (var list in syntax.AttributeLists)
        {
            foreach (var attribute in list.Attributes)
            {
                if (attribute.Name.ToString() is not ("Obsolete" or "System.Obsolete" or "ObsoleteAttribute" or "System.ObsoleteAttribute"))
                    continue;

                ExtractObsoleteAttributeData(attribute, out string? message, out string? error);
                obsoleteSyntax = (message, error) switch
                {
                    (not null, not null) => $"[Obsolete({message}, {error})]",
                    (not null, null) => $"[Obsolete({message})]",
                    _ => "[Obsolete]"
                };

                return true;
            }
        }

        obsoleteSyntax = null;
        return false;
    }

    private static void ExtractObsoleteAttributeData(AttributeSyntax attribute, out string? message, out string? error)
    {
        switch (attribute.ArgumentList?.Arguments.Count)
        {
            case 1:
                message = attribute.ArgumentList.Arguments[0].ToFullString();
                error = null;
                break;
            case 2:
                message = attribute.ArgumentList.Arguments[0].ToFullString();
                error = attribute.ArgumentList.Arguments[1].ToFullString();
                break;
            default:
                message = null;
                error = null;
                break;
        }
    }

}
