using Generators;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using System;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Handlers;
using LabApi.Events.Arguments.ServerEvents;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

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
        ItemPickupBase.OnPickupAdded += AddPickup;
        ItemPickupBase.OnPickupDestroyed += RemovePickup;

        Register<FlashbangGrenade>(n => new FlashbangProjectile(n));
        Register<ExplosionGrenade>(n => new ExplosiveGrenadeProjectile(n));
        Register((InventorySystem.Items.ThrowableProjectiles.Scp018Projectile n) => new Scp018Projectile(n));
        Register((InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile n) => new Scp2176Projectile(n));

        Register<InventorySystem.Items.Firearms.Ammo.AmmoPickup>(x => new AmmoPickup(x));
        Register<InventorySystem.Items.Armor.BodyArmorPickup>(x => new BodyArmorPickup(x));
        Register<InventorySystem.Items.Firearms.FirearmPickup>(x => new FirearmPickup(x));
        Register<InventorySystem.Items.Jailbird.JailbirdPickup>(x => new JailbirdPickup(x));
        Register<InventorySystem.Items.Keycards.KeycardPickup>(x => new KeycardPickup(x));
        Register<InventorySystem.Items.MicroHID.MicroHIDPickup>(x => new MicroHIDPickup(x));
        Register<InventorySystem.Items.Radio.RadioPickup>(x => new RadioPickup(x));
        Register<InventorySystem.Items.Usables.Scp1576.Scp1576Pickup>(x => new Scp1576Pickup(x));
        Register<InventorySystem.Items.Usables.Scp244.Scp244DeployablePickup>(x => new Scp244Pickup(x));
        Register<InventorySystem.Items.Usables.Scp330.Scp330Pickup>(x => new Scp330Pickup(x));
        Register<InventorySystem.Items.ThrowableProjectiles.TimedGrenadePickup>(x => new TimedGrenadePickup(x));
        Register<CollisionDetectionPickup>(x => new Pickup(x));
    }

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
    /// The <see cref="ItemPickupBase"/> of the pickup.
    /// </summary>
    public ItemPickupBase Base { get; }

    /// <summary>
    /// The <see cref="Mirror.NetworkIdentity"/> of the pickup.
    /// </summary>
    public NetworkIdentity NetworkIdentity => Base.netIdentity;

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
    public Transform Transform { get; }

    /// <summary>
    /// Gets the pickup's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject { get; }

    /// <summary>
    /// Gets whether the pickup was destroyed.
    /// </summary>
    public bool IsDestroyed => Base == null || GameObject == null;

    /// <summary>
    /// Gets whether or not this instance is used as a prefab.
    /// </summary>
    /// <remarks>
    /// Changes made to the prefab instance will be reflected across all subsequent new instances.
    /// </remarks>
    public bool IsPrefab { get; }

    /// <summary>
    /// Gets whether the pickup is spawned on the client.
    /// </summary>
    public bool IsSpawned => NetworkIdentity.netId != 0;

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
    /// Gets the pickup's <see cref="ItemCategory"/>.
    /// </summary>
    public ItemCategory Category => InventoryItemLoader.AvailableItems.TryGetValue(Type, out var itemBase) ? itemBase.Category : ItemCategory.None;

    /// <summary>
    /// Gets the pickup's <see cref="ItemTierFlags"/>.
    /// </summary>
    public ItemTierFlags Tier => InventoryItemLoader.AvailableItems.TryGetValue(Type, out var itemBase) ? itemBase.TierFlags : ItemTierFlags.Common;

    /// <summary>
    /// Gets the <see cref="Wrappers.Room"/> at the pickup's current position.
    /// </summary>
    public Room? Room => Room.GetRoomAtPosition(Position);

    /// <summary>
    /// Gets whether the item wrapper is allowed to be cached.
    /// </summary>
    protected bool CanCache => !IsDestroyed && !IsPrefab && Base.isActiveAndEnabled;

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> of the pickup.</param>
    protected Pickup(ItemPickupBase itemPickupBase)
    {
        Base = itemPickupBase;
        GameObject = itemPickupBase.gameObject;
        Transform = itemPickupBase.transform;
        IsPrefab = InventoryItemLoader.TryGetItem(itemPickupBase.ItemId.TypeId, out ItemBase prefab) && prefab.PickupDropModel == itemPickupBase;

        if (CanCache)
        {
            Dictionary.Add(itemPickupBase, this);

            if (itemPickupBase.Info.Serial != 0)
                SerialCache[itemPickupBase.Info.Serial] = this;
        }
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

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[{GetType().Name}: Type={Type}, IsSpawned={IsSpawned}, IsDestroyed={IsDestroyed}, Position={Position}, Rotation={Rotation}, Room={Room}, IsLocked={IsLocked}, IsInUse={IsInUse}, Serial={Serial}]";
    }

    /// <summary>
    /// Gets the pickup wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> of the pickup.</param>
    /// <returns>The requested item <see cref="Pickup"/>.</returns>
    [return: NotNullIfNotNull(nameof(itemPickupBase))]
    public static Pickup? Get(ItemPickupBase? itemPickupBase)
    {
        if (itemPickupBase == null)
            return null;

        if (Dictionary.TryGetValue(itemPickupBase, out Pickup pickup))
            return pickup;

        return CreateItemWrapper(itemPickupBase);
    }

    /// <summary>
    /// Gets the pickup wrapper from the <see cref="SerialCache"/>.
    /// </summary>
    /// <param name="itemSerial">The serial of the pickup.</param>
    /// <returns>The requested item <see cref="Pickup"/> or null if it doesn't exist.</returns>
    public static Pickup? Get(ushort itemSerial) => SerialCache.GetValueOrDefault(itemSerial);

    /// <summary>
    /// Tries to get the pickup wrapper from the <see cref="SerialCache"/>.
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
    /// A private method to handle the creation of new pickups in the server.
    /// </summary>
    /// <param name="pickup">The created <see cref="ItemPickupBase"/> instance.</param>
    private static void AddPickup(ItemPickupBase pickup)
    {
        try
        {
            if (!Dictionary.ContainsKey(pickup))
            {
                Pickup wrapper = CreateItemWrapper(pickup);
                ServerEvents.OnPickupCreated(new PickupCreatedEventArgs(wrapper));
            }
        }
        catch(Exception e)
        {
            Console.Logger.Error($"Failed to handle pickup creation with error: {e}");
        }
    }

    /// <summary>
    /// A private method to handle the removal of pickups from the server.
    /// </summary>
    /// <param name="pickup">The to be destroyed <see cref="ItemPickupBase"/> instance.</param>
    private static void RemovePickup(ItemPickupBase pickup)
    {
        try
        {
            if (Dictionary.TryGetValue(pickup, out Pickup item))
                item.OnRemove();
            
            ServerEvents.OnPickupDestroyed(new PickupDestroyedEventArgs(item));
        }
        catch(Exception e)
        {
            Console.Logger.Error($"Failed to handle pickup destruction with error: {e}");
        }
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
    /// Creates a new wrapper from the base pickup object.
    /// </summary>
    /// <param name="pickupBase">The base object.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static Pickup CreateItemWrapper(ItemPickupBase pickupBase)
    {
        if (typeWrappers.TryGetValue(pickupBase.GetType(), out Func<ItemPickupBase, Pickup> ctorFunc))
            return ctorFunc(pickupBase);

        Console.Logger.Warn($"Failed to find pickup wrapper for type {pickupBase.GetType()}");
        return new Pickup(pickupBase);
    }
}
