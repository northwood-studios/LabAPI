using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a door.
/// </summary>
public interface IDoorEvent
{
    /// <summary>
    /// The door that is involved in the event.
    /// </summary>
    public Door? Door { get; }
}
