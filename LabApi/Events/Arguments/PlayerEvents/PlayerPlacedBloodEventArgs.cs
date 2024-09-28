using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PlacedBlood"/> event.
/// </summary>
public class PlayerPlacedBloodEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPlacedBloodEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose blood it is.</param>
    /// <param name="hitPosition">Position at which blood has been spawned.</param>
    public PlayerPlacedBloodEventArgs(ReferenceHub player, Vector3 hitPosition)
    {
        Player = Player.Get(player);
        HitPosition = hitPosition;
    }

    /// <summary>
    /// Gets the player whose blood it is.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the position at which blood has been spawned.
    /// </summary>
    public Vector3 HitPosition { get; }
}