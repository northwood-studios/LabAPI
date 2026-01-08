using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseFlybyDetectorProjectile = InventorySystem.Items.ThrowableProjectiles.FlybyDetectorProjectile;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseFlybyDetectorProjectile">pre-calculated player detector trajectory</see>.
/// </summary>
public class FlybyDetectorProjectile : SingleTrajectoryProjectile
{
    /// <summary>
    /// Contains all the cached projectiles, accessible through their <see cref="BaseFlybyDetectorProjectile"/>.
    /// </summary>
    public static new Dictionary<BaseFlybyDetectorProjectile, FlybyDetectorProjectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Projectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<FlybyDetectorProjectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the projectile wrapper from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="FlybyDetectorProjectile"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> if the projectile.</param>
    /// <returns>The requested wrapper or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static FlybyDetectorProjectile? Get(BaseFlybyDetectorProjectile? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out FlybyDetectorProjectile wrapper) ? wrapper : (FlybyDetectorProjectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="BaseFlybyDetectorProjectile"/> of the pickup.</param>
    internal FlybyDetectorProjectile(BaseFlybyDetectorProjectile projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseFlybyDetectorProjectile"/> of the pickup.
    /// </summary>
    public new BaseFlybyDetectorProjectile Base { get; }

    /// <inheritdoc />
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
