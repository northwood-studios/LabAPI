using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.EnteredPocketDimension"/> event.
/// </summary>
public class PlayerEnteredPocketDimensionEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEnteredPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who entered the pocket dimension.</param>
    public PlayerEnteredPocketDimensionEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <summary>
    /// Gets the player who entered the pocket dimension.
    /// </summary>
    public Player Player { get; } 
}