using InventorySystem.Items.ThrowableProjectiles;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="FlashbangGrenade">flashbang</see>.
/// </summary>
public class FlashbangProjectile : TimedGrenadeProjectile
{
    /// <summary>
    /// Contains all the cached item pickups, accessible through their <see cref="FlashbangGrenade"/>.
    /// </summary>
    public static new Dictionary<FlashbangGrenade, FlashbangProjectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ExplosiveGrenadeProjectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<FlashbangProjectile> List => Dictionary.Values;

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="FlashbangGrenade"/> of the pickup.</param>
    internal FlashbangProjectile(FlashbangGrenade projectilePickup) : base(projectilePickup)
    {
        Base = projectilePickup;

        Dictionary.Add(projectilePickup, this);
    }

    /// <summary>
    /// The <see cref="FlashbangGrenade"/> of the pickup.
    /// </summary>
    public new FlashbangGrenade Base { get; }

    /// <summary>
    /// Gets or sets the mask used to block linecast from the flashbang to the <see cref="Player"/> to determine whether the <see cref="Player"/> should be affected.
    /// </summary>
    public LayerMask BlockingMask
    {
        get => Base.BlindingMask;
        set => Base.BlindingMask = value;
    }

    /// <summary>
    /// Gets or sets the base blind time to affect people with.
    /// <para>Note that this value is affected by distance from the grenade aswell as whether the player is looking at it and if it is on surface.</para>
    /// </summary>
    public float BaseBlindTime
    {
        get => Base.BlindTime;
        set => Base.BlindTime = value;
    }

    /// <inheritdoc/>
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }

    /// <summary>
    /// Gets the flashbang from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="FlashbangGrenade"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> of the projectile.</param>
    /// <returns>The requested projectile or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static FlashbangProjectile? Get(FlashbangGrenade? projectile)
    {
        if (projectile == null)
            return null;

        return Dictionary.TryGetValue(projectile, out FlashbangProjectile wrapper) ? wrapper : (FlashbangProjectile)CreateItemWrapper(projectile);
    }
}

