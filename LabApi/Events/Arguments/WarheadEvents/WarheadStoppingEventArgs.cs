using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.WarheadEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.WarheadEvents.Stopping"/> event.
/// </summary>
public class WarheadStoppingEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarheadStoppingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is stopping the warhead.</param>
    /// <param name="warheadState">The current state of the alpha warhead.</param>
    public WarheadStoppingEventArgs(Player player, AlphaWarheadSyncInfo warheadState)
    {
        IsAllowed = true;
        Player = player;
        WarheadState = warheadState;
    }
    
    /// <summary>
    /// Gets the current state of the alpha warhead.
    /// </summary>
    public AlphaWarheadSyncInfo WarheadState { get; set; }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// Gets the player who is stopping the warhead.
    /// </summary>
    public Player Player { get; set; }
}