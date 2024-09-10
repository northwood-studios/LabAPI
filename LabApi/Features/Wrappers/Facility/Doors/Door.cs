using System;
using System.Collections.Generic;
using System.Linq;
﻿using Interactables.Interobjects.DoorUtils;
using LabApi.Loader.Features.Misc;
using MapGeneration;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="DoorVariant">door variants</see>, the in-game doors.
/// </summary>
public class Door
{
    [InitializeWrapper]
    internal static void Initialize()
    {
        DoorVariant.OnInstanceCreated += (door) => new Door(door);
        DoorVariant.OnInstanceRemoved += (door) => Dictionary.Remove(door);
    }

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
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public DoorVariant Base { get; }

    /// <summary>
    /// Gets the rooms which have this door.
    /// </summary>
    public RoomIdentifier[] Rooms => Base.Rooms;

    /// <summary>
    /// Gets the zone in which this door is.
    /// </summary>
    public FacilityZone Zone => Rooms.FirstOrDefault()?.Zone ?? FacilityZone.Other;

    /// <summary>
    /// Gets or sets whether or not the door is open.
    /// </summary>
    public bool IsOpened
    {
        get => Base.TargetState;
        set => Base.TargetState = value;
    }

    /// <summary>
    /// Gets or sets whether or not the door is locked.
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
    /// <param name="enabled">Whether or not the door lock reason is new.</param>
    public void Lock(DoorLockReason reason, bool enabled) => Base.ServerChangeLock(reason, enabled);

    /// <summary>
    /// Gets or sets the required <see cref="KeycardPermissions"/>.
    /// </summary>
    public KeycardPermissions Permissions
    {
        get => Base.RequiredPermissions.RequiredPermissions;
        set => Base.RequiredPermissions.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets or sets whether or not the door will bypass 2176.
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
    /// Gets the door wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="doorVariant">The <see cref="DoorVariant"/> of the door.</param>
    /// <returns>The requested door.</returns>
    public static Door Get(DoorVariant doorVariant)
    {
        if (Dictionary.TryGetValue(doorVariant, out Door door))
            return door;

        throw new NullReferenceException("This should never happen.");
    }

    /// <summary>
    /// Gets the door by it's nametag.
    /// </summary>
    /// <param name="nametag">The door's nametag</param>
    /// <returns><The requested door. May be null if door with provided nametag does not exist./returns>
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
    /// <param name="roomId">Target room identifier.</param>
    public static IEnumerable<Door> Get(RoomIdentifier roomId) =>
        List.Where(x => x.Rooms.First().Equals(roomId));
}