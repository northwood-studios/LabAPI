using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a respawn wave. 
/// </summary>
public interface IRespawnWaveEvent
{
    /// <summary>
    /// The respawn wave that is involved in the event.
    /// </summary>
    public RespawnWave? Wave { get; }
}
