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
    /// <param name="hub">The player who entered the pocket dimension.</param>
    public PlayerEnteredPocketDimensionEventArgs(ReferenceHub hub)
    {
        Player = Player.Get(hub);
    }

    /// <summary>
    /// Gets the player who entered the pocket dimension.
    /// </summary>
    public Player Player { get; } 
}