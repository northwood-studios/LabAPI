using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.SentAdminChat"/> event.
/// </summary>
public class SentAdminChatEventArgs : EventArgs, ICommandSenderEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SentAdminChatEventArgs"/> class.
    /// </summary>
    /// <param name="sender">The sender of the admin chat message.</param>
    /// <param name="message">The message that was sent.</param>
    public SentAdminChatEventArgs(CommandSender sender, string message)
    {
        Sender = sender;
        Message = message;
    }

    /// <summary>
    /// Gets the message that was sent.
    /// </summary>
    public string Message { get; }

    /// <inheritdoc />
    public CommandSender Sender { get; }
}