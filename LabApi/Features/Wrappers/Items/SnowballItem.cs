using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseSnowballItem = InventorySystem.Items.ThrowableProjectiles.SnowballItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseSnowballItem"/>.
/// </summary>
public class SnowballItem : ThrowableItem
{
    /// <summary>
    /// Contains all the cached snowball items, accessible through their <see cref="BaseSnowballItem"/>.
    /// </summary>
    public static new Dictionary<BaseSnowballItem, SnowballItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="SnowballItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<SnowballItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the snowball item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseSnowballItem"/> was not null.
    /// </summary>
    /// <param name="baseSnowballItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseSnowballItem))]
    public static SnowballItem? Get(BaseSnowballItem? baseSnowballItem)
    {
        if (baseSnowballItem == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseSnowballItem, out SnowballItem item) ? item : (SnowballItem)CreateItemWrapper(baseSnowballItem);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseThrowableItem">The base <see cref="BaseSnowballItem"/> object.</param>
    internal SnowballItem(BaseSnowballItem baseThrowableItem)
        : base(baseThrowableItem)
    {
        Base = baseThrowableItem;

        if (CanCache)
        {
            Dictionary.Add(baseThrowableItem, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseSnowballItem"/> object.
    /// </summary>
    public new BaseSnowballItem Base { get; }

    /// <inheritdoc/>
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
