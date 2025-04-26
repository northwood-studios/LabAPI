﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using Interactables.Interobjects.DoorUtils;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.LockedDoor"/> event.
/// </summary>
public class Scp079LockedDoorEventArgs : EventArgs, IPlayerEvent, IDoorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079LockedDoorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="door">The affected door instance.</param>
    public Scp079LockedDoorEventArgs(ReferenceHub player, DoorVariant door)
    {
        Player = Player.Get(player);
        Door = Door.Get(door);
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected door instance.
    /// </summary>
    public Door Door { get; }
}
