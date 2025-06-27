using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseJailbirdItem = InventorySystem.Items.Jailbird.JailbirdItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseJailbirdItem"/>.
/// </summary>
public class JailbirdItem : Item
{
    /// <summary>
    /// Contains all the cached jailbird items, accessible through their <see cref="BaseJailbirdItem"/>.
    /// </summary>
    public new static Dictionary<BaseJailbirdItem, JailbirdItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="JailbirdItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<JailbirdItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseJailbirdItem">The base <see cref="BaseJailbirdItem"/> object.</param>
    internal JailbirdItem(BaseJailbirdItem baseJailbirdItem)
        : base(baseJailbirdItem)
    {
        Base = baseJailbirdItem;

        if (CanCache)
            Dictionary.Add(baseJailbirdItem, this);
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
    /// The base <see cref="BaseJailbirdItem"/> object.
    /// </summary>
    public new BaseJailbirdItem Base { get; }

    /// <summary>
    /// Gets the number of charges performed.
    /// </summary>
    public int TotalChargesPerformed => Base.TotalChargesPerformed;

    /// <summary>
    /// Gets whether the <see cref="Item.CurrentOwner"/> is currently charging with the jailbird.
    /// </summary>
    public bool IsCharging => Base.MovementOverrideActive;

    /// <summary>
    /// Gets the jailbird item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseJailbirdItem"/> was not null.
    /// </summary>
    /// <param name="baseJailbirdItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseJailbirdItem))]
    public static JailbirdItem? Get(BaseJailbirdItem? baseJailbirdItem)
    {
        if (baseJailbirdItem == null)
            return null;

        return Dictionary.TryGetValue(baseJailbirdItem, out JailbirdItem item) ? item : (JailbirdItem)CreateItemWrapper(baseJailbirdItem);
    }
}