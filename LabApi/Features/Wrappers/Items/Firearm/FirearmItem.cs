using InventorySystem.Items.Firearms;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Firearm"/>.
/// </summary>
public class FirearmItem : Item
{
    /// <summary>
    /// Contains all the cached firearm items, accessible through their <see cref="Firearm"/>.
    /// </summary>
    public new static Dictionary<Firearm, FirearmItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="FirearmItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<FirearmItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="firearm">The base <see cref="Firearm"/> object.</param>
    internal FirearmItem(Firearm firearm)
        : base(firearm)
    {
        Base = firearm;

        if (CanCache)
            Dictionary.Add(firearm, this);
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
    /// The base <see cref="Firearm"/> object.
    /// </summary>
    public new Firearm Base { get; }

    /// <summary>
    /// Gets the firearm item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Firearm"/> was not null.
    /// </summary>
    /// <param name="firearm">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(firearm))]
    public static FirearmItem? Get(Firearm? firearm)
    {
        if (firearm == null)
            return null;

        return Dictionary.TryGetValue(firearm, out FirearmItem item) ? item : (FirearmItem)CreateItemWrapper(firearm);
    }
}