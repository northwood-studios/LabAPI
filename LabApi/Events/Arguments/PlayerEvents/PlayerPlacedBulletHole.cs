using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PlacedBulletHole"/> event.
/// </summary>
public class PlayerPlacedBulletHoleEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPlacedBulletHoleEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who caused this bullet hole.</param>
    /// <param name="hitPosition">Position at which bullet hole has spawned.</param>
    public PlayerPlacedBulletHoleEventArgs(ReferenceHub player, Vector3 hitPosition)
    {
        Player = Player.Get(player);
        HitPosition = hitPosition;
    }

    /// <summary>
    /// Gets the player who caused this bullet hole.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the position at which bullet hole has spawned.
    /// </summary>
    public Vector3 HitPosition { get; }
}