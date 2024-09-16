using Generators;
using InventorySystem;
using InventorySystem.Items;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LabApi.Features.Wrappers;

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
    /// The <see cref="ItemBase"/> of the item.
    /// </summary>
    public ItemBase ItemBase { get; }

    /// <summary>
    /// Gets the item's <see cref="ItemType"/>.
    /// </summary>
    public ItemType Type => ItemBase.ItemTypeId;

    /// <summary>
    /// Gets or sets the item's <see cref="ItemCategory"/>.
    /// </summary>
    public ItemCategory Category
    {
        get => ItemBase.Category;
        set => ItemBase.Category = value;
    }

    /// <summary>
    /// Gets or sets the item's <see cref="ItemTierFlags"/>.
    /// </summary>
    public ItemTierFlags TierFlags
    {
        get => ItemBase.TierFlags;
        set => ItemBase.TierFlags = value;
    }

    /// <summary>
    /// Gets or sets the item's <see cref="ItemThrowSettings"/>.
    /// </summary>
    public ItemThrowSettings ThrowSettings
    {
        get => ItemBase.ThrowSettings;
        set => ItemBase.ThrowSettings = value;
    }

    /// <summary>
    /// Gets the item's current owner.
    /// </summary>
    public Player? CurrentOwner => ItemBase.Owner == null ? null : Player.Get(ItemBase.Owner);

    /// <summary>
    /// Gets the item's serial.
    /// </summary>
    public ushort Serial => ItemBase.ItemSerial;

    /// <summary>
    /// Gets the item's weight.
    /// </summary>
    public float Weight => ItemBase.Weight;

    /// <summary>
    /// Gets the item's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => ItemBase.transform;

    /// <summary>
    /// Gets the item's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => ItemBase.gameObject;

    /// <summary>
    /// Gets or sets the item's <see cref="UnityEngine.Vector3">position</see>.
    /// </summary>
    public Vector3 Position
    {
        get => Transform.position;
        set => Transform.position = value;
    }

    /// <summary>
    /// Gets or sets the item's <see cref="UnityEngine.Quaternion">rotation</see>.
    /// </summary>
    public Quaternion Rotation
    {
        get => Transform.rotation;
        set => Transform.rotation = value;
    }

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
    /// Drops this item from player's inventory
    /// </summary>
    public void DropItem() => ItemBase.ServerDropItem();

    /// <summary>
    /// Initializes the <see cref="Item"/> class to subscribe to <see cref="InventoryExtensions"/> events and handle the item caching.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();

        InventoryExtensions.OnItemAdded += (_, item, _) => _ = new Item(item);
        InventoryExtensions.OnItemRemoved += (_, item, _) => Dictionary.Remove(item);
    }

    /// <summary>
    /// Gets the item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="itemBase">The <see cref="ItemBase"/> of the item.</param>
    /// <returns>The requested item.</returns>
    public static Item Get(ItemBase itemBase) => Dictionary.TryGetValue(itemBase, out Item item) ? item : new Item(itemBase);

    /// <summary>
    /// Gets the item wrapper or null from the <see cref="Dictionary"/> based on provided serial number.
    /// </summary>
    /// <param name="serial">The serial number of the item.</param>
    /// <returns>The requested item.</returns>
    public static Item? Get(ushort serial) => List.FirstOrDefault(item => item.Serial == serial);

    /// <summary>
    /// Gets a pooled list of items having the same <see cref="ItemType"/>
    /// </summary>
    /// <param name="type">Target type</param>
    /// <returns>A List of items</returns>
    public static List<Item> GetAll(ItemType type)
    {
        List<Item> list = ListPool<Item>.Shared.Rent();
        list.AddRange(List.Where(n => n.Type == type));
        return list;
    }

    /// <summary>
    /// Gets a pooled list of items having the same <see cref="ItemCategory"/>
    /// </summary>
    /// <param name="category">Target category</param>
    /// <returns>A List of items</returns>
    public static List<Item> GetAll(ItemCategory category)
    {
        List<Item> list = ListPool<Item>.Shared.Rent();
        list.AddRange(List.Where(n => n.Category == category));
        return list;
    }
}