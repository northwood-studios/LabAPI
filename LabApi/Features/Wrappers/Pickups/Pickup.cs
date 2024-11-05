using InventorySystem.Items;
using InventorySystem;
using InventorySystem.Items.Pickups;
using LabApi.Loader.Features.Misc;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ItemPickupBase">item pickups</see>.
///
/// <para>Not to be confused with <see cref="Item">regular items</see>.</para>
/// </summary>
public class Pickup
{
    /// <summary>
    /// Initializes the <see cref="Pickup"/> class.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        ItemPickupBase.OnPickupAdded += (pickup) => _ = AddPickup(pickup);
        ItemPickupBase.OnPickupDestroyed += RemovePickup;
    }

    /// <summary>
    /// Contains all the cached items that have a none zero serial, accessible through their serial.
    /// </summary>
    /// <remarks>
    /// Item pickups spawned by the map do not have a serial until they are unlocked so may not be cached here.
    /// Use <see cref="Dictionary"/> or <see cref="List"/> instead if you need all item pickups.
    /// </remarks>
    public static Dictionary<ushort, Pickup> SerialCache { get; } = [];

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
        Base = itemPickupBase;
        
        if (itemPickupBase.Info.Serial == 0)
            itemPickupBase.OnInfoChanged += OnPickupInfoChanged;
    }

    /// <summary>
    /// The <see cref="ItemPickupBase"/> of the pickup.
    /// </summary>
    public ItemPickupBase Base { get; }

    /// <summary>
    /// Gets the pickup's <see cref="ItemType"/>.
    /// </summary>
    public ItemType Type => Base.Info.ItemId;

    /// <summary>
    /// Gets the pickup's previous owner <see cref="Player"/>.
    /// </summary>
    public Player? LastOwner => Base.PreviousOwner.Hub != null ? Player.Get(Base.PreviousOwner.Hub) : null;

    /// <summary>
    /// Gets the pickup's serial.
    /// </summary>
    public ushort Serial => Base.Info.Serial;

    /// <summary>
    /// Gets or sets the pickup's weight.
    /// </summary>
    public float Weight
    {
        get => Base.Info.WeightKg;
        set => Base.NetworkInfo = Base.Info with
        {
            WeightKg = value
        };
    }

    /// <summary>
    /// Gets or sets whether or not the pickup is locked.
    /// </summary>
    public bool IsLocked
    {
        get => Base.Info.Locked;
        set => Base.NetworkInfo = Base.Info with
        {
            Locked = value
        };
    }

    /// <summary>
    /// Gets or sets whether or not the pickup is in use.
    /// </summary>
    public bool IsInUse
    {
        get => Base.Info.InUse;
        set => Base.NetworkInfo = Base.Info with
        {
            InUse = value
        };
    }

    /// <summary>
    /// Gets the <see cref="Wrappers.Room"/> at the pickup's current position.
    /// </summary>
    public Room? Room => Wrappers.Room.GetRoomAtPosition(Position);

    /// <summary>
    /// Gets the pickup's <see cref="InventorySystem.Items.Pickups.PickupStandardPhysics"/>.
    /// </summary>
    /// <remarks>
    /// Will be null if the <see cref="PickupPhysicsModule"/> is not a <see cref="InventorySystem.Items.Pickups.PickupStandardPhysics"/> e.g. when SCP018 it is in its "Activated" state and uses an alternate physics module.
    /// Use <see cref="PhysicsModule"/> instead for those cases.
    /// </remarks>
    public PickupStandardPhysics? PickupStandardPhysics => Base.PhysicsModule as PickupStandardPhysics;

    /// <summary>
    /// Gets the pickup's <see cref="PickupPhysicsModule"/>.
    /// </summary>
    public PickupPhysicsModule PhysicsModule => Base.PhysicsModule;

    /// <summary>
    /// Gets the pickup's <see cref="UnityEngine.Rigidbody"/>.
    /// </summary>
    /// <remarks>
    /// Null if <see cref="PickupStandardPhysics"/> is null.
    /// </remarks>
    public Rigidbody? Rigidbody => PickupStandardPhysics?.Rb;

    /// <summary>
    /// Gets the pickup's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the pickup's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets or sets the pickup's position.
    /// </summary>
    public Vector3 Position
    {
        get => Base.Position;
        set => Base.Position = value;
    }

    /// <summary>
    /// Gets or sets the pickup's rotation.
    /// </summary>
    public Quaternion Rotation
    {
        get => Base.Rotation;
        set => Base.Rotation = value;
    }

    /// <summary>
    /// Gets whether the pickup was destroyed.
    /// </summary>
    public bool IsDestroyed => GameObject == null;

    /// <summary>
    /// Gets the pickup's <see cref="ItemCategory"/>.
    /// </summary>
    public ItemCategory Category => InventoryItemLoader.AvailableItems.TryGetValue(Type, out var itemBase) ? itemBase.Category : ItemCategory.None;

    /// <summary>
    /// Gets the pickup's <see cref="ItemTierFlags"/>.
    /// </summary>
    public ItemTierFlags Tier => InventoryItemLoader.AvailableItems.TryGetValue(Type, out var itemBase) ? itemBase.TierFlags : ItemTierFlags.Common;

    /// <summary>
    /// Gets the pickup wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> of the pickup.</param>
    /// <returns>The requested item <see cref="Pickup">.</returns>
    public static Pickup Get(ItemPickupBase itemPickupBase) =>
                Dictionary.TryGetValue(itemPickupBase, out Pickup pickup) ? pickup : new Pickup(itemPickupBase);

    /// <summary>
    /// Gets the pickup wrapper from the <see cref="SerialCache"/>.
    /// </summary>
    /// <param name="itemSerial">The serial of the pickup.</param>
    /// <returns>The requested item <see cref="Pickup"> or null if it doesn't exist.</returns>
    public static Pickup? Get(ushort itemSerial) => SerialCache.GetValueOrDefault(itemSerial);

    /// <summary>
    /// Trys to get the pickup wrapper from the <see cref="SerialCache"/>.
    /// </summary>
    /// <param name="itemSerial">The serial of the pickup.</param>
    /// <param name="pickup">The requested item <see cref="Pickup"> or null if it doesn't exist.</param>
    /// <returns>True of the pickup exists, otherwise false.</returns>
    public static bool TryGet(ushort itemSerial, [NotNullWhen(true)] out Pickup? pickup) => SerialCache.TryGetValue(itemSerial, out pickup);

    /// <summary>
    /// Creates a new <see cref="Pickup"/>.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/>.</param>
    /// <param name="position">The initial position.</param>
    /// <returns>The instantiated <see cref="Pickup"/></returns>
    /// <remarks>The pickup is only spawned on the server, to spawn the pickup for clients use <see cref="Spawn"/>.</remarks>
    public static Pickup? Create(ItemType type, Vector3 position) => Create(type, position, Quaternion.identity, Vector3.one);

    /// <summary>
    /// Creates a new <see cref="Pickup"/>.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/>.</param>
    /// <param name="position">The initial position.</param>
    /// <param name="rotation">The initial rotation.</param>
    /// <returns>The instantiated <see cref="Pickup"/></returns>
    /// <remarks>The pickup is only spawned on the server, to spawn the pickup for clients use <see cref="Spawn"/>.</remarks>
    public static Pickup? Create(ItemType type, Vector3 position, Quaternion rotation) => Create(type, position, rotation, Vector3.one);

    /// <summary>
    /// Creates a new <see cref="Pickup"/>.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/>.</param>
    /// <param name="position">The initial position.</param>
    /// <param name="rotation">The initial rotation.</param>
    /// <param name="scale">The initial scale.</param>
    /// <returns>The instantiated <see cref="Pickup"/></returns>
    /// <remarks>The pickup is only spawned on the server, to spawn the pickup for clients use <see cref="Spawn"/>.</remarks>
    public static Pickup? Create(ItemType type, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (type == ItemType.None || !InventoryItemLoader.AvailableItems.TryGetValue(type, out ItemBase itemBase))
            return null;

        ItemPickupBase newPickupBase = InventoryExtensions.ServerCreatePickup(itemBase, new PickupSyncInfo(type, itemBase.Weight), position, rotation, false);
        newPickupBase.transform.localScale = scale;
        return AddPickup(newPickupBase);
    }

    /// <summary>
    /// Spawns the pickup.
    /// </summary>
    public void Spawn()
    {
        NetworkServer.Spawn(GameObject);
    }

    /// <summary>
    /// Destroys the pickup.
    /// </summary>
    public void Destroy()
    {
        Base.DestroySelf();
    }

    /// <summary>
    /// Handles changes to the <see cref="ItemPickupBase.Info"/> that result in a serial being assigned.
    /// </summary>
    private void OnPickupInfoChanged()
    {
        if (Base.Info.Serial == 0)
            return;

        SerialCache[Base.Info.Serial] = this;
    }

    /// <summary>
    /// Removes all remaining subscriptions to the <see cref="ItemPickupBase"/> instance.
    /// </summary>
    private void UnsubscribeEvents()
    {
        Base.OnInfoChanged -= OnPickupInfoChanged;
    }

    /// <summary>
    /// Handles the addition of new or previously added <see cref="Pickup"/> wrappers.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> being added.</param>
    /// <returns>The new or previous item <see cref="Pickup"/> wrapper.</returns>
    private static Pickup AddPickup(ItemPickupBase itemPickupBase)
    {
        if (Dictionary.TryGetValue(itemPickupBase, out Pickup pickup))
            return pickup;

        Pickup newPickup = new(itemPickupBase);
        Dictionary.Add(itemPickupBase, newPickup);
        
        if (itemPickupBase.Info.Serial != 0)
            SerialCache.Add(itemPickupBase.Info.Serial, newPickup);

        return newPickup;
    }

    /// <summary>
    /// Handles the removal of a pickup from the server.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> being removed.</param>
    private static void RemovePickup(ItemPickupBase itemPickupBase)
    {
        if(Dictionary.TryGetValue(itemPickupBase, out Pickup pickup))
        {
            pickup.UnsubscribeEvents();
            Dictionary.Remove(itemPickupBase);
            if (itemPickupBase.Info.Serial != 0)
                SerialCache.Remove(itemPickupBase.Info.Serial);
        }
    }
}
