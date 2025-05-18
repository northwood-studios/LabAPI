using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Medkit"/>.
/// </summary>
public class MedkitItem : ConsumableItem
{
    /// <summary>
    /// Contains all the cached medkit items, accessible through their <see cref="Medkit"/>.
    /// </summary>
    public new static Dictionary<Medkit, MedkitItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="MedkitItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<MedkitItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="medkit">The base <see cref="Medkit"/> object.</param>
    internal MedkitItem(Medkit medkit)
        : base(medkit)
    {
        Base = medkit;

        if (CanCache)
            Dictionary.Add(medkit, this);
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
    /// The base <see cref="Medkit"/> object.
    /// </summary>
    public new Medkit Base { get; }

    /// <summary>
    /// Gets the medkit item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Medkit"/> was not null.
    /// </summary>
    /// <param name="medkit">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(medkit))]
    public static MedkitItem? Get(Medkit? medkit)
    {
        if (medkit == null)
            return null;

        return Dictionary.TryGetValue(medkit, out MedkitItem item) ? item : (MedkitItem)CreateItemWrapper(medkit);
    }
}