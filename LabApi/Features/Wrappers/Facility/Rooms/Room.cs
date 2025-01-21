using Generators;
using MapGeneration;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="RoomIdentifier">room identifiers</see>, the in-game rooms.
/// </summary>
public class Room
{
    /// <summary>
    /// Initializes the Room wrapper by subscribing to the RoomIdentifier events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        RoomIdentifier.OnAdded += AddRoom;
        RoomIdentifier.OnRemoved += (roomIdentifier) => Dictionary.Remove(roomIdentifier);
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
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="roomIdentifier">The identifier of the room.</param>
    internal Room(RoomIdentifier roomIdentifier)
    {
        Dictionary.Add(roomIdentifier, this);
        Base = roomIdentifier;
    }

    /// <summary>
    /// An internal virtual method to signal to derived wrappers that the base has been destroyed.
    /// </summary>
    internal virtual void OnRemoved()
    {
        Dictionary.Remove(Base);
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
    /// Gets the room's neighbors.
    /// </summary>
    public HashSet<RoomIdentifier> ConnectedRooms => Base.ConnectedRooms;

    /// <summary>
    /// Gets the doors that are a part of this room.
    /// </summary>
    public IEnumerable<Door> Doors
    {
        get
        {
            return Doors.Where(n => n.Rooms.Contains(Base)); // TODO: Cache the result in base game code?
        }
    }

    /// <summary>
    /// Gets the first light controller for this room.<br></br>
    /// <note>Please see <see cref="AllLightControllers"/> if you wish to modify all lights in this room.</note>
    /// </summary>
    public LightsController? LightController
    {
        get
        {
            return Base.LightControllers.Count > 0 ? LightsController.Get(Base.LightControllers[0]) : null;
        }
    }

    /// <summary>
    /// Gets all light controllers for this specified room.<br/>
    /// Some rooms such as 049, warhead and etc may have multiple light controllers as they are split by the elevator.
    /// </summary>
    public IEnumerable<LightsController> AllLightControllers
    {
        get
        {
            return Base.LightControllers.Select(LightsController.Get);
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

    /// <summary>
    /// Gets the room by its <see cref="RoomName"/>.
    /// </summary>
    /// <param name="roomName">The name of the room.</param>
    /// <returns>The requested room.</returns>
    public static IEnumerable<Room> Get(RoomName roomName) =>
        List.Where(x => x.Name == roomName);

    /// <summary>
    /// Gets the rooms by their shape.
    /// </summary>
    /// <param name="roomShape">The shape of the rooms to get.</param>
    /// <returns>The rooms with the specified shape.</returns>
    public static IEnumerable<Room> Get(RoomShape roomShape) =>
        List.Where(x => x.Shape == roomShape);

    /// <summary>
    /// Gets the rooms in the specified zone.
    /// </summary>
    /// <param name="facilityZone">The zone to get the rooms from.</param>
    /// <returns>The rooms in the specified zone.</returns>
    public static IEnumerable<Room> Get(FacilityZone facilityZone) =>
        List.Where(x => x.Zone == facilityZone);

    /// <summary>
    /// Gets the rooms from the provided <see cref="RoomIdentifier"/>s.
    /// </summary>
    /// <param name="roomIdentifiers">The room identifiers to get the rooms from.</param>
    /// <returns>The requested rooms.</returns>
    public static IEnumerable<Room> Get(IEnumerable<RoomIdentifier> roomIdentifiers) =>
        roomIdentifiers.Select(Get);


    /// <summary>
    /// Gets the closest <see cref="LightsController"/> to the specified player.
    /// </summary>
    /// <param name="player">The player to check the closest light controller for.</param>
    /// <returns>The closest light controller. May return <see langword="null"/> if player is not alive or is not in any room.</returns>
    public LightsController? GetClosestLightController(Player player)
    {
        RoomLightController rlc = Base.GetClosestLightController(player.ReferenceHub);
        return rlc == null ? null : LightsController.Get(rlc);
    }

    /// <summary>
    /// Tries to get the room at the specified position.
    /// </summary>
    /// <param name="position">The position to get the room at.</param>
    /// <param name="room">The room at the specified position.</param>
    /// <returns>Whether the room was found at the specified position.</returns>
    public static bool TryGetRoomAtPosition(Vector3 position, [NotNullWhen(true)] out Room? room)
    {
        RoomIdentifier? roomIdentifier = RoomIdUtils.RoomAtPosition(position) ?? RoomIdUtils.RoomAtPositionRaycasts(position);
        if (roomIdentifier == null)
        {
            room = null;
            return false;
        }

        room = Get(roomIdentifier);
        return true;
    }

    /// <summary>
    /// Gets the room at the specified position.
    /// </summary>
    /// <param name="position">The position to get the room at.</param>
    /// <returns>The room at the specified position or <see langword="null"/> if no room was found.</returns>
    public static Room? GetRoomAtPosition(Vector3 position) => TryGetRoomAtPosition(position, out Room? room) ? room : null;

    /// <summary>
    /// Handles the creation of a room in the server.
    /// </summary>
    /// <param name="roomIdentifier">The <see cref="RoomIdentifier"/> of the room.</param>
    // TODO: use factory instead.
    private static void AddRoom(RoomIdentifier roomIdentifier)
    {
        if (roomIdentifier.Name == RoomName.Pocket)
            _ = new PocketDimension(roomIdentifier);
        else
            _ = new Room(roomIdentifier);
    }

    /// <summary>
    /// Handles the removal of a room from the server.
    /// </summary>
    /// <param name="roomIdentifier">The <see cref="RoomIdentifier"/> of the room.</param>
    private static void RemoveRoom(RoomIdentifier roomIdentifier)
    {
        if (Dictionary.TryGetValue(roomIdentifier, out Room room))
            room.OnRemoved();
    }
}