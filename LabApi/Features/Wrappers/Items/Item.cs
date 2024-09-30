using Generators;
using InventorySystem;
using InventorySystem.Items;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ItemBase">object</see>.
///
/// <para>Not to be confused with <see cref="Pickup">item pickup.</see></para>
/// </summary>
public class Item
{
    /// <summary>
    /// Contains all the cached items, accessible through their <see cref="Base"/>.
    /// </summary>
    public static Dictionary<ItemBase, Item> Dictionary { get; } = [];

    /// <summary>
    /// Contains all cached items with their <see cref="Item.Serial"/> as a key.
    /// </summary>
    private static Dictionary<ushort, Item> SerialsCache { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Item"/>.
    /// </summary>
    public static IReadOnlyCollection<Item> List => Dictionary.Values;

    /// <summary>
    /// The <see cref="Base"/> of the item.
    /// </summary>
    public ItemBase Base { get; }

    /// <summary>
    /// Gets the item's <see cref="ItemType"/>.
    /// </summary>
    public ItemType Type => Base.ItemTypeId;

    /// <summary>
    /// Gets or sets the item's <see cref="ItemCategory"/>.
    /// <para>
    /// Category is not saved and is discarded when the item is dropped.
    /// </para>
    /// </summary>
    // TODO: Maybe do something with it? Not sure if anyone is gonna use these 3 properties anyways but you never know.
    public ItemCategory Category
    {
        get => Base.Category;
        set => Base.Category = value;
    }

    /// <summary>
    /// Gets or sets the item's <see cref="ItemTierFlags"/>.
    /// <para>
    /// Flags are not saved and are discarded when the item is dropped.
    /// </para>
    /// </summary>
    public ItemTierFlags TierFlags
    {
        get => Base.TierFlags;
        set => Base.TierFlags = value;
    }

    /// <summary>
    /// Gets or sets the item's <see cref="ItemThrowSettings"/>.
    /// <para>
    /// Settings are not saved and are discarded when the item is dropped.
    /// </para>
    /// </summary>
    public ItemThrowSettings ThrowSettings
    {
        get => Base.ThrowSettings;
        set => Base.ThrowSettings = value;
    }

    /// <summary>
    /// Gets the item's current owner.
    /// </summary>
    public Player? CurrentOwner => Base.Owner == null ? null : Player.Get(Base.Owner);

    /// <summary>
    /// Gets the item's serial.
    /// </summary>
    public ushort Serial => Base.ItemSerial;

    /// <summary>
    /// Gets the item's weight.
    /// </summary>
    public float Weight => Base.Weight;

    /// <summary>
    /// Gets the item's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the item's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

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
    /// <param name="itemBase">The <see cref="Base"/> of the item.</param>
    private Item(ItemBase itemBase)
    {
        Dictionary.Add(itemBase, this);
        Base = itemBase;
    }

    /// <summary>
    /// Drops this item from player's inventory
    /// </summary>
    public void DropItem() => Base.ServerDropItem(true);

    /// <summary>
    /// Initializes the <see cref="Item"/> class to subscribe to <see cref="InventoryExtensions"/> events and handle the item caching.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();

        InventoryExtensions.OnItemAdded += (_, item, _) => _ = new Item(item);
        InventoryExtensions.OnItemRemoved += (_, item, _) => RemoveItem(item);
    }

    /// <summary>
    /// Gets the item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="ItemBase"/> was not null.
    /// </summary>
    /// <param name="itemBase">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    public static Item? Get(ItemBase? itemBase)
    {
        if (itemBase == null)
            return null;

        return Dictionary.TryGetValue(itemBase, out Item item) ? item : new Item(itemBase);
    }

    /// <summary>
    /// Tries to get the item wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="itemBase">The <see cref="Base"/> of the item.</param>
    /// <param name="item">The requested item.</param>
    /// <returns>True if the item exists, otherwise false.</returns>
    public static bool TryGet(ItemBase? itemBase, [NotNullWhen(true)] out Item? item)
    {
        item = Get(itemBase);
        return item!= null;
    }

    /// <summary>
    /// Gets the item wrapper or null from <see cref="SerialsCache"/>.
    /// </summary>
    /// <param name="serial">Serial of the item</param>
    /// <returns>The requested item.</returns>
    public static Item? Get(ushort serial) => TryGet(serial, out Item? item) ? item : null;

    /// <summary>
    /// Gets the item wrapper or null from the <see cref="Dictionary"/> based on provided serial number.
    /// </summary>
    /// <param name="serial">The serial number of the item.</param>
    /// <param name="item">The requested item.</param>
    /// <returns>Whether the was successfully retrieved, otherwise false.</returns>
    public static bool TryGet(ushort serial, [NotNullWhen(true)] out Item? item)
    {
        item = null;
        if (SerialsCache.TryGetValue(serial, out item))
            return true;

        item = List.FirstOrDefault(x => x.Serial == serial);
        if (item == null)
            return false;

        SerialsCache[serial] = item;
        return true;
    }

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

    private static void RemoveItem(ItemBase item)
    {
        Dictionary.Remove(item);
        SerialsCache.Remove(item.ItemSerial);
    }
}