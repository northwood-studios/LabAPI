using InventorySystem.Items;
using InventorySystem;
using InventorySystem.Items.Pickups;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Diagnostics.CodeAnalysis;
using Generators;
using System;
using InventorySystem.Items.ThrowableProjectiles;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ItemPickupBase">item pickups</see>.
///
/// <para>Not to be confused with <see cref="Item">regular items</see>.</para>
/// </summary>
public class Pickup
{
    /// <summary>
    /// Contains all the handlers for constructing wrappers for the associated base game types.
    /// </summary>
    private static readonly Dictionary<Type, Func<ItemPickupBase, Pickup>> typeWrappers = [];

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
    /// Initializes the <see cref="Pickup"/> class.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        ItemPickupBase.OnPickupAdded += AddPickup;
        ItemPickupBase.OnBeforePickupDestroyed += RemovePickup;

        Register<FlashbangGrenade>(n => new FlashbangProjectile(n));
        Register<ExplosionGrenade>(n => new ExplosiveGrenadeProjectile(n));
        Register((InventorySystem.Items.ThrowableProjectiles.Scp018Projectile n) => new Scp018Projectile(n));
        Register((InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile n) => new Scp2176Projectile(n));
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> of the pickup.</param>
    protected Pickup(ItemPickupBase itemPickupBase)
    {
        Base = itemPickupBase;

        Dictionary.Add(itemPickupBase, this);

        if (itemPickupBase.Info.Serial != 0)
            SerialCache[itemPickupBase.Info.Serial] = this;
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
    public Room? Room => Room.GetRoomAtPosition(Position);

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
    /// <returns>The requested item <see cref="Pickup"/>.</returns>
    [return: NotNullIfNotNull(nameof(itemPickupBase))]
    public static Pickup? Get(ItemPickupBase itemPickupBase)
    {
        if (itemPickupBase == null)
            return null;

        if (Dictionary.TryGetValue(itemPickupBase, out Pickup pickup))
            return pickup;

        return CreateItemWrapper(itemPickupBase);
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
    /// An internal virtual method to signal to derived implementations to uncache when the base object is destroyed.
    /// </summary>
    internal virtual void OnRemove()
    {
        Dictionary.Remove(Base);
        SerialCache.Remove(Serial);
    }

    /// <summary>
    /// Gets the pickup wrapper from the <see cref="SerialCache"/>.
    /// </summary>
    /// <param name="itemSerial">The serial of the pickup.</param>
    /// <returns>The requested item <see cref="Pickup"/> or null if it doesn't exist.</returns>
    public static Pickup? Get(ushort itemSerial) => SerialCache.GetValueOrDefault(itemSerial);

    /// <summary>
    /// Trys to get the pickup wrapper from the <see cref="SerialCache"/>.
    /// </summary>
    /// <param name="itemSerial">The serial of the pickup.</param>
    /// <param name="pickup">The requested item <see cref="Pickup"/> or null if it doesn't exist.</param>
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
        return Get(newPickupBase);
    }

    /// <summary>
    /// A private method to handle the creation of new hazards in the server.
    /// </summary>
    /// <param name="pickup">The created <see cref="ItemPickupBase"/> instance.</param>
    private static void AddPickup(ItemPickupBase pickup)
    {
        if (!Dictionary.ContainsKey(pickup))
            _ = CreateItemWrapper(pickup);
    }

    /// <summary>
    /// A private method to handle the removal of hazards from the server.
    /// </summary>
    /// <param name="pickup">The to be destroyed <see cref="ItemPickupBase"/> instance.</param>
    private static void RemovePickup(ItemPickupBase pickup)
    {
        if (Dictionary.TryGetValue(pickup, out Pickup item))
            item.OnRemove();
    }

    /// <summary>
    /// A private method to handle the addition of wrapper handlers.
    /// </summary>
    /// <typeparam name="T">The derived base game type to handle.</typeparam>
    /// <param name="constructor">A handler to construct the wrapper with the base game instance.</param>
    private static void Register<T>(Func<T, Pickup> constructor) where T : ItemPickupBase
    {
        typeWrappers.Add(typeof(T), x => constructor((T)x));
    }

    /// <summary>
    /// Creates a new wrapper from the base envronental hazard object.
    /// </summary>
    /// <param name="hazard">The base object.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static Pickup CreateItemWrapper(ItemPickupBase hazard)
    {
        if (typeWrappers.TryGetValue(hazard.GetType(), out Func<ItemPickupBase, Pickup> ctorFunc))
        {
            return ctorFunc(hazard);
        }

        return new Pickup(hazard); // Default for unimplemented wrappers for specific items
    }
}
