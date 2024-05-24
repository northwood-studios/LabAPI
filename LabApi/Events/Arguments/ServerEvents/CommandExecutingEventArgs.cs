using System;
using CommandSystem;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Enums;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CommandExecuting"/> event.
/// </summary>
public class CommandExecutingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandExecutingEventArgs"/> class.
    /// </summary>
    /// <param name="sender">The sender of the command.</param>
    /// <param name="commandType">The type of the command.</param>
    /// <param name="wasFound">Whether the command was found.</param>
    /// <param name="command">The command.</param>
    /// <param name="arguments">The arguments of the command.</param>
    public CommandExecutingEventArgs(CommandSender sender, CommandType commandType, bool wasFound, ICommand command, ArraySegment<string> arguments)
    {
        IsAllowed = true;
        Sender = sender;
        CommandType = commandType;
        CommandFound = wasFound;
        Command = command;
        Arguments = arguments;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
    
    /// <summary>
    /// The sender of the command.
    /// </summary>
    public CommandSender Sender { get; set; }
    
    /// <summary>
    /// The type of the command that is being executed.
    /// </summary>
    public CommandType CommandType { get; }
    
    /// <summary>
    /// Whether the command was found.
    /// </summary>
    public bool CommandFound { get; }
    
    /// <summary>
    /// The command that is being executed.
    /// <para>Can be null if the command was not found.</para>
    /// </summary>
    public ICommand Command { get; set; }
    
    /// <summary>
    /// The arguments of the command.
    /// </summary>
    public ArraySegment<string> Arguments { get; set; }

    /// <summary>
    /// Gets the name or alias of the command that was executed.
    /// </summary>
    public string CommandName => Arguments.Array?[0] ?? string.Empty;
}