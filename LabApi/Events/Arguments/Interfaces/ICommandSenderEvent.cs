namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a <see cref="CommandSender"/>.
/// </summary>
public interface ICommandSenderEvent
{
    /// <summary>
    /// The <see cref="CommandSender"/> that is involved in the event.
    /// </summary>
    public CommandSender? Sender { get; }
}