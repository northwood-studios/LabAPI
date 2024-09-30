using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.EnteringPocketDimension"/> event.
/// </summary>
public class PlayerEnteringPocketDimensionEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEnteringPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is entering the pocket dimension.</param>
    public PlayerEnteringPocketDimensionEventArgs(ReferenceHub player)
    {
        IsAllowed = true;
        Player = Player.Get(player);
    }

    /// <summary>
    /// Gets the player who is entering the pocket dimension.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}