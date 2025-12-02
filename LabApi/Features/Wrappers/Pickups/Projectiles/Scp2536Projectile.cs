using InventorySystem.Items.ThrowableProjectiles;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp2536Projectile = InventorySystem.Items.ThrowableProjectiles.Scp2536Projectile;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp2536Projectile">Scp-2536</see>.
/// </summary>
public class Scp2536Projectile : FlybyDetectorProjectile
{
    /// <summary>
    /// Contains all the cached projectiles, accessible through their <see cref="BaseScp2536Projectile"/>.
    /// </summary>
    public static new Dictionary<BaseScp2536Projectile, Scp2536Projectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Projectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp2536Projectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the projectile wrapper from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="Scp2536Projectile"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> if the projectile.</param>
    /// <returns>The requested wrapper or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static Scp2536Projectile? Get(BaseScp2536Projectile? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out Scp2536Projectile wrapper) ? wrapper : (Scp2536Projectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="ThrownProjectile"/> of the pickup.</param>
    internal Scp2536Projectile(BaseScp2536Projectile projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseScp2536Projectile"/> of the pickup.
    /// </summary>
    public new BaseScp2536Projectile Base { get; }

    /// <inheritdoc />
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
