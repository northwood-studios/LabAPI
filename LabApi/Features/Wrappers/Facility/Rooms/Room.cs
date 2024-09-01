using System.Collections.Generic;
using System.Linq;
using Interactables.Interobjects.DoorUtils;
using LabApi.Loader.Features.Misc;
using MapGeneration;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="RoomIdentifier">room identifiers</see>, the in-game rooms.
/// </summary>
public class Room
{
    [InitializeWrapper]
    internal static void Initialize()
    {
        RoomIdentifier.OnAdded += (roomIdentifier) => _ = new Room(roomIdentifier);
        RoomIdentifier.OnRemoved += (roomIdentier) => Dictionary.Remove(roomIdentier);
    }

    /// <summary>
    /// Contains all the cached rooms in the game, accessible through their <see cref="RoomIdentifier"/>.
    /// </summary>
    private static Dictionary<RoomIdentifier, Room> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Room"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Room> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="roomIdentifier">The identifier of the room.</param>
    private Room(RoomIdentifier roomIdentifier)
    {
        Dictionary.Add(roomIdentifier, this);
        Base = roomIdentifier;
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public RoomIdentifier Base { get; }

    /// <summary>
    /// The room's shape.
    /// </summary>

    public RoomShape Shape => Base.Shape;

    /// <summary>
    /// The room's name.
    /// </summary>

    public RoomName Name => Base.Name;

    /// <summary>
    /// The zone in which this room is located.
    /// </summary>
    public FacilityZone Zone => Base.Zone;

    /// <summary>
    /// Gets the doors that are a part of this room.
    /// </summary>
    public IEnumerable<Door> Doors
    {
        get
        {
            if (!DoorVariant.DoorsByRoom.TryGetValue(Base, out HashSet<DoorVariant> doorList))
                return [];

            return doorList.Select(Door.Get);
        }
    }

    /// <summary>
    /// Gets the room's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the room's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the room's position.
    /// </summary>
    public Vector3 Position => Transform.position;

    /// <summary>
    /// Gets the room's rotation.
    /// </summary>
    public Quaternion Rotation => Transform.rotation;

    /// <summary>
    /// Gets the room wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="roomIdentifier">The identifier of the room.</param>
    /// <returns>The requested room.</returns>
    public static Room Get(RoomIdentifier roomIdentifier) =>
        Dictionary.TryGetValue(roomIdentifier, out Room room) ? room : new Room(roomIdentifier);

    public static IEnumerable<Room> Get(RoomName roomName) =>
        List.Where(x => x.Name == roomName);

    public static IEnumerable<Room> Get(RoomShape roomShape) =>
        List.Where(x => x.Shape == roomShape);

    public static IEnumerable<Room> Get(FacilityZone facilityZone) =>
        List.Where(x => x.Zone == facilityZone);
}