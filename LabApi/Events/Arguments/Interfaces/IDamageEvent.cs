using PlayerStatsSystem;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involved a damage handler.
/// </summary>
public interface IDamageEvent
{
    /// <summary>
    /// The damage handler that is involved in the event.
    /// </summary>
    public DamageHandlerBase? DamageHandler { get; }
}
