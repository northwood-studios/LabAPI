using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.DoorLockChanged"/> event.
/// </summary>
public class DoorLockChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoorLockChangedEventArgs"/> class.
    /// </summary>
    /// <param name="door">The door whose lock reason changed.</param>
    /// <param name="prevLockReason">The previous lock reason.</param>
    /// <param name="activeLocks">The <paramref name="door"/>'s lock reason.</param>
    public DoorLockChangedEventArgs(DoorVariant door, ushort prevLockReason, ushort activeLocks)
    {
        Door = Door.Get(door);
        PrevLockReason = (DoorLockReason)prevLockReason;
        LockReason = (DoorLockReason)activeLocks;
    }

    /// <summary>
    /// Gets the current Door.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets the <see cref="Door"/> old lock reason.
    /// </summary>
    public DoorLockReason PrevLockReason { get; }

    /// <summary>
    /// Gets the <see cref="Door"/> new lock reason.
    /// </summary>
    public DoorLockReason LockReason { get; }
}
