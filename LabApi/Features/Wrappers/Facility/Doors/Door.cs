using Generators;
using Hazards;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Enums;
using MapGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="DoorVariant">door variants</see>, the in-game doors.
/// </summary>
public class Door
{
    [InitializeWrapper]
    internal static void Initialize()
    {
        DoorVariant.OnInstanceCreated += OnAdded;
        DoorVariant.OnInstanceRemoved += OnRemoved;

        Register<Interactables.Interobjects.BreakableDoor>(x => new BreakableDoor(x));
        Register<Interactables.Interobjects.ElevatorDoor>(x => new ElevatorDoor(x));
        Register<Timed173PryableDoor>(x => new Timed173Gate(x));
        Register<PryableDoor>(x => x.name.StartsWith("HCZ BulkDoor") ? new BulkheadDoor(x) : new Gate(x));
        Register<BasicNonInteractableDoor>(x => new NonInteractableDoor(x));
        Register<Interactables.Interobjects.CheckpointDoor>(x => new CheckpointDoor(x));
        Register<Interactables.Interobjects.DummyDoor>(x => new DummyDoor(x));
        Register<DoorVariant>(x => new Door(x));
    }

    /// <summary>
    /// Contains all the handlers for constructing wrappers for the associated base game types.
    /// </summary>
    private static readonly Dictionary<Type, Func<DoorVariant, Door>> typeWrappers = [];

    /// <summary>
    /// Contains all the <see cref="Enums.DoorName"/> values for the associated <see cref="NameTag"/>.
    /// </summary>
    private static readonly Dictionary<string, DoorName> doorNameDictionary = new()
    {
        { "LCZ_CAFE", DoorName.LczPc },
        { "LCZ_WC", DoorName.LczWc },
        { "CHECKPOINT_LCZ_A", DoorName.LczCheckpointA },
        { "CHECKPOINT_LCZ_B", DoorName.LczCheckpointB },
        { "LCZ_ARMORY", DoorName.LczArmory },
        { "173_BOTTOM", DoorName.Lcz173Bottom },
        { "173_GATE", DoorName.Lcz173Gate },
        { "173_CONNECTOR", DoorName.Lcz173Connector },
        { "173_ARMORY", DoorName.Lcz173Armory },
        { "GR18_INNER", DoorName.LczGr18Inner },
        { "GR18", DoorName.LczGr18Gate },
        { "914", DoorName.Lcz914Gate },
        { "330", DoorName.Lcz330 },
        { "330_CHAMBER", DoorName.Lcz330Chamber },
        { "079_FIRST", DoorName.Hcz079FirstGate },
        { "079_SECOND", DoorName.Hcz079SecondGate },
        { "079_ARMORY", DoorName.Hcz079Armory },
        { "096", DoorName.Hcz096 },
        { "939_CRYO", DoorName.Hcz939Cryo },
        { "HCZ_ARMORY", DoorName.HczArmory },
        { "049_ARMORY", DoorName.Hcz049Armory },
        { "HID_CHAMBER", DoorName.HczHidChamber },
        { "HID_UPPER", DoorName.HczHidUpper },
        { "HID_LOWER", DoorName.HczHidLower },
        { "NUKE_ARMORY", DoorName.HczNukeArmory },
        { "106_PRIMARY", DoorName.Hcz106Primiary },
        { "106_SECONDARY", DoorName.Hcz106Secondary },
        { "HCZ_127_LAB", DoorName.Hcz127Lab },
        { "CHECKPOINT_EZ_HCZ_A", DoorName.HczCheckpoint },
        { "INTERCOM", DoorName.EzIntercom },
        { "GATE_A", DoorName.EzGateA },
        { "GATE_B", DoorName.EzGateB },
        { "SURFACE_GATE", DoorName.SurfaceGate },
        { "SURFACE_NUKE", DoorName.SurfaceNuke },
        { "ESCAPE_PRIMARY", DoorName.SurfaceEscapePrimary },
        { "ESCAPE_SECONDARY", DoorName.SurfaceEscapeSecondary },
        { "ESCAPE_FINAL", DoorName.SurfaceEscapeFinal }
    };

    /// <summary>
    /// Contains all the cached <see cref="Door">doors</see> in the game, accessible through their <see cref="DoorVariant"/>.
    /// </summary>
    protected static Dictionary<DoorVariant, Door> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Door"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Door> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="doorVariant">The <see cref="DoorVariant"/> of the door.</param>
    protected Door(DoorVariant doorVariant)
    {
        Dictionary.Add(doorVariant, this);
        Base = doorVariant;

        if (doorVariant.TryGetComponent(out DoorNametagExtension nametag) && !string.IsNullOrEmpty(nametag.GetName))
        {
            if (doorNameDictionary.TryGetValue(nametag.GetName, out DoorName doorName))
                Type = doorName;
            else
                Logger.Warn($"Missing DoorName enum value for door name tag {nametag.GetName}");
        }
    }

    /// <summary>
    /// An internal virtual method to signal that the base object has been destroyed.
    /// </summary>
    internal virtual void OnRemove()
    {
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public DoorVariant Base { get; }

    /// <summary>
    /// Gets the <see cref="DoorName"/> of the door.
    /// </summary>
    /// <remarks>
    /// Is the enum version of <see cref="NameTag"/>.
    /// </remarks>
    public DoorName Type { get; } = DoorName.None;

    /// <summary>
    /// Gets the name tag of the door.
    /// </summary>
    /// <remarks>
    /// Is the string version of <see cref="DoorName"/>.
    /// </remarks>
    public string NameTag => Base.DoorName;

    /// <summary>
    /// Gets the rooms which have this door.
    /// </summary>
    public RoomIdentifier[] Rooms => Base.Rooms;

    /// <summary>
    /// Gets the zone in which this door is.
    /// </summary>
    public FacilityZone Zone => Rooms.FirstOrDefault()?.Zone ?? FacilityZone.Other;

    /// <summary>
    /// Gets or sets whether the door is open.
    /// </summary>
    public bool IsOpened
    {
        get => Base.TargetState;
        set => Base.NetworkTargetState = value;
    }

    /// <summary>
    /// Gets whether the door can be interacted with by a <see cref="Player"/>.
    /// </summary>
    public bool CanInteract => Base.AllowInteracting(null, 0);

    /// <summary>
    /// A value from 0.0f to 1.0f used to determine the intermediate state between fully closed and fully open.
    /// </summary>
    /// <remarks>
    /// When a door is fully closed and fully open and how to interpolate between that is implementation dependent
    /// so using this value to compare between doors of different type is not recommend.
    /// </remarks>
    public float ExactState => Base.GetExactState();

    /// <summary>
    /// Gets or sets whether the door is locked.
    /// </summary>
    public bool IsLocked
    {
        get => Base.ActiveLocks > 0;
        set => Base.ServerChangeLock(DoorLockReason.AdminCommand, value);
    }

    /// <summary>
    /// Gets the door's <see cref="DoorLockReason"/>
    /// </summary>
    public DoorLockReason LockReason => (DoorLockReason)Base.ActiveLocks;

    /// <summary>
    /// Locks the door.
    /// </summary>
    /// <param name="reason">The reason.</param>
    /// <param name="enabled">Whether the door lock reason is new.</param>
    public void Lock(DoorLockReason reason, bool enabled) => Base.ServerChangeLock(reason, enabled);

    /// <summary>
    /// Gets or sets the required <see cref="DoorPermissionFlags"/>.
    /// </summary>
    public DoorPermissionFlags Permissions
    {
        get => Base.RequiredPermissions.RequiredPermissions;
        set => Base.RequiredPermissions.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets or sets whether the door will bypass 2176.
    /// </summary>
    public bool Bypass2176
    {
        get => Base.RequiredPermissions.Bypass2176;
        set => Base.RequiredPermissions.Bypass2176 = value;
    }

    /// <summary>
    /// Gets the door's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the door's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the door's position.
    /// </summary>
    public Vector3 Position => Transform.position;

    /// <summary>
    /// Gets the door's rotation.
    /// </summary>
    public Quaternion Rotation => Transform.rotation;

    /// <summary>
    /// Plays a sound that indicates that lock bypass was denied.
    /// </summary>
    public void PlayLockBypassDeniedSound() => Base.LockBypassDenied(null, 0);

    /// <summary>
    /// Plays a sound and flashes permission denied on the panel.
    /// </summary>
    public void PlayPermissionDeniedAnimation() => Base.PermissionsDenied(null, 0);

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[{GetType().Name}: DoorName={Type}, NameTag={NameTag}, Zone={Zone}, IsOpened={IsOpened}, IsLocked={IsLocked}, Permissions={Permissions}]";
    }

    /// <summary>
    /// Gets the door wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="doorVariant">The <see cref="DoorVariant"/> of the door.</param>
    /// <returns>The requested door or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(doorVariant))]
    public static Door? Get(DoorVariant? doorVariant)
    {
        if (doorVariant == null)
            return null;

        if (Dictionary.TryGetValue(doorVariant, out Door door))
            return door;

        return CreateDoorWrapper(doorVariant);
    }

    /// <summary>
    /// Gets the door by it's nametag.
    /// </summary>
    /// <param name="nametag">The door's nametag</param>
    /// <returns>The requested door. May be null if door with provided nametag does not exist.</returns>
    public static Door? Get(string nametag)
    {
        if (!DoorNametagExtension.NamedDoors.TryGetValue(nametag, out DoorNametagExtension doorNametagExtension))
            return null;

        return Get(doorNametagExtension.TargetDoor);
    }

    /// <summary>
    /// Gets the door in specified zone.
    /// </summary>
    /// <param name="facilityZone">Target zone.</param>
    public static IEnumerable<Door> Get(FacilityZone facilityZone) =>
        List.Where(x => x.Rooms.First().Zone.Equals(facilityZone));

    /// <summary>
    /// Gets the door in specified room.
    /// </summary>
    /// <param name="roomId">Target room wrapper.</param>
    public static IEnumerable<Door> Get(Room roomId) => Get(roomId.Base);

    /// <summary>
    /// Gets the door in specified room.
    /// </summary>
    /// <param name="roomId">Target room identifier.</param>
    public static IEnumerable<Door> Get(RoomIdentifier roomId) =>
        List.Where(x => x.Rooms.First().Equals(roomId));

    /// <summary>
    /// A protected method to create new door wrappers from the base game object.
    /// </summary>
    /// <param name="doorVariant">The base object to create the wrapper from.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static Door CreateDoorWrapper(DoorVariant doorVariant)
    {
        Type targetType = doorVariant.GetType();
        if (!typeWrappers.TryGetValue(targetType, out Func<DoorVariant, Door> ctorFunc))
        {
            Logger.Warn($"Unable to find {nameof(Door)} wrapper for {targetType.Name}, backup up to base constructor!");
            return new Door(doorVariant);
        }

        return ctorFunc.Invoke(doorVariant);
    }

    /// <summary>
    /// Private method to handle the creation of new doors in the server.
    /// </summary>
    /// <param name="doorVariant">The <see cref="DoorVariant"/> that was created.</param>
    private static void OnAdded(DoorVariant doorVariant)
    {
        try
        {
            if (!Dictionary.ContainsKey(doorVariant))
                CreateDoorWrapper(doorVariant);
        }
        catch (Exception ex)
        {
            Console.Logger.Error($"An exception occurred while handling the creation of a new door in LabApi.Features.Wrappers.Door.OnAdded(DoorVariant). Error: {ex}");
        }
    }

    /// <summary>
    /// Private method to handle the removal of doors from the server.
    /// </summary>
    /// <param name="doorVariant">The door being destroyed.</param>
    private static void OnRemoved(DoorVariant doorVariant)
    {
        if (Dictionary.TryGetValue(doorVariant, out Door door))
            door.OnRemove();
    }

    /// <summary>
    /// A private method to handle the addition of wrapper handlers.
    /// </summary>
    /// <typeparam name="T">The derived base game type to handle.</typeparam>
    /// <param name="constructor">A handler to construct the wrapper with the base game instance.</param>
    private static void Register<T>(Func<T, Door> constructor) where T : DoorVariant
    {
        typeWrappers.Add(typeof(T), x => constructor((T)x));
    }
}