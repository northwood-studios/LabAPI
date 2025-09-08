using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.ThrowableProjectiles.ThrowableItem;
using BaseThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ThrowingProjectile"/> event.
/// </summary>
public class PlayerThrowingProjectileEventArgs : EventArgs, IPlayerEvent, IThrowableItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerThrowingProjectileEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who threw the projectile.</param>
    /// <param name="item">The original throwable item.</param>
    /// <param name="projectileSettings">Projectile settings at which is throwable being thrown.</param>
    /// <param name="fullForce">Value whenever the throwable is being thrown at full force (overhand).</param>
    public PlayerThrowingProjectileEventArgs(ReferenceHub hub, BaseThrowableItem item, ProjectileSettings projectileSettings, bool fullForce)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        ThrowableItem = ThrowableItem.Get(item);
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
    public ThrowableItem ThrowableItem { get; }

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

    /// <inheritdoc cref="ThrowableItem"/>
    [Obsolete($"Use {nameof(ThrowableItem)} instead")]
    public BaseThrowableItem Item => ThrowableItem.Base;
}