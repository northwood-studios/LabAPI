using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseTimedGrenadePickup = InventorySystem.Items.ThrowableProjectiles.TimedGrenadePickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseTimedGrenadePickup"/> class.
/// </summary>
public class TimedGrenadePickup : Pickup
{
    /// <summary>
    /// Contains all the cached timed grenade pickups, accessible through their <see cref="BaseTimedGrenadePickup"/>.
    /// </summary>
    public new static Dictionary<BaseTimedGrenadePickup, TimedGrenadePickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="TimedGrenadePickup"/>.
    /// </summary>
    public new static IReadOnlyCollection<TimedGrenadePickup> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseTimedGrenadePickup">The base <see cref="BaseTimedGrenadePickup"/> object.</param>
    internal TimedGrenadePickup(BaseTimedGrenadePickup baseTimedGrenadePickup)
        : base(baseTimedGrenadePickup)
    {
        Base = baseTimedGrenadePickup;

        if (CanCache)
            Dictionary.Add(baseTimedGrenadePickup, this);
    }

    /// <summary>
    /// A internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The <see cref="BaseTimedGrenadePickup"/> object.
    /// </summary>
    public new BaseTimedGrenadePickup Base { get; }

    /// <summary>
    /// Gets the timed grenade pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseTimedGrenadePickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static TimedGrenadePickup? Get(BaseTimedGrenadePickup? pickup)
    {
        if (pickup == null)
            return null;

        return Dictionary.TryGetValue(pickup, out TimedGrenadePickup wrapper) ? wrapper : (TimedGrenadePickup)CreateItemWrapper(pickup);
    }
}