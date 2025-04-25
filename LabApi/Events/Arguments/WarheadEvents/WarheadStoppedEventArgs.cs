using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.WarheadEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.WarheadEvents.Stopped"/> event.
/// </summary>
public class WarheadStoppedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarheadStoppedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who stopped the warhead.</param>
    /// <param name="warheadState">The current state of the alpha warhead.</param>
    public WarheadStoppedEventArgs(ReferenceHub player, AlphaWarheadSyncInfo warheadState)
    {
        Player = Player.Get(player);
        WarheadState = warheadState;
    }

    /// <summary>
    /// Gets the current state of the alpha warhead.
    /// </summary>
    public AlphaWarheadSyncInfo WarheadState { get; }

    /// <summary>
    /// Gets the player who stopped the warhead.
    /// </summary>
    public Player Player { get; }
}