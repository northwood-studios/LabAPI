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
    /// <param name="hub">The player whose blood it is.</param>
    /// <param name="attacker">The player that attacked.</param>
    /// <param name="hitPosition">Position at which is blood being placed.</param>
    /// <param name="startRaycast">Position where the blood decal raycast will start for it to be properly attached to surface.</param>
    public PlayerPlacingBloodEventArgs(ReferenceHub hub, ReferenceHub attacker, Vector3 hitPosition, Vector3 startRaycast)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Attacker = Player.Get(attacker);
        HitPosition = hitPosition;
        RaycastStart = startRaycast;
    }

    /// <summary>
    /// Gets the player whose blood it is.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player that attacked the <see cref="Player"/>.
    /// </summary>
    public Player Attacker { get; }

    /// <summary>
    /// Gets the position at which is blood being placed.
    /// </summary>
    public Vector3 HitPosition { get; set; }

    /// <summary>
    /// Gets or sets the position where the blood decal raycast will start for it to be properly attached to surface.
    /// </summary>
    public Vector3 RaycastStart { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}