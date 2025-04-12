using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.TogglingNoclip"/> event.
/// </summary>
public class PlayerTogglingNoclipEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of <see cref=" PlayerTogglingNoclipEventArgs"/> clas.
    /// </summary>
    /// <param name="player">The player who is attempting to toggle noclip.</param>
    public PlayerTogglingNoclipEventArgs(ReferenceHub player, bool newState)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        NewNoclipState = newState;
    }

    /// <summary>
    /// Gets the player who is attempting to toggle noclip.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the new state that will be applied to the noclip flag.
    /// </summary>
    public bool NewNoclipState { get; }

    /// <summary>
    /// Whether the event is allowed to run.
    /// </summary>
    /// <remarks>This value is assigned on based on whether the player has noclip permitted. So it may not be allowed by default for some players.</remarks>
    public bool IsAllowed { get; set; }
}
