using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Adrenaline"/>.
/// </summary>
public class AdrenalineItem : ConsumableItem
{
    /// <summary>
    /// Contains all the cached adrenaline items, accessible through their <see cref="Adrenaline"/>.
    /// </summary>
    public new static Dictionary<Adrenaline, AdrenalineItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="AdrenalineItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<AdrenalineItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="adrenaline">The base <see cref="Adrenaline"/> object.</param>
    internal AdrenalineItem(Adrenaline adrenaline)
        : base(adrenaline)
    {
        Base = adrenaline;

        if (CanCache)
            Dictionary.Add(adrenaline, this);
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
    /// The base <see cref="Adrenaline"/> object.
    /// </summary>
    public new Adrenaline Base { get; }

    /// <summary>
    /// Gets the adrenaline item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Adrenaline"/> was not null.
    /// </summary>
    /// <param name="adrenaline">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(adrenaline))]
    public static AdrenalineItem? Get(Adrenaline? adrenaline)
    {
        if (adrenaline == null)
            return null;

        return Dictionary.TryGetValue(adrenaline, out AdrenalineItem item) ? item : (AdrenalineItem)CreateItemWrapper(adrenaline);
    }
}