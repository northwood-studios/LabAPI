using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.WarheadEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.WarheadEvents.Detonated"/> event.
/// </summary>
public class WarheadDetonatedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarheadDetonatedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who detonated the warhead.</param>
    public WarheadDetonatedEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <summary>
    /// Gets the player who detonated the warhead.
    /// </summary>
    public Player Player { get; }
}