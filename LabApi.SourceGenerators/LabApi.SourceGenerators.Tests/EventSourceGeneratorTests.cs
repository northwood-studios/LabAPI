using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace LabApi.SourceGenerators.Tests;

public class EventSourceGeneratorTests
{

    private const string SourceClassText = $$"""
                                           using System;
                                           using {{Core.EventHandlerNamespace}};

                                           namespace {{Core.EventHandlerNamespace}}
                                           {
                                               public class Scp079Events
                                               {
                                                   public static event LabEventHandler<Scp079GainingExperienceEventArgs>? GainingExperience;
                                                   public event LabEventHandler<Scp079GainedExperienceEventArgs>?  GainedExperience;
                                               }
                                               
                                               public static partial class ServerEvents
                                               {
                                                   public static event LabEventHandler? WaitingForPlayers;
                                                   public static event LabEventHandler<RoundEndingEventArgs>? RoundEnding;
                                               }
                                           }

                                           namespace {{Core.EventArgumentsNamespace}}.Scp079Events
                                           {
                                               public class Scp079GainingExperienceEventArgs : EventArgs { }
                                               public class Scp079GainedExperienceEventArgs : EventArgs { }
                                           }

                                           namespace {{Core.EventArgumentsNamespace}}.ServerEvents
                                           {
                                               public class RoundEndingEventArgs : EventArgs { }
                                           }
                                           """;

    private static readonly Dictionary<string, string> ExpectedGeneratedFiles = new ()
    {
        {
            "Scp079Events.EventInvokers.g.cs",
            $$"""
            using {{Core.EventArgumentsNamespace}}.Scp079Events;

            namespace {{Core.EventHandlerNamespace}};

            /// <inheritdoc />
            public static partial class Scp079Events
            {
                /// <summary>
                /// Invokes the <see cref="GainingExperience"/> event.
                /// </summary>
                /// <param name="{{Core.EventArgsName}}">The <see cref="Scp079GainingExperienceEventArgs"/> of the event.</param>
                public static void OnGainingExperience(Scp079GainingExperienceEventArgs {{Core.EventArgsName}}) => GainingExperience.InvokeEvent({{Core.EventArgsName}});
            
                /// <summary>
                /// Invokes the <see cref="GainedExperience"/> event.
                /// </summary>
                /// <param name="{{Core.EventArgsName}}">The <see cref="Scp079GainedExperienceEventArgs"/> of the event.</param>
                public static void OnGainedExperience(Scp079GainedExperienceEventArgs {{Core.EventArgsName}}) => GainedExperience.InvokeEvent({{Core.EventArgsName}});
            }
            """
        },
        {
            "ServerEvents.EventInvokers.g.cs",
            $$"""
            using {{Core.EventArgumentsNamespace}}.ServerEvents;

            namespace {{Core.EventHandlerNamespace}};

            /// <inheritdoc />
            public static partial class ServerEvents
            {
                /// <summary>
                /// Invokes the <see cref="WaitingForPlayers"/> event.
                /// </summary>
                public static void OnWaitingForPlayers() => WaitingForPlayers.InvokeEvent();
            
                /// <summary>
                /// Invokes the <see cref="RoundEnding"/> event.
                /// </summary>
                /// <param name="{{Core.EventArgsName}}">The <see cref="RoundEndingEventArgs"/> of the event.</param>
                public static void OnRoundEnding(RoundEndingEventArgs {{Core.EventArgsName}}) => RoundEnding.InvokeEvent({{Core.EventArgsName}});
            }
            """
        },
        {
            "CustomHandlersManager.g.cs",
            $$"""
            using System;
            using {{Core.EventHandlerNamespace}};

            namespace {{Core.CustomHandlersNamespace}};

            public static partial class CustomHandlersManager
            {
                static partial void RegisterEvents<T>(T handler, Type handlerType) where T : CustomEventsHandler
                {
                    CheckEvent(handler, handlerType, nameof(CustomEventsHandler.OnScp079GainingExperience), typeof(Scp079Events), nameof(Scp079Events.GainingExperience));
                    CheckEvent(handler, handlerType, nameof(CustomEventsHandler.OnScp079GainedExperience), typeof(Scp079Events), nameof(Scp079Events.GainedExperience));
                    CheckEvent(handler, handlerType, nameof(CustomEventsHandler.OnServerWaitingForPlayers), typeof(ServerEvents), nameof(ServerEvents.WaitingForPlayers));
                    CheckEvent(handler, handlerType, nameof(CustomEventsHandler.OnServerRoundEnding), typeof(ServerEvents), nameof(ServerEvents.RoundEnding));
                }
            }
            """
        }
    };

    [Fact]
    public void CustomEventHandlerSourceGenerator_GeneratesExpectedCode()
    {
        EventSourceGenerator generator = new ();

        CSharpGeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        Compilation compilation = CSharpCompilation.Create(nameof(EventSourceGeneratorTests),
            new[] { CSharpSyntaxTree.ParseText(SourceClassText) },
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            });

        // Run generators and retrieve all results.
        GeneratorDriverRunResult runResult = driver.RunGenerators(compilation).GetRunResult();

        // Check for each expected generated file
        foreach (KeyValuePair<string, string> expectedGeneratedFile in ExpectedGeneratedFiles)
        {
            SyntaxTree generatedFileSyntax = runResult.GeneratedTrees.Single(t => t.FilePath.EndsWith(expectedGeneratedFile.Key));
            Assert.Equal(expectedGeneratedFile.Value, generatedFileSyntax.GetText().ToString(), ignoreLineEndingDifferences: true);
        }
    }
}