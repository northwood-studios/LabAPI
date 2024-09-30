using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ThrowingProjectile"/> event.
/// </summary>
public class PlayerThrowingProjectileEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerThrowingProjectileEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who threw the projectile.</param>
    /// <param name="item">The original throwable item.</param>
    /// <param name="projectileSettings">Projectile settings at which is throwable being thrown.</param>
    /// <param name="fullForce">Value whenever the throwable is being thrown at full force (overhand).</param>
    //TODO: Throwable item wrapper
    public PlayerThrowingProjectileEventArgs(ReferenceHub player, ThrowableItem item, ProjectileSettings projectileSettings, bool fullForce)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Item = item;
        ProjectileSettings = projectileSettings;
        FullForce = fullForce;
    }

    /// <summary>
    /// Gets the player who threw the projectile.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the original throwable item.
    /// </summary>
    public ThrowableItem Item { get; }

    /// <summary>
    /// Gets or sets the projectile settings at which is throwable being thrown.
    /// </summary>
    public ProjectileSettings ProjectileSettings { get; set; }

    /// <summary>
    /// Gets or sets the value whenever the throwable was thrown at full force (overhand).
    /// </summary>
    public bool FullForce { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}