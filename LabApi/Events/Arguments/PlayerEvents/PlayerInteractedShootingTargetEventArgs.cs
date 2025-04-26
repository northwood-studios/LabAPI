﻿using AdminToys;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedShootingTarget"/> event.
/// </summary>
public class PlayerInteractedShootingTargetEventArgs : EventArgs, IPlayerEvent, IShootingTargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedShootingTargetEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who interacted with the target.</param>
    /// <param name="target">The shooting target.</param>
    public PlayerInteractedShootingTargetEventArgs(ReferenceHub player, ShootingTarget target)
    {
        Player = Player.Get(player);
        ShootingTarget = target;
    }

    /// <summary>
    /// Gets the player who interacted with the target.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the shooting target.
    /// </summary>
    public ShootingTarget ShootingTarget { get; }

    /// <inheritdoc cref="ShootingTarget"/>
    [Obsolete($"Use {nameof(ShootingTarget)} instead")]
    public ShootingTarget Target => ShootingTarget;
}