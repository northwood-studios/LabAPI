using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Scp268"/>.
/// </summary>
public class Scp268Item : UsableItem
{
    /// <summary>
    /// Contains all the cached SCP-268 items, accessible through their <see cref="Scp268"/>.
    /// </summary>
    public new static Dictionary<Scp268, Scp268Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp268Item"/>.
    /// </summary>
    public new static IReadOnlyCollection<Scp268Item> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="scp268">The base <see cref="Scp268"/> object.</param>
    internal Scp268Item(Scp268 scp268)
        : base(scp268)
    {
        Dictionary.Add(scp268, this);
        Base = scp268;
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
    /// The base <see cref="Scp268"/> object.
    /// </summary>
    public new Scp268 Base { get; }

    /// <summary>
    /// Gets the SCP-268 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Scp268"/> was not null.
    /// </summary>
    /// <param name="scp268">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(scp268))]
    public static Scp268Item? Get(Scp268? scp268)
    {
        if (scp268 == null)
            return null;

        return Dictionary.TryGetValue(scp268, out Scp268Item item) ? item : (Scp268Item)CreateItemWrapper(scp268);
    }
}
