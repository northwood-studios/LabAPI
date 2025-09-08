using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseAmmoItem = InventorySystem.Items.Firearms.Ammo.AmmoItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseAmmoItem"/>.
/// </summary>
// Since ammo is stored in the inventory using a dict this likely only exists for the prefab and its pickup drop model and is never actually instantiated.
// Best to leave as last to implement to we can tell if this is really needed.
public class AmmoItem : Item
{
    /// <summary>
    /// Contains all the cached ammo items, accessible through their <see cref="BaseAmmoItem"/>.
    /// </summary>
    public static new Dictionary<BaseAmmoItem, AmmoItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="AmmoItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<AmmoItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the ammo item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseAmmoItem"/> was not null.
    /// </summary>
    /// <param name="baseAmmoItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseAmmoItem))]
    public static AmmoItem? Get(BaseAmmoItem? baseAmmoItem)
    {
        if (baseAmmoItem == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseAmmoItem, out AmmoItem item) ? item : (AmmoItem)CreateItemWrapper(baseAmmoItem);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseAmmoItem">The base <see cref="BaseAmmoItem"/> object.</param>
    internal AmmoItem(BaseAmmoItem baseAmmoItem)
        : base(baseAmmoItem)
    {
        Base = baseAmmoItem;

        if (CanCache)
        {
            Dictionary.Add(baseAmmoItem, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseAmmoItem"/> object.
    /// </summary>
    public new BaseAmmoItem Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
