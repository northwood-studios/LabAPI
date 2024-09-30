﻿using AdminToys;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingShootingTarget"/> event.
/// </summary>
public class PlayerInteractingShootingTargetEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractingShootingTargetEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is interacting with the target.</param>
    /// <param name="target">The shooting target.</param>
    public PlayerInteractingShootingTargetEventArgs(ReferenceHub player, ShootingTarget target)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Target = target;
    }

    /// <summary>
    /// Gets the player who is interacting with the target.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the shooting target.
    /// </summary>
    public ShootingTarget Target { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}