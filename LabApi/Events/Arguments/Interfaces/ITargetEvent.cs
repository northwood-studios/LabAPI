using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that is related to a target player.
/// </summary>
public interface ITargetEvent
{
    /// <summary>
    /// Gets the player that was targeted in the event.
    /// </summary>
    public Player Target { get; }
}