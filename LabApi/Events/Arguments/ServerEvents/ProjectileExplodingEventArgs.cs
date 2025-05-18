using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ProjectileExploding"/> event.
/// </summary>
public class ProjectileExplodingEventArgs : EventArgs, IPlayerEvent, ITimedGrenadeEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectileExplodingEventArgs"/> class.
    /// </summary>
    /// <param name="grenade">The grenade which will cause explosion.</param>
    /// <param name="player">The player which threw that grenade.</param>
    /// <param name="position">The position of explosion.</param>
    public ProjectileExplodingEventArgs(TimeGrenade grenade, ReferenceHub player, Vector3 position)
    {
        TimedGrenade = TimedGrenadeProjectile.Get(grenade);
        Player = Player.Get(player);
        Position = position;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets grenade which caused this explosion.
    /// </summary>
    public TimedGrenadeProjectile TimedGrenade { get; }

    /// <summary>
    /// Gets who threw this grenade.
    /// </summary>
    public Player? Player { get; set; }

    /// <summary>
    /// Gets or sets position of explosion.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="TimedGrenade"/>
    [Obsolete($"Use {nameof(TimedGrenade)} instead")]
    public TimedGrenadeProjectile Grenade => TimedGrenade;
}