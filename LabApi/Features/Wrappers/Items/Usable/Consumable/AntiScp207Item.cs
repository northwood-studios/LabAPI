using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="AntiScp207"/>.
/// </summary>
public class AntiScp207Item : ConsumableItem
{
    /// <summary>
    /// Contains all the cached anti SCP-207 items, accessible through their <see cref="AntiScp207"/>.
    /// </summary>
    public new static Dictionary<AntiScp207, AntiScp207Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="AntiScp207Item"/>.
    /// </summary>
    public new static IReadOnlyCollection<AntiScp207Item> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="antiScp207">The base <see cref="AntiScp207"/> object.</param>
    internal AntiScp207Item(AntiScp207 antiScp207)
        : base(antiScp207)
    {
        Base = antiScp207;

        if (CanCache)
            Dictionary.Add(antiScp207, this);
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
    /// The base <see cref="AntiScp207"/> object.
    /// </summary>
    public new AntiScp207 Base { get; }

    /// <summary>
    /// Gets the anti SPC-207 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="AntiScp207"/> was not null.
    /// </summary>
    /// <param name="antiScp207">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(antiScp207))]
    public static AntiScp207Item? Get(AntiScp207? antiScp207)
    {
        if (antiScp207 == null)
            return null;

        return Dictionary.TryGetValue(antiScp207, out AntiScp207Item item) ? item : (AntiScp207Item)CreateItemWrapper(antiScp207);
    }
}
