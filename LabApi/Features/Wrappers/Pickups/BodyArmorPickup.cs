using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseBodyArmorPickup = InventorySystem.Items.Armor.BodyArmorPickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseBodyArmorPickup"/> class.
/// </summary>
public class BodyArmorPickup : Pickup
{
    /// <summary>
    /// Contains all the cached body armor pickups, accessible through their <see cref="BaseBodyArmorPickup"/>.
    /// </summary>
    public static new Dictionary<BaseBodyArmorPickup, BodyArmorPickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="BodyArmorPickup"/>.
    /// </summary>
    public static new IReadOnlyCollection<BodyArmorPickup> List => Dictionary.Values;

    /// <summary>
    /// Gets the body armor pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseBodyArmorPickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static BodyArmorPickup? Get(BaseBodyArmorPickup? pickup)
    {
        if (pickup == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(pickup, out BodyArmorPickup wrapper) ? wrapper : (BodyArmorPickup)CreateItemWrapper(pickup);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseBodyArmorPickup">The base <see cref="BaseBodyArmorPickup"/> object.</param>
    internal BodyArmorPickup(BaseBodyArmorPickup baseBodyArmorPickup)
        : base(baseBodyArmorPickup)
    {
        Base = baseBodyArmorPickup;

        if (CanCache)
        {
            Dictionary.Add(baseBodyArmorPickup, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseBodyArmorPickup"/> object.
    /// </summary>
    public new BaseBodyArmorPickup Base { get; }

    /// <summary>
    /// A internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
