using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that is related to a player.
/// </summary>
public interface IPlayerEvent
{
    /// <summary>
    /// Gets the player that invoked the event.
    /// </summary>
    public Player Player { get; }
}