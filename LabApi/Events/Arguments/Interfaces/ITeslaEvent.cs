using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that is related to a <see cref="Features.Wrappers.Tesla"/>.
/// </summary>
public interface ITeslaEvent
{
    /// <summary>
    /// Gets the <see cref="Features.Wrappers.Tesla"/> associated with the event.
    /// </summary>
    public Tesla Tesla { get; }
}