using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an elevator.
/// </summary>
public interface IElevatorEvent
{
    /// <summary>
    /// The elevator that is involved in the event.
    /// </summary>
    public Elevator? Elevator { get; }
}