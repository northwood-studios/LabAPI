namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a <see cref="UserGroup"/>.
/// </summary>
public interface IGroupEvent
{
    /// <summary>
    /// The <see cref="UserGroup"/> involved in the event.
    /// </summary>
    public UserGroup? Group { get; }
}