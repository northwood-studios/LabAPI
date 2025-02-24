using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.SendingAdminChat"/> event.
/// </summary>
public class SendingAdminChatEventArgs : EventArgs, ICommandSenderEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SendingAdminChatEventArgs"/> class.
    /// </summary>
    /// <param name="sender">The sender of the admin chat message.</param>
    /// <param name="message">The message to be sent.</param>
    public SendingAdminChatEventArgs(CommandSender sender, string message)
    {
        IsAllowed = true;
        Sender = sender;
        Message = message;
    }

    /// <summary>
    /// Gets or sets the message to be sent.
    /// </summary>
    public string Message { get; set; }

    /// <inheritdoc />
    public CommandSender Sender { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}