using InventorySystem.Items.ToggleableLights;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ToggleableLightItemBase"/>.
/// </summary>
public class LightItem : Item
{
    /// <summary>
    /// Contains all the cached light items, accessible through their <see cref="ToggleableLightItemBase"/>.
    /// </summary>
    public new static Dictionary<ToggleableLightItemBase, LightItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="LightItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<LightItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="toggleableLightItemBase">The base <see cref="ToggleableLightItemBase"/> object.</param>
    internal LightItem(ToggleableLightItemBase toggleableLightItemBase)
        : base(toggleableLightItemBase)
    {
        Dictionary.Add(toggleableLightItemBase, this);
        Base = toggleableLightItemBase;
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
    /// The base <see cref="ToggleableLightItemBase"/> object.
    /// </summary>
    public new ToggleableLightItemBase Base { get; }

    /// <summary>
    /// Gets the light item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="ToggleableLightItemBase"/> was not null.
    /// </summary>
    /// <param name="toggleableLight">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(toggleableLight))]
    public static LightItem? Get(ToggleableLightItemBase? toggleableLight)
    {
        if (toggleableLight == null)
            return null;

        return Dictionary.TryGetValue(toggleableLight, out LightItem item) ? item : (LightItem)CreateItemWrapper(toggleableLight);
    }
}
