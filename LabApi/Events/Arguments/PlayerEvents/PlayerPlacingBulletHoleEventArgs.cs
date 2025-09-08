using Decals;
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
    /// <param name="hub">The player who caused it.</param>
    /// <param name="type">Decal type to be spawned.</param>
    /// <param name="hitPosition">Position at which is bullet hole being placed.</param>
    /// <param name="startRaycast">Position where the bullet hole raycast will start for it to be properly attached to surface.</param>
    public PlayerPlacingBulletHoleEventArgs(ReferenceHub hub, DecalPoolType type, Vector3 hitPosition, Vector3 startRaycast)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        DecalType = type;
        HitPosition = hitPosition;
        RaycastStart = startRaycast;
    }

    /// <summary>
    /// Gets the player who caused it.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the decal type to be spawned.
    /// </summary>
    public DecalPoolType DecalType { get; set; }

    /// <summary>
    /// Gets the position at which is bullet hole being placed.
    /// </summary>
    public Vector3 HitPosition { get; set; }

    /// <summary>
    /// Gets or sets the position where the bullet hole raycast will start for it to be properly attached to surface.
    /// </summary>
    public Vector3 RaycastStart { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}