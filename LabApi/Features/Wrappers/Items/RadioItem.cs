using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseRadioItem = InventorySystem.Items.Radio.RadioItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseRadioItem"/>.
/// </summary>
public class RadioItem : Item
{
    /// <summary>
    /// Contains all the cached radio items, accessible through their <see cref="BaseRadioItem"/>.
    /// </summary>
    public new static Dictionary<BaseRadioItem, RadioItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="RadioItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<RadioItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseRadioItem">The base <see cref="BaseRadioItem"/> object.</param>
    internal RadioItem(BaseRadioItem baseRadioItem)
        : base(baseRadioItem)
    {
        Dictionary.Add(baseRadioItem, this);
        Base = baseRadioItem;
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
    /// The base <see cref="BaseRadioItem"/> object.
    /// </summary>
    public new BaseRadioItem Base { get; }

    /// <summary>
    /// Gets the radio item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseRadioItem"/> was not null.
    /// </summary>
    /// <param name="baseRadioItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseRadioItem))]
    public static RadioItem? Get(BaseRadioItem? baseRadioItem)
    {
        if (baseRadioItem == null)
            return null;

        return Dictionary.TryGetValue(baseRadioItem, out RadioItem item) ? item : (RadioItem)CreateItemWrapper(baseRadioItem);
    }
}
