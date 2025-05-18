using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseFlashlightItem = InventorySystem.Items.ToggleableLights.Flashlight.FlashlightItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseFlashlightItem"/>.
/// </summary>
public class FlashlightItem : LightItem
{
    /// <summary>
    /// Contains all the cached flashlight items, accessible through their <see cref="BaseFlashlightItem"/>.
    /// </summary>
    public new static Dictionary<BaseFlashlightItem, FlashlightItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="FlashlightItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<FlashlightItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseFlashlightItem">The base <see cref="BaseFlashlightItem"/> object.</param>
    internal FlashlightItem(BaseFlashlightItem baseFlashlightItem)
        : base(baseFlashlightItem)
    {
        Base = baseFlashlightItem;

        if (CanCache)
            Dictionary.Add(baseFlashlightItem, this);
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
    /// The base <see cref="BaseFlashlightItem"/> object.
    /// </summary>
    public new BaseFlashlightItem Base { get; }

    /// <summary>
    /// Gets the flashlight item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseFlashlightItem"/> was not null.
    /// </summary>
    /// <param name="baseFlashlightItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseFlashlightItem))]
    public static FlashlightItem? Get(BaseFlashlightItem? baseFlashlightItem)
    {
        if (baseFlashlightItem == null)
            return null;

        return Dictionary.TryGetValue(baseFlashlightItem, out FlashlightItem item) ? item : (FlashlightItem)CreateItemWrapper(baseFlashlightItem);
    }
}