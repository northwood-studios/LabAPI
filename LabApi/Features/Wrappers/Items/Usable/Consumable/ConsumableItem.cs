using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Consumable"/>.
/// </summary>
public class ConsumableItem : UsableItem
{
    /// <summary>
    /// Contains all the cached consumable items, accessible through their <see cref="Consumable"/>.
    /// </summary>
    public static new Dictionary<Consumable, ConsumableItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ConsumableItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<ConsumableItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the consumable item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Consumable"/> was not null.
    /// </summary>
    /// <param name="consumable">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(consumable))]
    public static ConsumableItem? Get(Consumable? consumable)
    {
        if (consumable == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(consumable, out ConsumableItem item) ? item : (ConsumableItem)CreateItemWrapper(consumable);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="consumable">The base <see cref="Consumable"/> object.</param>
    internal ConsumableItem(Consumable consumable)
        : base(consumable)
    {
        Base = consumable;

        if (CanCache)
        {
            Dictionary.Add(consumable, this);
        }
    }

    /// <summary>
    /// The base <see cref="Consumable"/> object.
    /// </summary>
    public new Consumable Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
