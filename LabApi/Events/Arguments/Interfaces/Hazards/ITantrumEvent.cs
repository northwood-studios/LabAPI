using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a tantrum hazard.
/// </summary>
public interface ITantrumEvent : IHazardEvent
{
    /// <inheritdoc />
    Hazard? IHazardEvent.Hazard => Tantrum;

    /// <summary>
    /// The tantrum hazard that is involved in the event.
    /// </summary>
    public TantrumHazard? Tantrum { get; }
}