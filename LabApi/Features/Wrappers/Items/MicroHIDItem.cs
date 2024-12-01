using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseMicroHIDItem = InventorySystem.Items.MicroHID.MicroHIDItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseMicroHIDItem"/>.
/// </summary>
public class MicroHIDItem : Item
{
    /// <summary>
    /// Contains all the cached micro hid items, accessible through their <see cref="BaseMicroHIDItem"/>.
    /// </summary>
    public new static Dictionary<BaseMicroHIDItem, MicroHIDItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="MicroHIDItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<MicroHIDItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseMicroHIDItem">The base <see cref="BaseMicroHIDItem"/> object.</param>
    internal MicroHIDItem(BaseMicroHIDItem baseMicroHIDItem)
        : base(baseMicroHIDItem)
    {
        Dictionary.Add(baseMicroHIDItem, this);
        Base = baseMicroHIDItem;
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
    /// The base <see cref="BaseMicroHIDItem"/> object.
    /// </summary>
    public new BaseMicroHIDItem Base { get; }

    /// <summary>
    /// Gets the micro hid item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseMicroHIDItem"/> was not null.
    /// </summary>
    /// <param name="baseMicroHIDItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseMicroHIDItem))]
    public static MicroHIDItem? Get(BaseMicroHIDItem? baseMicroHIDItem)
    {
        if (baseMicroHIDItem == null)
            return null;

        return Dictionary.TryGetValue(baseMicroHIDItem, out MicroHIDItem item) ? item : (MicroHIDItem)CreateItemWrapper(baseMicroHIDItem);
    }
}
