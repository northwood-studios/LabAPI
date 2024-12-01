using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseUsableItem = InventorySystem.Items.Usables.UsableItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseUsableItem"/>.
/// </summary>
public class UsableItem : Item
{
    /// <summary>
    /// Contains all the cached usable items, accessible through their <see cref="BaseUsableItem"/>.
    /// </summary>
    public new static Dictionary<BaseUsableItem, UsableItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="UsableItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<UsableItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseUsableItem">The base <see cref="BaseUsableItem"/> object.</param>
    internal UsableItem(BaseUsableItem baseUsableItem)
        :base(baseUsableItem)
    {
        Dictionary.Add(baseUsableItem, this);
        Base = baseUsableItem;
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
    /// The base <see cref="BaseUsableItem"/> object.
    /// </summary>
    public new BaseUsableItem Base { get; }

    /// <summary>
    /// Gets the usable item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseUsableItem"/> was not null.
    /// </summary>
    /// <param name="baseUsableItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseUsableItem))]
    public static UsableItem? Get(BaseUsableItem? baseUsableItem)
    {
        if (baseUsableItem == null)
            return null;

        return Dictionary.TryGetValue(baseUsableItem, out UsableItem item) ? item : (UsableItem)CreateItemWrapper(baseUsableItem);
    }
}
