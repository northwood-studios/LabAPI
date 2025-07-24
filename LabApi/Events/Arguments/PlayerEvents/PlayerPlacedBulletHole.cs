using Decals;
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
    /// <param name="hub">The player who caused this bullet hole.</param>
    /// <param name="type">Decal type which has spawned.</param>
    /// <param name="hitPosition">Position at which bullet hole has spawned.</param>
    /// <param name="startRaycast">Position where the bullet hole raycast has started for it to be properly attached to surface.</param>
    public PlayerPlacedBulletHoleEventArgs(ReferenceHub hub, DecalPoolType type, Vector3 hitPosition, Vector3 startRaycast)
    {
        Player = Player.Get(hub);
        DecalType = type;
        HitPosition = hitPosition;
        RaycastStart = startRaycast;
    }

    /// <summary>
    /// Gets the player who caused this bullet hole.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the decal type to be spawned.
    /// </summary>
    public DecalPoolType DecalType { get; set; }

    /// <summary>
    /// Gets the position at which bullet hole has spawned.
    /// </summary>
    public Vector3 HitPosition { get; }

    /// <summary>
    /// Gets or sets the position where the bullet hole raycast will start for it to be properly attached to surface.
    /// </summary>
    public Vector3 RaycastStart { get; }
}