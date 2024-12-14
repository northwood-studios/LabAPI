using Generators;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Armor;
using InventorySystem.Items.Coin;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Usables;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ItemBase">object</see>.
///
/// <para>Not to be confused with <see cref="Pickup">item pickup.</see></para>
/// </summary>
public class Item
{
    /// <summary>
    /// Contains all the handlers for constructing wrappers for the associated base game types.
    /// </summary>
    private static Dictionary<Type, Func<ItemBase, Item>> typeWrappers = [];

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
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="itemBase">The <see cref="Base"/> of the item.</param>
    protected Item(ItemBase itemBase)
    {
        Dictionary.Add(itemBase, this);
        Base = itemBase;
    }

    /// <summary>
    /// An internal virtual method to signal to derived implementations to uncache when the base object is destroyed.
    /// </summary>
    internal virtual void OnRemove()
    {
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

        InventoryExtensions.OnItemAdded += (_, item, _) => AddItem(item);
        InventoryExtensions.OnItemRemoved += (_, item, _) => RemoveItem(item);

        Register<ItemBase>(x => new Item(x));

        Register<Consumable>(x => new ConsumableItem(x));
        Register<Scp500>(x => new Scp500Item(x));
        Register<InventorySystem.Items.Usables.Scp1853Item>(x => new Scp1853Item(x));
        Register<Painkillers>(x => new PainkillersItem(x));
        Register<Adrenaline>(x => new AdrenalineItem(x));
        Register<Medkit>(x => new MedkitItem(x));
        Register<Scp207>(x => new Scp207Item(x));
        Register<AntiScp207>(x => new AntiScp207Item(x));

        Register<InventorySystem.Items.Usables.UsableItem>(x => new UsableItem(x));
        Register<InventorySystem.Items.Usables.Scp1576.Scp1576Item>(x => new Scp1576Item(x));
        Register<InventorySystem.Items.Usables.Scp330.Scp330Bag>(x => new Scp330Item(x));
        Register<InventorySystem.Items.Usables.Scp244.Scp244Item>(x => new Scp244Item(x));
        Register<Scp268>(x => new Scp268Item(x));

        Register<Firearm>(x => new FirearmItem(x));
        Register<ParticleDisruptor>(x => new ParticleDisruptorItem(x));
        Register<InventorySystem.Items.Jailbird.JailbirdItem>(x => new JailbirdItem(x));
        Register<Coin>(x => new CoinItem(x));

        Register<InventorySystem.Items.ToggleableLights.ToggleableLightItemBase>(x => new LightItem(x));
        Register<InventorySystem.Items.ToggleableLights.Flashlight.FlashlightItem>(x => new FlashlightItem(x));
        Register<InventorySystem.Items.ToggleableLights.Lantern.LanternItem>(x => new LanternItem(x));

        Register<InventorySystem.Items.Radio.RadioItem>(x => new RadioItem(x));
        Register<InventorySystem.Items.Firearms.Ammo.AmmoItem>(x => new AmmoItem(x));
        Register<BodyArmor>(x => new BodyArmorItem(x));
        Register<InventorySystem.Items.ThrowableProjectiles.ThrowableItem>(x => new ThrowableItem(x));
        Register<InventorySystem.Items.Keycards.KeycardItem>(x => new KeycardItem(x));
        Register<InventorySystem.Items.MicroHID.MicroHIDItem>(x => new MicroHIDItem(x));
    }

    /// <summary>
    /// Gets the item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="ItemBase"/> was not null.
    /// </summary>
    /// <param name="itemBase">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(itemBase))]
    public static Item? Get(ItemBase? itemBase)
    {
        if (itemBase == null)
            return null;

        return Dictionary.TryGetValue(itemBase, out Item item) ? item : CreateItemWrapper(itemBase);
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
        return item != null;
    }

    /// <summary>
    /// Gets the item wrapper or null from <see cref="SerialsCache"/>.
    /// </summary>
    /// <param name="serial">Serial of the item.</param>
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
    /// Gets a pooled list of items having the same <see cref="ItemType"/>.
    /// </summary>
    /// <param name="type">Target type.</param>
    /// <returns>A List of items.</returns>
    public static List<Item> GetAll(ItemType type)
    {
        List<Item> list = ListPool<Item>.Shared.Rent();
        list.AddRange(List.Where(n => n.Type == type));
        return list;
    }

    /// <summary>
    /// Gets a pooled list of items having the same <see cref="ItemCategory"/>.
    /// </summary>
    /// <param name="category">Target category.</param>
    /// <returns>A List of items.</returns>
    public static List<Item> GetAll(ItemCategory category)
    {
        List<Item> list = ListPool<Item>.Shared.Rent();
        list.AddRange(List.Where(n => n.Category == category));
        return list;
    }

    /// <summary>
    /// Creates a new wrapper from the base item object.
    /// </summary>
    /// <param name="item">The base object.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static Item CreateItemWrapper(ItemBase item)
    {
        return typeWrappers[item.GetType()].Invoke(item);
    }

    /// <summary>
    /// A private method to handle the creation of new items in the server.
    /// </summary>
    /// <param name="item">The created <see cref="ItemBase"/> instance.</param>
    private static void AddItem(ItemBase item)
    {
        if (!Dictionary.ContainsKey(item))
            _ = CreateItemWrapper(item);
    }

    /// <summary>
    /// A private method to handle the removal of items from the server.
    /// </summary>
    /// <param name="itemBase">The to be destroyed <see cref="ItemBase"/> instance.</param>
    private static void RemoveItem(ItemBase itemBase)
    {
        SerialsCache.Remove(itemBase.ItemSerial);
        if (Dictionary.TryGetValue(itemBase, out Item item))
        {
            Dictionary.Remove(itemBase);
            item.OnRemove();
        }
    }

    /// <summary>
    /// A private method to handle the addition of wrapper handlers.
    /// </summary>
    /// <typeparam name="T">The derived base game type to handle.</typeparam>
    /// <param name="constructor">A handler to construct the wrapper with the base game instance.</param>
    private static void Register<T>(Func<T, Item> constructor) where T : ItemBase
    {
        typeWrappers.Add(typeof(T), x => constructor((T)x));
    }
}