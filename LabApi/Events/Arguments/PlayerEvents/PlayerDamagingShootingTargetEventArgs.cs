using AdminToys;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DamagingShootingTarget"/> event.
/// </summary>
public class PlayerDamagingShootingTargetEventArgs : EventArgs, IPlayerEvent, IShootingTargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDamagingShootingTargetEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is damaging the shooting target.</param>
    /// <param name="target">The shooting target.</param>
    /// <param name="damageHandler">The damage handler.</param>
    public PlayerDamagingShootingTargetEventArgs(ReferenceHub player, ShootingTarget target, DamageHandlerBase damageHandler)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        ShootingTarget = ShootingTargetToy.Get(target);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who is damaging the shooting target.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the shooting target.
    /// </summary>
    public ShootingTargetToy ShootingTarget { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="ShootingTarget"/>
    [Obsolete($"Use {nameof(ShootingTarget)} instead")]
    public ShootingTarget Target => ShootingTarget.Base;
}