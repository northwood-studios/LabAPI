using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ProjectileExploded"/> event.
/// </summary>
public class ProjectileExplodedEventArgs : EventArgs, IPlayerEvent, ITimedGrenadeEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectileExplodedEventArgs"/> class.
    /// </summary>
    /// <param name="projectile">The projectile which will cause explosion.</param>
    /// <param name="hub">The player which threw that grenade.</param>
    /// <param name="position">The position of explosion.</param>
    public ProjectileExplodedEventArgs(TimeGrenade projectile, ReferenceHub hub, Vector3 position)
    {
        TimedGrenade = TimedGrenadeProjectile.Get(projectile);
        Player = Player.Get(hub);
        Position = position;
    }

    /// <summary>
    /// Gets projectile which caused this explosion.
    /// </summary>
    public TimedGrenadeProjectile TimedGrenade { get; }

    /// <summary>
    /// Gets who threw this grenade.
    /// </summary>
    public Player? Player { get; }

    /// <summary>
    /// Gets position of explosion.
    /// </summary>
    public Vector3 Position { get; }

    /// <inheritdoc cref="TimedGrenade"/>
    [Obsolete($"Use {nameof(TimedGrenade)} instead")]
    public TimedGrenadeProjectile Projectile => TimedGrenade;
}
