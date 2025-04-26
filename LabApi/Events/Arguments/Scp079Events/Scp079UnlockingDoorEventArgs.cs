﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using Interactables.Interobjects.DoorUtils;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.UnlockingDoor"/> event.
/// </summary>
public class Scp079UnlockingDoorEventArgs : EventArgs, IPlayerEvent, IDoorEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079UnlockingDoorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="door">The affected door instance.</param>
    public Scp079UnlockingDoorEventArgs(ReferenceHub player, DoorVariant door)
    {
        Player = Player.Get(player);
        Door = Door.Get(door);
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected door instance.
    /// </summary>
    public Door Door { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
