using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseKeycardItem = InventorySystem.Items.Keycards.KeycardItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseKeycardItem"/>.
/// </summary>
public class KeycardItem : Item
{
    /// <summary>
    /// Contains all the cached keycard items, accessible through their <see cref="BaseKeycardItem"/>.
    /// </summary>
    public new static Dictionary<BaseKeycardItem, KeycardItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="KeycardItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<KeycardItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseKeycardItem">The base <see cref="BaseKeycardItem"/> object.</param>
    internal KeycardItem(BaseKeycardItem baseKeycardItem)
        : base(baseKeycardItem)
    {
        Dictionary.Add(baseKeycardItem, this);
        Base = baseKeycardItem;
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
    /// The base <see cref="BaseKeycardItem"/> object.
    /// </summary>
    public new BaseKeycardItem Base { get; }

    /// <summary>
    /// Gets the keycard item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseKeycardItem"/> was not null.
    /// </summary>
    /// <param name="baseKeycardItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseKeycardItem))]
    public static KeycardItem? Get(BaseKeycardItem? baseKeycardItem)
    {
        if (baseKeycardItem == null)
            return null;

        return Dictionary.TryGetValue(baseKeycardItem, out KeycardItem item) ? item : (KeycardItem)CreateItemWrapper(baseKeycardItem);
    }
}
