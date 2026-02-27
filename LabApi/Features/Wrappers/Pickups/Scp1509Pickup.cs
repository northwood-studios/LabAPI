using Mirror;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp1509Pickup = InventorySystem.Items.Scp1509.Scp1509Pickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseScp1509Pickup"/> class.
/// </summary>
public class Scp1509Pickup : Pickup
{
    /// <summary>
    /// Contains all the cached SCP-1509 pickups, accessible through their <see cref="BaseScp1509Pickup"/>.
    /// </summary>
    public static new Dictionary<BaseScp1509Pickup, Scp1509Pickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp1509Pickup"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp1509Pickup> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-1509 pickup from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp1509Pickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The base <see cref="BaseScp1509Pickup"/> of the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static Scp1509Pickup? Get(BaseScp1509Pickup? pickup)
    {
        if (pickup == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(pickup, out Scp1509Pickup wrapper) ? wrapper : (Scp1509Pickup)CreateItemWrapper(pickup);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp1509">The base <see cref="BaseScp1509Pickup"/> object.</param>
    internal Scp1509Pickup(BaseScp1509Pickup baseScp1509)
        : base(baseScp1509)
    {
        Base = baseScp1509;

        if (CanCache)
        {
            Dictionary.Add(baseScp1509, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseScp1509Pickup"/> object.
    /// </summary>
    public new BaseScp1509Pickup Base { get; }

    /// <summary>
    /// Gets or sets the next revive time.
    /// </summary>
    public double NextReviveTime
    {
        get => Base.NextReviveTime;
        set
        {
            Base.NextReviveTime = value;
            Base.ResurrectDirty = true;
        }
    }

    /// <summary>
    /// Gets or sets the next revive time delta.
    /// </summary>
    /// <remarks>
    /// Get and set is returning the value without <see cref="NetworkTime.time"/>.
    /// </remarks>
    public double NextReviveTimeDelta
    {
        get => Base.NextReviveTime - NetworkTime.time;
        set
        {
            Base.NextReviveTime = NetworkTime.time + value;
            Base.ResurrectDirty = true;
        }
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
