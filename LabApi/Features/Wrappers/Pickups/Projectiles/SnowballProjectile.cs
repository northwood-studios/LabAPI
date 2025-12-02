using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseSnowballProjectile = InventorySystem.Items.ThrowableProjectiles.SnowballProjectile;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseSnowballProjectile">snowball projectile</see>.
/// </summary>
public class SnowballProjectile : FlybyDetectorProjectile
{
    /// <summary>
    /// Contains all the cached projectiles, accessible through their <see cref="BaseSnowballProjectile"/>.
    /// </summary>
    public static new Dictionary<BaseSnowballProjectile, SnowballProjectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Projectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<SnowballProjectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the projectile wrapper from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="SnowballProjectile"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> if the projectile.</param>
    /// <returns>The requested wrapper or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static SnowballProjectile? Get(BaseSnowballProjectile? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out SnowballProjectile wrapper) ? wrapper : (SnowballProjectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="SnowballProjectile"/> of the pickup.</param>
    internal SnowballProjectile(BaseSnowballProjectile projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseSnowballProjectile"/> of the pickup.
    /// </summary>
    public new BaseSnowballProjectile Base { get; }

    /// <inheritdoc />
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
