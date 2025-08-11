using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.LockingDownRoom"/> event.
/// </summary>
public class Scp079LockingDownRoomEventArgs : EventArgs, IPlayerEvent, IRoomEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079LockingDownRoomEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="room">The affected room instance.</param>
    public Scp079LockingDownRoomEventArgs(ReferenceHub player, RoomIdentifier room)
    {
        Player = Player.Get(player);
        Room = Room.Get(room);
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected room instance.
    /// </summary>
    public Room Room { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
