using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Painkillers"/>.
/// </summary>
public class PainkillersItem : ConsumableItem
{
    /// <summary>
    /// Contains all the cached painkiller items, accessible through their <see cref="Painkillers"/>.
    /// </summary>
    public static new Dictionary<Painkillers, PainkillersItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="PainkillersItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<PainkillersItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the painkillers item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Painkillers"/> was not null.
    /// </summary>
    /// <param name="painkillers">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(painkillers))]
    public static PainkillersItem? Get(Painkillers? painkillers)
    {
        if (painkillers == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(painkillers, out PainkillersItem item) ? item : (PainkillersItem)CreateItemWrapper(painkillers);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="painkillers">The base <see cref="Painkillers"/> object.</param>
    internal PainkillersItem(Painkillers painkillers)
        : base(painkillers)
    {
        Base = painkillers;

        if (CanCache)
        {
            Dictionary.Add(painkillers, this);
        }
    }

    /// <summary>
    /// The base <see cref="Painkillers"/> object.
    /// </summary>
    public new Painkillers Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
