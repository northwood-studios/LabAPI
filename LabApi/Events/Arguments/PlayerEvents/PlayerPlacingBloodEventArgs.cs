using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PlacingBlood"/> event.
/// </summary>
public class PlayerPlacingBloodEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPlacingBloodEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose blood it is.</param>
    /// <param name="hitPosition">Position at which is blood being placed.</param>
    public PlayerPlacingBloodEventArgs(ReferenceHub player, Vector3 hitPosition)
    {
        Player = Player.Get(player);
        HitPosition = hitPosition;
    }

    /// <summary>
    /// Gets the player whose blood it is.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the position at which is blood being placed.
    /// </summary>
    public Vector3 HitPosition { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}