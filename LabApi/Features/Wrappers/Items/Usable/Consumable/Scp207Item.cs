using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Scp207"/>.
/// </summary>
public class Scp207Item : ConsumableItem
{
    /// <summary>
    /// Contains all the cached SCP-207 items, accessible through their <see cref="Scp207"/>.
    /// </summary>
    public new static Dictionary<Scp207, Scp207Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp207Item"/>.
    /// </summary>
    public new static IReadOnlyCollection<Scp207Item> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="scp207">The base <see cref="Scp207"/> object.</param>
    internal Scp207Item(Scp207 scp207)
        : base(scp207)
    {
        Dictionary.Add(scp207, this);
        Base = scp207;
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
    /// The base <see cref="Scp207"/> object.
    /// </summary>
    public new Scp207 Base { get; }

    /// <summary>
    /// Gets the SCP-207 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Scp207"/> was not null.
    /// </summary>
    /// <param name="scp207">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(scp207))]
    public static Scp207Item? Get(Scp207? scp207)
    {
        if (scp207 == null)
            return null;

        return Dictionary.TryGetValue(scp207, out Scp207Item item) ? item : (Scp207Item)CreateItemWrapper(scp207);
    }
}
