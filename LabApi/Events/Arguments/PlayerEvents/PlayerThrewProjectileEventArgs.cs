using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.ThrowableProjectiles.ThrowableItem;
using ThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ThrewProjectile"/> event.
/// </summary>
public class PlayerThrewProjectileEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerThrewProjectileEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who threw the throwable item.</param>
    /// <param name="item">The original item that was thrown.</param>
    /// <param name="projectileSettings">Projectile settings at which throwable was thrown.</param>
    /// <param name="fullForce">Value whenever the throwable was thrown at full force (overhand).</param>
    public PlayerThrewProjectileEventArgs(ReferenceHub player, ThrowableItem item, ProjectileSettings projectileSettings, bool fullForce)
    {
        Player = Player.Get(player);
        Item = item;
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
    public ThrowableItem Item { get; }

    /// <summary>
    /// Gets the projectile settings at which throwable was thrown.
    /// </summary>
    public ProjectileSettings ProjectileSettings { get; }

    /// <summary>
    /// Gets the value whenever the throwable was thrown at full force (overhand).
    /// </summary>
    public bool FullForce { get; }
}