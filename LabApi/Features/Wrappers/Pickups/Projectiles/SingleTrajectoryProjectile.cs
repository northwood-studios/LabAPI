using InventorySystem.Items.ThrowableProjectiles;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseSingleTrajectoryProjectile = InventorySystem.Items.ThrowableProjectiles.SingleTrajectoryProjectile;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseSingleTrajectoryProjectile">pre-calculated trajectory</see>.
/// </summary>
public class SingleTrajectoryProjectile : Projectile
{
    /// <summary>
    /// Contains all the cached projectiles, accessible through their <see cref="BaseSingleTrajectoryProjectile"/>.
    /// </summary>
    public static new Dictionary<BaseSingleTrajectoryProjectile, SingleTrajectoryProjectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Projectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<SingleTrajectoryProjectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the projectile wrapper from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="ThrownProjectile"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> if the projectile.</param>
    /// <returns>The requested wrapper or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static SingleTrajectoryProjectile? Get(BaseSingleTrajectoryProjectile? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out SingleTrajectoryProjectile wrapper) ? wrapper : (SingleTrajectoryProjectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="ThrownProjectile"/> of the pickup.</param>
    internal SingleTrajectoryProjectile(BaseSingleTrajectoryProjectile projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseSingleTrajectoryProjectile"/> of the pickup.
    /// </summary>
    public new BaseSingleTrajectoryProjectile Base { get; }

    /// <inheritdoc />
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
