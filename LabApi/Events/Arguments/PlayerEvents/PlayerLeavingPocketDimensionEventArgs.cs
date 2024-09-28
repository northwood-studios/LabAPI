using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.LeavingPocketDimension"/> event.
/// </summary>
public class PlayerLeavingPocketDimensionEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeavingPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is trying to leave from pocket dimension.</param>
    /// <param name="isSuccessful">Whenever it is gonna be success.</param>
    public PlayerLeavingPocketDimensionEventArgs(ReferenceHub player, bool isSuccessful)
    {
        Player = Player.Get(player);
        IsSuccessful = isSuccessful;
    }

    /// <summary>
    /// Gets the player who is trying to leave from pocket dimension.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets whether it is gonna be success.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}