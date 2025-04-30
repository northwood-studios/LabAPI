using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp1853Item = InventorySystem.Items.Usables.Scp1853Item;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp1853Item"/>.
/// </summary>
public class Scp1853Item : ConsumableItem
{
    /// <summary>
    /// Contains all the cached SCP-1853 items, accessible through their <see cref="BaseScp1853Item"/>.
    /// </summary>
    public new static Dictionary<BaseScp1853Item, Scp1853Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp1853Item"/>.
    /// </summary>
    public new static IReadOnlyCollection<Scp1853Item> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp1853Item">The base <see cref="BaseScp1853Item"/> object.</param>
    internal Scp1853Item(BaseScp1853Item baseScp1853Item)
        : base(baseScp1853Item)
    {
        Base = baseScp1853Item;

        if (CanCache)
            Dictionary.Add(baseScp1853Item, this);
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
    /// The base <see cref="BaseScp1853Item"/> object.
    /// </summary>
    public new BaseScp1853Item Base { get; }

    /// <summary>
    /// Gets the SCP-1853 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp1853Item"/> was not null.
    /// </summary>
    /// <param name="baseScp1853Item">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseScp1853Item))]
    public static Scp1853Item? Get(BaseScp1853Item? baseScp1853Item)
    {
        if (baseScp1853Item == null)
            return null;

        return Dictionary.TryGetValue(baseScp1853Item, out Scp1853Item item) ? item : (Scp1853Item)CreateItemWrapper(baseScp1853Item);
    }
}
