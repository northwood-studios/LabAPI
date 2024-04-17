using System.Collections.Generic;
using MapGeneration;

namespace LabApi.Features.Wrappers.Facility.Rooms;

/// <summary>
/// The wrapper representing <see cref="RoomIdentifier">room identifiers</see>, the in-game rooms.
/// </summary>
public class Room
{
    /// <summary>
    /// Contains all the cached rooms in the game, accessible through their <see cref="RoomIdentifier"/>.
    /// </summary>
    public static Dictionary<RoomIdentifier, Room> Dictionary { get; } = [];

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
        RoomIdentifier = roomIdentifier;
    }
    
    /// <summary>
    /// The <see cref="RoomIdentifier">identifier</see> of the room.
    /// </summary>
    public RoomIdentifier RoomIdentifier { get; }
    
    /// <summary>
    /// Gets the room wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="roomIdentifier">The identifier of the room.</param>
    /// <returns>The requested room.</returns>
    public static Room Get(RoomIdentifier roomIdentifier) =>
        Dictionary.TryGetValue(roomIdentifier, out Room room) ? room : new Room(roomIdentifier);
}