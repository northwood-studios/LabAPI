using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using MapGeneration;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.LockedDownRoom"/> event.
/// </summary>
public class Scp079LockedDownRoomEventArgs : EventArgs, IPlayerEvent, IRoomEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079LockedDoorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="room">The affected room instance.</param>
    public Scp079LockedDownRoomEventArgs(ReferenceHub player, RoomIdentifier room)
    {
        Player = Player.Get(player);
        Room = Room.Get(room);
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected room instance.
    /// </summary>
    public Room Room { get; }
}