using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseAmmoPickup = InventorySystem.Items.Firearms.Ammo.AmmoPickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseAmmoPickup"/> class.
/// </summary>
public class AmmoPickup : Pickup
{
    /// <summary>
    /// Contains all the cached ammo pickups, accessible through their <see cref="BaseAmmoPickup"/>.
    /// </summary>
    public new static Dictionary<BaseAmmoPickup, AmmoPickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="AmmoPickup"/>.
    /// </summary>
    public new static IReadOnlyCollection<AmmoPickup> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseAmmoPickup">The base <see cref="BaseAmmoPickup"/> object.</param>
    internal AmmoPickup(BaseAmmoPickup baseAmmoPickup)
        : base(baseAmmoPickup)
    {
        Base = baseAmmoPickup;

        if (CanCache)
            Dictionary.Add(baseAmmoPickup, this);
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The <see cref="BaseAmmoPickup"/> object.
    /// </summary>
    public new BaseAmmoPickup Base { get; }

    /// <summary>
    /// Gets or sets the ammo stored in this pickup.
    /// </summary>
    public ushort Ammo
    {
        get => Base.SavedAmmo;
        set => Base.NetworkSavedAmmo = value;
    }

    /// <summary>
    /// Gets the ammo pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseAmmoPickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> of the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static AmmoPickup? Get(BaseAmmoPickup? pickup)
    {
        if (pickup == null)
            return null;

        return Dictionary.TryGetValue(pickup, out AmmoPickup wrapper) ? wrapper : (AmmoPickup)CreateItemWrapper(pickup);
    }
}