using Generators;
using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Extensions;
using MapGeneration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

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
        RoomIdentifier.OnRemoved += RemoveRoom;
    }

    /// <summary>
    /// Contains all the cached rooms in the game, accessible through their <see cref="RoomIdentifier"/>.
    /// </summary>
    public static Dictionary<RoomIdentifier, Room> Dictionary { get; } = [];

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
        Base = roomIdentifier;

        if (CanCache)
            Dictionary.Add(roomIdentifier, this);
    }

    /// <summary>
    /// An internal virtual method to signal to derived wrappers that the base has been destroyed.
    /// </summary>
    internal virtual void OnRemoved()
    {
        Dictionary.Remove(Base);
        _adjacentRooms = null;
        _connectedRooms = null;
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public RoomIdentifier Base { get; }

    /// <summary>
    /// Gets whether the base room instance was destroyed.
    /// </summary>
    public bool IsDestroyed => Base == null || GameObject == null;

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
    /// Gets the room's adjacent rooms where the player can traverse to.
    /// Includes rooms that can be traversed to via elevator.
    /// </summary>
    public IReadOnlyCollection<Room> AdjacentRooms
    {
        get
        {
            if (_adjacentRooms != null)
                return _adjacentRooms;

            List<Room> rooms = [.. ConnectedRooms.Select(Get)];

            // Check if the room has elevator
            Elevator? target = null;
            foreach (Elevator elevator in Elevator.List)
            {
                if (elevator.Rooms.Contains(this))
                    target = elevator;
            }

            // Add rooms that are on other floors of this elevator
            if (target != null)
            {
                foreach (Room room in target.Rooms)
                {
                    if (room == this)
                        continue;

                    rooms.Add(room);
                }
            }

            //Hcz-Ez checkpoints
            if (Name == RoomName.HczCheckpointToEntranceZone)
            {
                FacilityZone targetZone = Zone == FacilityZone.HeavyContainment ? FacilityZone.Entrance : FacilityZone.HeavyContainment;

                Room? match = List
                    .Where(n => n.Name == RoomName.HczCheckpointToEntranceZone && n.Zone == targetZone)
                    .MinBy(n => (n.Position - Position).sqrMagnitude);

                if (match != null)
                    rooms.Add(match);
            }

            _adjacentRooms = rooms.AsReadOnly();
            return _adjacentRooms;
        }
    }

    /// <summary>
    /// Gets the doors that are a part of this room.
    /// </summary>
    public IEnumerable<Door> Doors
    {
        get
        {
            return DoorVariant.DoorsByRoom.TryGetValue(Base, out HashSet<DoorVariant> doors) ? doors.Where(static x => x != null).Select(static x => Door.Get(x!)) : [];
        }
    }

    /// <summary>
    /// Gets the first light controller for this room.<br/>
    /// <note>
    /// Use <see cref="AllLightControllers"/> if you wish to modify all lights in this room.
    /// </note>
    /// </summary>
    public LightsController? LightController => Base.LightControllers.Count > 0 ? LightsController.Get(Base.LightControllers[0]) : null;

    /// <summary>
    /// Gets all light controllers for this specified room.<br/>
    /// Some rooms such as 049, warhead and such may have multiple light controllers as they are split by the elevator.
    /// </summary>
    public IEnumerable<LightsController> AllLightControllers => Base.LightControllers.Select(LightsController.Get);

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
    /// Gets a collection of players in the room.
    /// </summary>
    public IEnumerable<Player> Players => Player.List.Where(p => p.Room == this);

    /// <summary>
    /// Gets a collection of cameras in the room.
    /// </summary>
    public IEnumerable<Camera> Cameras => Camera.List.Where(x => x.Room == this);

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[{GetType().Name}: Name={Name}, Shape={Shape}, Zone={Zone}]";
    }

    /// <summary>
    /// Gets whether the room wrapper is allowed to be cached.
    /// </summary>
    protected bool CanCache => !IsDestroyed;

    private HashSet<Room>? _connectedRooms;

    private IReadOnlyCollection<Room>? _adjacentRooms;

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
    /// Gets path from <paramref name="start"/> to <paramref name="end"/>.<br/>
    /// Path is found via <see href="https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm">Dijkstra's algorithm</see>. Path still works between zones (including via elevators) as it uses <see cref="AdjacentRooms"/>.<br/>
    /// If no path is found, an empty list is returned.
    /// </summary>
    /// <param name="start">The starting room.</param>
    /// <param name="end">The ending room.</param>
    /// <param name="weightFunction">The weight function to calculate cost to the next room. Can be change if you prefer to go around tesla gates and such.</param>
    /// <returns>A pooled list containing rooms from <paramref name="start"/> to <paramref name="end"/> (including these rooms).</returns>
    public static List<Room> FindPath(Room start, Room end, Func<Room, int> weightFunction)
    {
        List<Room> path = NorthwoodLib.Pools.ListPool<Room>.Shared.Rent();

        if (start == null || end == null || start == end)
            return path;

        Dictionary<Room, Room?> previous = DictionaryPool<Room, Room?>.Get();
        Dictionary<Room, int> distances = DictionaryPool<Room, int>.Get();
        HashSet<Room> visited = NorthwoodLib.Pools.HashSetPool<Room>.Shared.Rent();
        PriorityQueue<Room> queue = PriorityQueuePool<Room>.Shared.Rent();

        // Standard dijakstra
        foreach (Room room in List)
        {
            distances[room] = int.MaxValue;
            previous[room] = null;
        }

        distances[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            Room current = queue.Dequeue();

            if (visited.Contains(current))
                continue;

            visited.Add(current);

            if (current == end)
                break;

            foreach (Room neighbor in current.AdjacentRooms)
            {
                if (visited.Contains(neighbor))
                    continue;

                int newDistance = distances[current] + weightFunction(neighbor);
                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    previous[neighbor] = current;
                    queue.Enqueue(neighbor, newDistance);
                }
            }
        }

        Room? step = end;

        // Reconstruct the path from start to end room
        while (step != null)
        {
            path.Insert(0, step);
            step = previous[step];
        }

        // If only entry is the end room, clear the path as it wasnt found
        if (path.Count == 1 && path[0] == end)
            path.Clear();

        DictionaryPool<Room, Room?>.Release(previous);
        DictionaryPool<Room, int>.Release(distances);
        NorthwoodLib.Pools.HashSetPool<Room>.Shared.Return(visited);
        PriorityQueuePool<Room>.Shared.Return(queue);

        return path;
    }

    /// <inheritdoc cref="FindPath(Room, Room, Func{Room, int})"/>
    public static List<Room> FindPath(Room start, Room end) => FindPath(start, end, static (room) => 1);

    /// <summary>
    /// Gets the room wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="roomIdentifier">The identifier of the room.</param>
    /// <returns>The requested room.</returns>
    [return: NotNullIfNotNull(nameof(roomIdentifier))]
    public static Room? Get(RoomIdentifier? roomIdentifier)
    {
        if (roomIdentifier == null)
            return null;

        if (Dictionary.TryGetValue(roomIdentifier, out Room room))
            return room;

        return CreateRoomWrapper(roomIdentifier);
    }

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
    /// Tries to get the room at the specified position.
    /// </summary>
    /// <param name="position">The position to get the room at.</param>
    /// <param name="room">The room at the specified position.</param>
    /// <returns>Whether the room was found at the specified position.</returns>
    public static bool TryGetRoomAtPosition(Vector3 position, [NotNullWhen(true)] out Room? room)
    {
        if (!RoomUtils.TryGetRoom(position, out RoomIdentifier baseRoom))
        {
            room = null;
            return false;
        }

        room = Get(baseRoom);
        return true;
    }

    /// <summary>
    /// Gets the room at the specified position.
    /// </summary>
    /// <param name="position">The position to get the room at.</param>
    /// <returns>The room at the specified position or <see langword="null"/> if no room was found.</returns>
    public static Room? GetRoomAtPosition(Vector3 position) => TryGetRoomAtPosition(position, out Room? room) ? room : null;

    /// <summary>
    /// Creates a new wrapper from the base room object.
    /// </summary>
    /// <param name="roomIdentifier">The base <see cref="RoomIdentifier"/> object.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static Room CreateRoomWrapper(RoomIdentifier roomIdentifier)
    {
        if (roomIdentifier.Name == RoomName.Pocket)
            return new PocketDimension(roomIdentifier);
        else if (roomIdentifier.Name == RoomName.Lcz914)
            return new Scp914(roomIdentifier);
        else
            return new Room(roomIdentifier);
    }

    /// <summary>
    /// Handles the creation of a room in the server.
    /// </summary>
    /// <param name="roomIdentifier">The <see cref="RoomIdentifier"/> of the room.</param>
    private static void AddRoom(RoomIdentifier roomIdentifier)
    {
        try
        {
            if (!Dictionary.ContainsKey(roomIdentifier))
                _ = CreateRoomWrapper(roomIdentifier);
        }
        catch (Exception e)
        {
            Console.Logger.InternalError($"Failed to handle room creation with error: {e}");
        }
    }

    /// <summary>
    /// Handles the removal of a room from the server.
    /// </summary>
    /// <param name="roomIdentifier">The <see cref="RoomIdentifier"/> of the room.</param>
    private static void RemoveRoom(RoomIdentifier roomIdentifier)
    {
        try
        {
            if (Dictionary.TryGetValue(roomIdentifier, out Room room))
                room.OnRemoved();
        }
        catch (Exception e)
        {
            Console.Logger.InternalError($"Failed to handle item destruction with error: {e}");
        }
    }
}