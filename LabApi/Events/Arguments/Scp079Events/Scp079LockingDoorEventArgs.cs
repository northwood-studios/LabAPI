using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.LockingDoor"/> event.
/// </summary>
public class Scp079LockingDoorEventArgs : EventArgs, IPlayerEvent, IDoorEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079LockingDoorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-079 player instance.</param>
    /// <param name="door">The affected door instance.</param>
    public Scp079LockingDoorEventArgs(ReferenceHub hub, DoorVariant door)
    {
        Player = Player.Get(hub);
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
