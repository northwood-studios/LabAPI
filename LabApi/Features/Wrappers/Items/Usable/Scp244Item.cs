using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp244Item = InventorySystem.Items.Usables.Scp244.Scp244Item;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp244Item"/>.
/// </summary>
public class Scp244Item : UsableItem
{
    /// <summary>
    /// Contains all the cached SCP-244 items, accessible through their <see cref="BaseScp244Item"/>.
    /// </summary>
    public static new Dictionary<BaseScp244Item, Scp244Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp244Item"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp244Item> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-244 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp244Item"/> was not null.
    /// </summary>
    /// <param name="baseScp244Item">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseScp244Item))]
    public static Scp244Item? Get(BaseScp244Item? baseScp244Item)
    {
        if (baseScp244Item == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseScp244Item, out Scp244Item item) ? item : (Scp244Item)CreateItemWrapper(baseScp244Item);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp244Item">The base <see cref="BaseScp244Item"/> object.</param>
    internal Scp244Item(BaseScp244Item baseScp244Item)
        : base(baseScp244Item)
    {
        Base = baseScp244Item;

        if (CanCache)
        {
            Dictionary.Add(baseScp244Item, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseScp244Item"/> object.
    /// </summary>
    public new BaseScp244Item Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
