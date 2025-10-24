using CommandSystem;
using LabApi.Features.Console;
using LabApi.Loader.Features.Misc;
using LabApi.Loader.Features.Plugins;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace LabApi.Loader;

/// <summary>
/// LabAPIs command loader.
/// Responsible for loading all the different Commands in plugin assemblies.
/// </summary>
public static class CommandLoader
{
    private const string LoggerPrefix = "[COMMAND_LOADER]";

    /// <summary>
    /// The dictionary of command handlers.
    /// </summary>
    public static Dictionary<Type, List<CommandHandler>> CommandHandlers { get; } = new()
    {
        // The server console command handler.
        [typeof(GameConsoleCommandHandler)] = [GameCore.Console.ConsoleCommandHandler],

        // The remote admin command handler.
        [typeof(RemoteAdminCommandHandler)] = [CommandProcessor.RemoteAdminCommandHandler],

        // The client console command handler.
        [typeof(ClientCommandHandler)] = [QueryProcessor.DotCommandHandler],
    };

    /// <summary>
    /// The dictionary of registered commands by plugins.
    /// </summary>
    public static Dictionary<Plugin, IEnumerable<ICommand>> RegisteredCommands { get; } = [];

    /// <summary>
    /// The dictionary of registered LabAPI commands.
    /// </summary>
    public static IEnumerable<ICommand>? LabApiCommands { get; private set; }

    /// <summary>
    /// Registers all commands in the given <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to register the commands from.</param>
    public static void RegisterCommands(this Plugin plugin)
    {
        // We first check if the assembly of the plugin is in the dictionary of loaded assemblies.
        if (!plugin.TryGetLoadedAssembly(out Assembly assembly))
        {
            // If the assembly of the plugin could not be retrieved, we use reflection to get the assembly.
            assembly = plugin.GetType().Assembly;
        }

        // We finally register all commands in the assembly of the plugin.
        // We convert it to an array since IEnumerable are lazy and need to be iterated through to be executed.
        IEnumerable<ICommand> registeredCommands = RegisterCommands(assembly, plugin.Name).ToArray();

        // We add the registered commands to the dictionary of registered commands.
        RegisteredCommands.Add(plugin, registeredCommands);
    }

    /// <summary>
    /// Registers all commands in the given <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to register the commands from.</param>
    /// <param name="logName">The name of the plugin to log to use when logging errors.</param>
    /// <returns>An enumeration of all registered commands.</returns>
    public static IEnumerable<ICommand> RegisterCommands(Assembly assembly, string logName = "")
    {
        // We use reflection to get all types in the assembly.
        Type[] types = assembly.GetTypes();

        // We iterate through all types in the assembly.
        foreach (Type type in types)
        {
            // We register all commands in the type.
            foreach (ICommand command in RegisterCommands(type, logName))
            {
                // We add the command to the list of registered commands.
                yield return command;
            }
        }
    }

    /// <summary>
    /// Registers all commands in the given <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> to register the commands from.</param>
    /// <param name="logName">The name of the plugin to log to use when logging errors.</param>
    /// <returns>Returns a list of registered commands.</returns>
    public static IEnumerable<ICommand> RegisterCommands(Type type, string logName = "")
    {
        // We check if the type implements the ICommand interface.
        if (!typeof(ICommand).IsAssignableFrom(type))
        {
            yield break;
        }

        // We iterate through all custom attributes of the type.
        foreach (CustomAttributeData attributeData in type.GetCustomAttributesData())
        {
            // If the attribute type is not a CommandHandlerAttribute, we continue.
            if (attributeData.AttributeType != typeof(CommandHandlerAttribute))
            {
                continue;
            }

            // We retrieve the command handler type from the attribute data.
            Type commandHandlerType = (Type)attributeData.ConstructorArguments[0].Value;

            // We check if the parent command has already been registered.
            if (!CommandHandlers.ContainsKey(commandHandlerType) && typeof(ParentCommand).IsAssignableFrom(commandHandlerType))
            {
                // If the parent command has not been registered, we register the parent command.
                foreach (ICommand parent in RegisterCommands(commandHandlerType))
                {
                    // And we add the parent command to the list of registered commands.
                    yield return parent;
                }
            }

            // And we finally register the command.
            if (!TryRegisterCommand(type, commandHandlerType, out ICommand? command, logName))
            {
                continue;
            }

            // We add the command to the list of registered commands.
            yield return command;
        }
    }

    /// <summary>
    /// We register a command with the given <see cref="Type"/> and <see cref="Type"/>.
    /// </summary>
    /// <param name="commandType">The <see cref="Type"/> of the command to register.</param>
    /// <param name="commandHandlerType">The <see cref="Type"/> of the command handler to register the command to.</param>
    /// <param name="command">The registered command if the command was successfully registered.</param>
    /// <param name="logName">The name of the plugin to log to use when logging errors.</param>
    /// <returns>Whether the command registered successfully.</returns>
    public static bool TryRegisterCommand(Type commandType, Type commandHandlerType, [NotNullWhen(true)] out ICommand? command, string logName)
    {
        command = null;

        // We try to get the command handler from the dictionary of command handlers.
        if (!CommandHandlers.TryGetValue(commandHandlerType, out List<CommandHandler> commandHandlers))
        {
            Logger.Error($"{LoggerPrefix} Unable to register command '{commandType.Name}' from '{logName}'. CommandHandler '{commandHandlerType}' not found.");
            return false;
        }

        try
        {
            // We create an instance of the command type.
            if (Activator.CreateInstance(commandType) is not ICommand cmd)
            {
                Logger.Error($"{LoggerPrefix} Unable to register command '{commandType.Name}' from '{logName}'. Couldn't create an instance of the command.");
                return false;
            }

            // We set the command to the created command.
            command = cmd;
        }
        catch (Exception e)
        {
            Logger.Error($"{LoggerPrefix} Unable to register command '{commandType.Name}' from '{logName}'. Couldn't create an instance of the command.");
            Logger.Error(e);
            throw;
        }

        // We iterate through all command handlers (including subcommand that has multiple handler).
        foreach (CommandHandler commandHandler in commandHandlers)
        {
            // We finally try to register the command in the command handler.
            if (!TryRegisterCommand(command, commandHandler, logName))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Tries to register a command with the given <see cref="ICommand"/> and <see cref="CommandHandler"/>.
    /// </summary>
    /// <param name="command">The <see cref="ICommand"/> to register.</param>
    /// <param name="commandHandler">The <see cref="CommandHandler"/> to register the command to.</param>
    /// <param name="logName">The name of the plugin to log to use when logging errors.</param>
    /// <returns>Whether the command was successfully registered.</returns>
    public static bool TryRegisterCommand(ICommand command, CommandHandler commandHandler, string logName)
    {
        try
        {
            // We check if the command already registered (might happen) and just return.
            if (commandHandler.AllCommands.Any(x => string.Equals(x.Command, command.Command, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            // We try to register the command.
            commandHandler.RegisterCommand(command);

            // We check if the command type is a parent command.
            if (command is not ParentCommand parentCommand)
            {
                return true;
            }

            // If the command type is a parent command, we register the command type as a command handler type.
            // This allows us to register subcommands to the parent command by just using the CommandHandlerAttribute.
            // [CommandHandler(typeof(MyParentCommand))]
            Type commandType = command.GetType();

            if (!CommandHandlers.ContainsKey(commandType))
            {
                // We add the command type to the dictionary of command handlers.
                CommandHandlers[commandType] = [];
            }

            if (!CommandHandlers[commandType].Contains(parentCommand))
            {
                CommandHandlers[commandType].Add(parentCommand);
            }

            // We can finally return true.
            return true;
        }
        catch (Exception e)
        {
            // If the command name is not null then the error was thrown because of a duplicate command.
            if (!string.IsNullOrWhiteSpace(command.Command))
            {
                Logger.Error($"{LoggerPrefix} Unable to register command '{command.Command}' from '{logName}'. A command with the same name or aliases has already been registered!");
                return false;
            }

            // If the command name is null then we log the exception.
            Logger.Error($"{LoggerPrefix} Unable to register command '{command.Command}' from '{logName}'.");
            Logger.Error(e);
            return false;
        }
    }

    /// <summary>
    /// Unregisters all commands in the given <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to unregister the commands from.</param>
    public static void UnregisterCommands(this Plugin plugin)
    {
        if (!RegisteredCommands.TryGetValue(plugin, out IEnumerable<ICommand> commands))
        {
            return;
        }

        // We iterate through all commands in the plugin.
        foreach (ICommand command in commands)
        {
            // And we unregister the command.
            UnregisterCommand(command);
        }

        // Once we have unregistered all commands, we remove the plugin from the dictionary of registered commands.
        RegisteredCommands.Remove(plugin);
    }

    /// <summary>
    /// Unregisters a command from all registered <see cref="CommandHandler"/>s.
    /// </summary>
    /// <param name="command">The command to unregister.</param>
    public static void UnregisterCommand(ICommand command)
    {
        // We iterate through all command handlers.
        foreach (List<CommandHandler> commandHandlers in CommandHandlers.Values)
        {
            foreach (CommandHandler commandHandler in commandHandlers)
            {
                // If the command handler does not contain the command, we continue.
                if (!commandHandler.AllCommands.Contains(command))
                {
                    continue;
                }

                // We manually unregister the command from the command handler.
                commandHandler.UnregisterCommand(command);
            }
        }

        // We check if the command is a parent command.
        if (command is not ParentCommand parentCommand)
        {
            return;
        }

        // If the command is a parent command, we remove the command type from the dictionary of command handlers.
        Type commandType = parentCommand.GetType();
        CommandHandlers.Remove(commandType);
    }

    /// <summary>
    /// Registers all commands in the LabAPI solution.
    /// </summary>
    internal static void RegisterCommands()
    {
        // We register all commands in the LabAPI assembly.
        // We convert it to an array since IEnumerable are lazy and need to be iterated through to be executed.
        LabApiCommands = RegisterCommands(Assembly.GetExecutingAssembly(), "LabApi").ToArray();
    }
}