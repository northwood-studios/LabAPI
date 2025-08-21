using InventorySystem.Items.ThrowableProjectiles;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ExplosionGrenade">HE grenade</see>.
/// </summary>
public class ExplosiveGrenadeProjectile : TimedGrenadeProjectile
{
    /// <summary>
    /// Contains all the cached item pickups, accessible through their <see cref="TimeGrenade"/>.
    /// </summary>
    public static new Dictionary<ExplosionGrenade, ExplosiveGrenadeProjectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ExplosiveGrenadeProjectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<ExplosiveGrenadeProjectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the explosion grenade from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="ExplosionGrenade"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> of the projectile.</param>
    /// <returns>The requested projectile or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static ExplosiveGrenadeProjectile? Get(ExplosionGrenade? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out ExplosiveGrenadeProjectile wrapper) ? wrapper : (ExplosiveGrenadeProjectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="ExplosionGrenade"/> of the pickup.</param>
    internal ExplosiveGrenadeProjectile(ExplosionGrenade projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="ExplosionGrenade"/> of the pickup.
    /// </summary>
    public new ExplosionGrenade Base { get; }

    /// <summary>
    /// Gets or sets the <see cref="LayerMask"/> this grenade will detect collisions on during explosion.
    /// </summary>
    public LayerMask DetectionMask
    {
        get => Base.DetectionMask;
        set => Base.DetectionMask = value;
    }

    /// <summary>
    /// Gets or sets the maximum range this grenade will detect collisions during explosion.
    /// </summary>
    public float MaxRadius
    {
        get => Base.MaxRadius;
        set => Base.MaxRadius = value;
    }

    /// <summary>
    /// Gets or sets the damage multiplier for SCPs.
    /// </summary>
    public float ScpDamageMultiplier
    {
        get => Base.ScpDamageMultiplier;
        set => Base.ScpDamageMultiplier = value;
    }

    /// <inheritdoc/>
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
