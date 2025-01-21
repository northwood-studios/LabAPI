using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a hazard.
/// </summary>
public interface IHazardEvent
{
    /// <summary>
    /// Hazard that is involved in the event.
    /// </summary>
    public Hazard Hazard { get; }
}

