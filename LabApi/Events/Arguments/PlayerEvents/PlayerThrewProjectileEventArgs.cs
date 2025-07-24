using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.ThrowableProjectiles.ThrowableItem;
using BaseThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;
using ThrownProjectile = InventorySystem.Items.ThrowableProjectiles.ThrownProjectile;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ThrewProjectile"/> event.
/// </summary>
public class PlayerThrewProjectileEventArgs : EventArgs, IPlayerEvent, IThrowableItemEvent, IProjectileEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerThrewProjectileEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who threw the throwable item.</param>
    /// <param name="item">The original item that was thrown.</param>
    /// <param name="projectile">The new projectile object created.</param>
    /// <param name="projectileSettings">Projectile settings at which throwable was thrown.</param>
    /// <param name="fullForce">Value whenever the throwable was thrown at full force (overhand).</param>
    public PlayerThrewProjectileEventArgs(ReferenceHub hub, BaseThrowableItem item, ThrownProjectile projectile, ProjectileSettings projectileSettings, bool fullForce)
    {
        Player = Player.Get(hub);
        ThrowableItem = ThrowableItem.Get(item);
        Projectile = Projectile.Get(projectile);
        ProjectileSettings = projectileSettings;
        FullForce = fullForce;
    }

    /// <summary>
    /// Gets the player who threw the throwable item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the original item that was thrown.
    /// </summary>
    public ThrowableItem ThrowableItem { get; }

    /// <summary>
    /// Gets the projectile object.
    /// </summary>
    public Projectile Projectile { get; }

    /// <summary>
    /// Gets the projectile settings at which throwable was thrown.
    /// </summary>
    public ProjectileSettings ProjectileSettings { get; }

    /// <summary>
    /// Gets the value whenever the throwable was thrown at full force (overhand).
    /// </summary>
    public bool FullForce { get; }

    /// <inheritdoc cref="ThrowableItem"/>
    [Obsolete($"Use {nameof(ThrowableItem)} instead")]
    public BaseThrowableItem Item => ThrowableItem.Base;
}