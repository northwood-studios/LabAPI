using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a locker structure.
/// </summary>
public interface ILockerEvent : IStructureEvent
{
    /// <inheritdoc />
    Structure? IStructureEvent.Structure => Locker;

    /// <summary>
    /// The locker that is involved in the event.
    /// </summary>
    public Locker? Locker { get; }
}