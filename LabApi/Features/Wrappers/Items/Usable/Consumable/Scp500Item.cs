using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Scp500"/>.
/// </summary>
public class Scp500Item : ConsumableItem
{
    /// <summary>
    /// Contains all the cached SCP-500 items, accessible through their <see cref="Scp500"/>.
    /// </summary>
    public static new Dictionary<Scp500, Scp500Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp500Item"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp500Item> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-500 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Scp500"/> was not null.
    /// </summary>
    /// <param name="scp500">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(scp500))]
    public static Scp500Item? Get(Scp500? scp500)
    {
        if (scp500 == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(scp500, out Scp500Item item) ? item : (Scp500Item)CreateItemWrapper(scp500);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="scp500">The base <see cref="Scp500"/> object.</param>
    internal Scp500Item(Scp500 scp500)
        : base(scp500)
    {
        Base = scp500;

        if (CanCache)
        {
            Dictionary.Add(scp500, this);
        }
    }

    /// <summary>
    /// The base <see cref="Scp500"/> object.
    /// </summary>
    public new Scp500 Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
