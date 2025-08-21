using InventorySystem.Items.Pickups;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="InventorySystem.Items.ThrowableProjectiles.Scp018Projectile">SCP-018</see>.
/// </summary>
public class Scp018Projectile : TimedGrenadeProjectile
{
    /// <summary>
    /// Contains all the cached item pickups, accessible through their <see cref="InventorySystem.Items.ThrowableProjectiles.Scp018Projectile"/>.
    /// </summary>
    public static new Dictionary<InventorySystem.Items.ThrowableProjectiles.Scp018Projectile, Scp018Projectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="InventorySystem.Items.ThrowableProjectiles.Scp018Projectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp018Projectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the Scp-018 from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="InventorySystem.Items.ThrowableProjectiles.Scp018Projectile"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> of the projectile.</param>
    /// <returns>The requested projectile or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static Scp018Projectile? Get(InventorySystem.Items.ThrowableProjectiles.Scp018Projectile? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out Scp018Projectile wrapper) ? wrapper : (Scp018Projectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="InventorySystem.Items.ThrowableProjectiles.Scp018Projectile"/> of the pickup.</param>
    internal Scp018Projectile(InventorySystem.Items.ThrowableProjectiles.Scp018Projectile projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="InventorySystem.Items.ThrowableProjectiles.Scp018Projectile"/> of the pickup.
    /// </summary>
    public new InventorySystem.Items.ThrowableProjectiles.Scp018Projectile Base { get; }

    /// <summary>
    /// Gets the physics module for this ball.
    /// </summary>
    public new PickupStandardPhysics PhysicsModule => PhysicsModule;

    /// <summary>
    /// Gets the damage applied to player on hit.
    /// </summary>
    public float CurrentDamage => Base.CurrentDamage;

    /// <summary>
    /// Gets or sets the velocity of SCP-018.
    /// </summary>
    public Vector3 Velocity
    {
        get => PhysicsModule.Rb.linearVelocity;
        set => PhysicsModule.Rb.linearVelocity = value;
    }

    /// <summary>
    /// Plays the bounce sound. Defaults to <see cref="Velocity"/> if value is below 0.
    /// <para>
    /// Intensities:<br/>
    /// 4-150 Low<br/>
    /// 150-400 Medium<br/>
    /// 400+ High.
    /// </para>
    /// </summary>
    /// <param name="velSqrt">Velocity to play the sound for.</param>
    public void PlayBounceSound(float velSqrt = -1) => Base.RpcPlayBounce(velSqrt < 0 ? Velocity.sqrMagnitude : velSqrt);

    /// <inheritdoc/>
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
