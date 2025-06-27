using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an amnestic cloud hazard.
/// </summary>
public interface IAmnesticCloudEvent : IHazardEvent
{
    /// <inheritdoc />
    Hazard? IHazardEvent.Hazard => AmnesticCloud;

    /// <summary>
    /// The amnestic cloud hazard that is involved in the event.
    /// </summary>
    public AmnesticCloudHazard? AmnesticCloud { get; }
}