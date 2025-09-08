using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RoomChanged"/> event.
/// </summary>
public class PlayerRoomChangedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRoomChangedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose room changed.</param>
    /// <param name="oldRoom">The old room.</param>
    /// <param name="newRoom">The new room.</param>
    public PlayerRoomChangedEventArgs(ReferenceHub player, RoomIdentifier oldRoom, RoomIdentifier newRoom)
    {
        Player = Player.Get(player);
        OldRoom = Room.Get(oldRoom);
        NewRoom = Room.Get(newRoom);
    }

    /// <summary>
    /// Gets the player whose last known room changed.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old room. May be null if the player just spawned, went through void and such.
    /// </summary>
    public Room? OldRoom { get; }

    /// <summary>
    /// Gets the new room. May be null if the player went into void, died and such.
    /// </summary>
    public Room? NewRoom { get; }
}
