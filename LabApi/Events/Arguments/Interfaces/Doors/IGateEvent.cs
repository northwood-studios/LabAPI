using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a gate.
/// </summary>
public interface IGateEvent : IDoorEvent
{
    /// <inheritdoc />
    Door? IDoorEvent.Door => Gate;

    /// <summary>
    /// The gate that is involved in the event.
    /// </summary>
    public Gate Gate { get; }
}
