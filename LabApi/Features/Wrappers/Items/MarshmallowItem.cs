using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseMarshmallowItem = InventorySystem.Items.MarshmallowMan.MarshmallowItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseMarshmallowItem"/>.
/// </summary>
public class MarshmallowItem : Item
{
    /// <summary>
    /// Contains all the cached Marshmallow items, accessible through their <see cref="BaseMarshmallowItem"/>.
    /// </summary>
    public static new Dictionary<BaseMarshmallowItem, MarshmallowItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="MarshmallowItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<MarshmallowItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-021J item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseMarshmallowItem"/> was not null.
    /// </summary>
    /// <param name="baseMarshmallowItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseMarshmallowItem))]
    public static MarshmallowItem? Get(BaseMarshmallowItem? baseMarshmallowItem)
    {
        if (baseMarshmallowItem == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseMarshmallowItem, out MarshmallowItem item) ? item : (MarshmallowItem)CreateItemWrapper(baseMarshmallowItem);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseMarshmallowItem">The base <see cref="BaseMarshmallowItem"/> object.</param>
    internal MarshmallowItem(BaseMarshmallowItem baseMarshmallowItem)
        : base(baseMarshmallowItem)
    {
        Base = baseMarshmallowItem;

        if (CanCache)
        {
            Dictionary.Add(baseMarshmallowItem, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseMarshmallowItem"/> object.
    /// </summary>
    public new BaseMarshmallowItem Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
