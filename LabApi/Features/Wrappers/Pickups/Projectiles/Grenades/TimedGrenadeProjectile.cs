using Footprinting;
using InventorySystem;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Utils;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="TimeGrenade">time grenade</see>.
/// This includes HE grenade, Flashbang, SCP-018 and SCP-2176.
/// </summary>
public class TimedGrenadeProjectile : Projectile
{
    /// <summary>
    /// Contains all the cached item pickups, accessible through their <see cref="TimeGrenade"/>.
    /// </summary>
    public static new Dictionary<TimeGrenade, TimedGrenadeProjectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="TimeGrenade"/>.
    /// </summary>
    public static new IReadOnlyCollection<TimedGrenadeProjectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the timed grenade from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="TimeGrenade"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> of the projectile.</param>
    /// <returns>The requested projectile or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static TimedGrenadeProjectile? Get(TimeGrenade? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out TimedGrenadeProjectile wrapper) ? wrapper : (TimedGrenadeProjectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// Spawns a explosion particles and effect on specified location.<br/>
    /// Valid for <see cref="ItemType.GrenadeHE"/>, <see cref="ItemType.GrenadeFlash"/> and <see cref="ItemType.SCP2176"/>. Doesn't do anything for any other input.
    /// </summary>
    /// <param name="position">Target world position to play the effect on.</param>
    /// <param name="type">The type of the effect.</param>
    public static void PlayEffect(Vector3 position, ItemType type) => ExplosionUtils.ServerSpawnEffect(position, type);

    /// <summary>
    /// Spawns a active timed grenade with specified parameters.
    /// </summary>
    /// <param name="pos">The position to spawn the grenade on.</param>
    /// <param name="type">Type of the grenade.</param>
    /// <param name="owner">The player owner of the grenade.</param>
    /// <param name="timeOverride">Time override until detonation. A value less than 0 will not change the original fuse time.</param>
    /// <returns>An active projectile.</returns>
    public static TimedGrenadeProjectile? SpawnActive(Vector3 pos, ItemType type, Player? owner = null, double timeOverride = -1d)
    {
        if (!InventoryItemLoader.TryGetItem(type, out InventorySystem.Items.ThrowableProjectiles.ThrowableItem throwable))
        {
            return null;
        }

        if (throwable.Projectile is not TimeGrenade grenade)
        {
            return null;
        }

        TimeGrenade newPickup = GameObject.Instantiate(grenade, pos, Quaternion.identity);

        PickupSyncInfo psi = new(throwable.ItemTypeId, throwable.Weight, locked: true);

        newPickup.Info = psi;
        newPickup.PreviousOwner = new Footprint(owner?.ReferenceHub);
        NetworkServer.Spawn(newPickup.gameObject);

        newPickup.ServerActivate();
        TimedGrenadeProjectile wrapper = (TimedGrenadeProjectile)Pickup.Get(newPickup);

        if (timeOverride >= 0)
        {
            wrapper.RemainingTime = timeOverride;
        }

        return wrapper;
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="TimeGrenade"/> of the pickup.</param>
    internal TimedGrenadeProjectile(TimeGrenade projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="TimeGrenade"/> of the pickup.
    /// </summary>
    public new TimeGrenade Base { get; }

    /// <summary>
    /// Gets or sets the remaining time until detonation in seconds.
    /// </summary>
    public double RemainingTime
    {
        get => Base.TargetTime;
        set => Base.TargetTime = NetworkTime.time + value;
    }

    /// <summary>
    /// Ends the fuse of this grenade, causing instant detonation.
    /// </summary>
    public virtual void FuseEnd() => Base.ServerFuseEnd();

    /// <inheritdoc/>
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
