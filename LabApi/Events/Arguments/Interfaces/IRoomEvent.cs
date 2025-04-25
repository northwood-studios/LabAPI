using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a room.
/// </summary>
public interface IRoomEvent
{
    /// <summary>
    /// The room that is involved in the event.
    /// </summary>
    public Room? Room { get; }
}
