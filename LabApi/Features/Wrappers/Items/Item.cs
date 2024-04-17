using System.Collections.Generic;
using InventorySystem.Items;
using LabApi.Features.Wrappers.Pickups;

namespace LabApi.Features.Wrappers.Items;

/// <summary>
/// The wrapper representing <see cref="ItemBase">items</see>.
///
/// <para>Not to be confused with <see cref="Pickup">item pickups</see>.</para>
/// </summary>
public class Item
{
    /// <summary>
    /// Contains all the cached items, accessible through their <see cref="ItemBase"/>.
    /// </summary>
    public static Dictionary<ItemBase, Item> Dictionary { get; } = [];
    
    /// <summary>
    /// A reference to all instances of <see cref="Item"/>.
    /// </summary>
    public static IReadOnlyCollection<Item> List => Dictionary.Values;
    
    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="itemBase">The <see cref="ItemBase"/> of the item.</param>
    private Item(ItemBase itemBase)
    {
        Dictionary.Add(itemBase, this);
        ItemBase = itemBase;
    }
    
    /// <summary>
    /// The <see cref="ItemBase"/> of the item.
    /// </summary>
    public ItemBase ItemBase { get; }
    
    /// <summary>
    /// Gets the item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="itemBase">The <see cref="ItemBase"/> of the item.</param>
    /// <returns>The requested item.</returns>
    public static Item Get(ItemBase itemBase) =>
	        Dictionary.TryGetValue(itemBase, out Item item) ? item : new Item(itemBase);
}