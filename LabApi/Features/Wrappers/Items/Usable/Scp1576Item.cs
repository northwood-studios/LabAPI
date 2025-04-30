using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BaseScp1576Item = InventorySystem.Items.Usables.Scp1576.Scp1576Item;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp1576Item"/>.
/// </summary>
public class Scp1576Item : UsableItem
{
    /// <summary>
    /// Contains all the cached SCP-1576 items, accessible through their <see cref="BaseScp1576Item"/>.
    /// </summary>
    public new static Dictionary<BaseScp1576Item, Scp1576Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp1576Item"/>.
    /// </summary>
    public new static IReadOnlyCollection<Scp1576Item> List => Dictionary.Values;

    /// <summary>
    /// The set of players who are able to transmit their voice to spectators using Scp1576.
    /// </summary>
    public static IEnumerable<Player> TransmitterList => BaseScp1576Item.ValidatedTransmitters.Select(x => Player.Get(x));

    /// <summary>
    /// The set of players who are able to receive hear spectators talking through Scp1576.
    /// Includes people in the audible range of a player using Scp1567.
    /// </summary>
    public static IEnumerable<Player> ReceiverList => BaseScp1576Item.ValidatedReceivers.Select(x => Player.Get(x));

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp1576Item">The base <see cref="BaseScp1576Item"/> object.</param>
    internal Scp1576Item(BaseScp1576Item baseScp1576Item)
        : base(baseScp1576Item)
    {
        Base = baseScp1576Item;

        if (CanCache)
            Dictionary.Add(baseScp1576Item, this);
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
    /// The base <see cref="BaseScp1576Item"/> object.
    /// </summary>
    public new BaseScp1576Item Base { get; }

    /// <summary>
    /// Gets the SCP-1576 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp1576Item"/> was not null.
    /// </summary>
    /// <param name="baseScp1576Item">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseScp1576Item))]
    public static Scp1576Item? Get(BaseScp1576Item? baseScp1576Item)
    {
        if (baseScp1576Item == null)
            return null;

        return Dictionary.TryGetValue(baseScp1576Item, out Scp1576Item item) ? item : (Scp1576Item)CreateItemWrapper(baseScp1576Item);
    }
}
