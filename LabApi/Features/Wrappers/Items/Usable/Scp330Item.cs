using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp330Item = InventorySystem.Items.Usables.Scp330.Scp330Bag;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp330Item"/>.
/// </summary>
public class Scp330Item : UsableItem
{
    /// <summary>
    /// Contains all the cached SCP-330 items, accessible through their <see cref="BaseScp330Item"/>.
    /// </summary>
    public new static Dictionary<BaseScp330Item, Scp330Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp330Item"/>.
    /// </summary>
    public new static IReadOnlyCollection<Scp330Item> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp330Item">The base <see cref="BaseScp330Item"/> object.</param>
    internal Scp330Item(BaseScp330Item baseScp330Item)
        : base(baseScp330Item)
    {
        Dictionary.Add(baseScp330Item, this);
        Base = baseScp330Item;
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
    /// The base <see cref="BaseScp330Item"/> object.
    /// </summary>
    public new BaseScp330Item Base { get; }

    /// <summary>
    /// Gets the SCP-330 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp330Item"/> was not null.
    /// </summary>
    /// <param name="baseSCp330Item">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseSCp330Item))]
    public static Scp330Item? Get(BaseScp330Item? baseSCp330Item)
    {
        if (baseSCp330Item == null)
            return null;

        return Dictionary.TryGetValue(baseSCp330Item, out Scp330Item item) ? item : (Scp330Item)CreateItemWrapper(baseSCp330Item);
    }
}
