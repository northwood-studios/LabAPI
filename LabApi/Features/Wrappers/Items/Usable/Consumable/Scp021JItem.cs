using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp021JItem = InventorySystem.Items.Usables.Scp021J;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp021JItem"/>.
/// </summary>
public class Scp021JItem : ConsumableItem
{
    /// <summary>
    /// Contains all the cached SCP-021J items, accessible through their <see cref="BaseScp021JItem"/>.
    /// </summary>
    public static new Dictionary<BaseScp021JItem, Scp021JItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp021JItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp021JItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-021J item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp021JItem"/> was not null.
    /// </summary>
    /// <param name="baseScp021JItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseScp021JItem))]
    public static Scp021JItem? Get(BaseScp021JItem? baseScp021JItem)
    {
        if (baseScp021JItem == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseScp021JItem, out Scp021JItem item) ? item : (Scp021JItem)CreateItemWrapper(baseScp021JItem);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp021JItem">The base <see cref="BaseScp021JItem"/> object.</param>
    internal Scp021JItem(BaseScp021JItem baseScp021JItem)
        : base(baseScp021JItem)
    {
        Base = baseScp021JItem;

        if (CanCache)
        {
            Dictionary.Add(baseScp021JItem, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseScp021JItem"/> object.
    /// </summary>
    public new BaseScp021JItem Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
