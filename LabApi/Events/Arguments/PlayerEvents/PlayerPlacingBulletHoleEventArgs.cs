using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PlacingBulletHole"/> event.
/// </summary>
public class PlayerPlacingBulletHoleEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPlacingBulletHoleEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who caused it.</param>
    /// <param name="hitPosition">Position at which is bullet hole being placed.</param>
    public PlayerPlacingBulletHoleEventArgs(ReferenceHub player, Vector3 hitPosition)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        HitPosition = hitPosition;
    }

    /// <summary>
    /// Gets the player who caused it.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the position at which is bullet hole being placed.
    /// </summary>
    public Vector3 HitPosition { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}