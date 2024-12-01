using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseThrowableItem"/>.
/// </summary>
public class ThrowableItem : Item
{
    /// <summary>
    /// Contains all the cached throwable items, accessible through their <see cref="BaseThrowableItem"/>.
    /// </summary>
    public new static Dictionary<BaseThrowableItem, ThrowableItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ThrowableItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<ThrowableItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseThrowableItem">The base <see cref="BaseThrowableItem"/> object.</param>
    internal ThrowableItem(BaseThrowableItem baseThrowableItem)
        : base(baseThrowableItem)
    {
        Dictionary.Add(baseThrowableItem, this);
        Base = baseThrowableItem;
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
    /// The base <see cref="BaseThrowableItem"/> object.
    /// </summary>
    public new BaseThrowableItem Base { get; }

    /// <summary>
    /// Gets the throwable item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseThrowableItem"/> was not null.
    /// </summary>
    /// <param name="baseThrowableItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseThrowableItem))]
    public static ThrowableItem? Get(BaseThrowableItem? baseThrowableItem)
    {
        if (baseThrowableItem == null)
            return null;

        return Dictionary.TryGetValue(baseThrowableItem, out ThrowableItem item) ? item : (ThrowableItem)CreateItemWrapper(baseThrowableItem);
    }
}
