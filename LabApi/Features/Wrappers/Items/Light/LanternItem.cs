using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseLanternItem = InventorySystem.Items.ToggleableLights.Lantern.LanternItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseLanternItem"/>.
/// </summary>
public class LanternItem : LightItem
{
    /// <summary>
    /// Contains all the cached lantern items, accessible through their <see cref="BaseLanternItem"/>.
    /// </summary>
    public new static Dictionary<BaseLanternItem, LanternItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="LanternItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<LanternItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLanternItem">The base <see cref="BaseLanternItem"/> object.</param>
    internal LanternItem(BaseLanternItem baseLanternItem)
        : base(baseLanternItem)
    {
        Base = baseLanternItem;

        if (CanCache)
            Dictionary.Add(baseLanternItem, this);
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
    /// The base <see cref="BaseLanternItem"/> object.
    /// </summary>
    public new BaseLanternItem Base { get; }

    /// <summary>
    /// Gets the lantern item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseLanternItem"/> was not null.
    /// </summary>
    /// <param name="baseLanternItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseLanternItem))]
    public static LanternItem? Get(BaseLanternItem? baseLanternItem)
    {
        if (baseLanternItem == null)
            return null;

        return Dictionary.TryGetValue(baseLanternItem, out LanternItem item) ? item : (LanternItem)CreateItemWrapper(baseLanternItem);
    }
}
