using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BaseThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseThrowableItem"/>.
/// </summary>
public class ThrowableItem : Item
{
    /// <summary>
    /// Contains all the cached throwable items, accessible through their <see cref="BaseThrowableItem"/>.
    /// </summary>
    public static new Dictionary<BaseThrowableItem, ThrowableItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ThrowableItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<ThrowableItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the throwable item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseThrowableItem"/> was not null.
    /// </summary>
    /// <param name="baseThrowableItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseThrowableItem))]
    public static ThrowableItem? Get(BaseThrowableItem? baseThrowableItem)
    {
        if (baseThrowableItem == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseThrowableItem, out ThrowableItem item) ? item : (ThrowableItem)CreateItemWrapper(baseThrowableItem);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseThrowableItem">The base <see cref="BaseThrowableItem"/> object.</param>
    internal ThrowableItem(BaseThrowableItem baseThrowableItem)
        : base(baseThrowableItem)
    {
        Base = baseThrowableItem;

        if (CanCache)
        {
            Dictionary.Add(baseThrowableItem, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseThrowableItem"/> object.
    /// </summary>
    public new BaseThrowableItem Base { get; }

    ///// <summary>
    ///// The projectile prefab instance, changes to this will be reflect across all new spawned projectiles.
    ///// </summary>
    // TODO: use projectile wrapper.
    // FIX: prefab caching.
    // Pickup ProjectilePrefab => Pickup.Get(Base.Projectile);

    /// <summary>
    /// Gets or set the velocity added in the forward direction on a weak throw.
    /// </summary>
    public float WeakThrowStartVelocity
    {
        get => Base.WeakThrowSettings.StartVelocity;
        set => Base.WeakThrowSettings.StartVelocity = value;
    }

    /// <summary>
    /// Gets or sets the velocity added in the upward direction on a weak throw.
    /// </summary>
    public float WeakThrowUpwardsFactor
    {
        get => Base.WeakThrowSettings.UpwardsFactor;
        set => Base.WeakThrowSettings.UpwardsFactor = value;
    }

    /// <summary>
    /// Gets or sets the torque added to the projectile on a weak throw.
    /// </summary>
    public Vector3 WeakThrowStartTorque
    {
        get => Base.WeakThrowSettings.StartTorque;
        set => Base.WeakThrowSettings.StartTorque = value;
    }

    /// <summary>
    /// Gets or sets the spawnpoint relative to the players camera on a weak throw.
    /// </summary>
    public Vector3 WeakThrowRelativePosition
    {
        get => Base.WeakThrowSettings.RelativePosition;
        set => Base.WeakThrowSettings.RelativePosition = value;
    }

    /// <summary>
    /// Gets or set the velocity added in the forward direction on a full throw.
    /// </summary>
    public float FullThrowStartVelocity
    {
        get => Base.FullThrowSettings.StartVelocity;
        set => Base.FullThrowSettings.StartVelocity = value;
    }

    /// <summary>
    /// Gets or sets the velocity added in the upward direction on a full throw.
    /// </summary>
    public float FullThrowUpwardsFactor
    {
        get => Base.FullThrowSettings.UpwardsFactor;
        set => Base.FullThrowSettings.UpwardsFactor = value;
    }

    /// <summary>
    /// Gets or sets the torque added to the projectile on a full throw.
    /// </summary>
    public Vector3 FullThrowStartTorque
    {
        get => Base.FullThrowSettings.StartTorque;
        set => Base.FullThrowSettings.StartTorque = value;
    }

    /// <summary>
    /// Gets or sets the spawnpoint relative to the players camera on a full throw.
    /// </summary>
    public Vector3 FullThrowRelativePosition
    {
        get => Base.FullThrowSettings.StartTorque;
        set => Base.FullThrowSettings.StartTorque = value;
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
