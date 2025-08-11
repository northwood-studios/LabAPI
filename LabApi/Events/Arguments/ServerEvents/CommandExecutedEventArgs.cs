using CommandSystem;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Enums;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CommandExecuted"/> event.
/// </summary>
public class CommandExecutedEventArgs : EventArgs, ICommandSenderEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandExecutedEventArgs"/> class.
    /// </summary>
    /// <param name="sender">The sender of the command.</param>
    /// <param name="commandType">The type of the command.</param>
    /// <param name="command">The command.</param>
    /// <param name="arguments">The arguments of the command.</param>
    /// <param name="successful">Whether the command was executed successfully.</param>
    /// <param name="response">The response of the command.</param>
    public CommandExecutedEventArgs(CommandSender? sender, CommandType commandType, ICommand command, ArraySegment<string> arguments, bool successful, string response)
    {
        Sender = sender;
        CommandType = commandType;
        Command = command;
        Arguments = arguments;
        ExecutedSuccessfully = successful;
        Response = response;
    }

    /// <summary>
    /// The sender of the command.
    /// </summary>
    public CommandSender? Sender { get; }

    /// <summary>
    /// The type of the command that was executed.
    /// </summary>
    public CommandType CommandType { get; }

    /// <summary>
    /// The command that was executed.
    /// </summary>
    public ICommand Command { get; }

    /// <summary>
    /// The arguments of the command.
    /// </summary>
    public ArraySegment<string> Arguments { get; }

    /// <summary>
    /// Whether the command was executed successfully.
    /// </summary>
    public bool ExecutedSuccessfully { get; set; }

    /// <summary>
    /// The response of the command.
    /// </summary>
    public string Response { get; set; }

    /// <summary>
    /// Gets the name or alias of the command that was executed.
    /// </summary>
    public string CommandName => Arguments.Array?[0] ?? string.Empty;
}