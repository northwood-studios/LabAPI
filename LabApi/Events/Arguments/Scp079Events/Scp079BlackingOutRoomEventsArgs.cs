﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using MapGeneration;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.BlackingOutRoom"/> event.
/// </summary>
public class Scp079BlackingOutRoomEventsArgs : EventArgs, IPlayerEvent, IRoomEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079BlackingOutRoomEventsArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-079 player instance.</param>
    /// <param name="room">The affected room instance.</param>
    public Scp079BlackingOutRoomEventsArgs(ReferenceHub hub, RoomIdentifier room)
    {
        Player = Player.Get(hub);
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
