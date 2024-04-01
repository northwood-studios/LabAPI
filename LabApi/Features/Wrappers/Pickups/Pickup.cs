using System.Collections.Generic;
using InventorySystem.Items.Pickups;

namespace LabApi.Features.Wrappers.Pickups;

/// <summary>
/// The wrapper representing <see cref="ItemPickupBase">item pickups</see>.
///
/// <para>Not to be confused with <see cref="Item">regular items</see>.</para>
/// </summary>
public class Pickup
{
    /// <summary>
    /// Contains all the cached item pickups, accessible through their <see cref="ItemPickupBase"/>.
    /// </summary>
    public static Dictionary<ItemPickupBase, Pickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Pickup"/>.
    /// </summary>
    public static IReadOnlyCollection<Pickup> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> of the pickup.</param>
    private Pickup(ItemPickupBase itemPickupBase)
    {
        Dictionary.Add(itemPickupBase, this);
        ItemPickupBase = itemPickupBase;
    }
    
    /// <summary>
    /// The <see cref="ItemPickupBase"/> of the pickup.
    /// </summary>
    public ItemPickupBase ItemPickupBase { get; }
    
    /// <summary>
    /// Gets the pickup wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> of the pickup.</param>
    /// <returns>The requested item pickup.</returns>
    public static Pickup Get(ItemPickupBase itemPickupBase) =>
		        Dictionary.TryGetValue(itemPickupBase, out Pickup pickup) ? pickup : new Pickup(itemPickupBase);
}