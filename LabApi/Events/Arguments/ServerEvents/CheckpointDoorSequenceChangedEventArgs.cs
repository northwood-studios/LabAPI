using LabApi.Features.Wrappers;
using System;
using static Interactables.Interobjects.CheckpointDoor;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CheckpointDoorSequenceChanged"/> event.
/// </summary>
public class CheckpointDoorSequenceChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoorDamagingEventArgs"/> class.
    /// </summary>
    /// <param name="door">The current checkpoint door.</param>
    /// <param name="sequenceState">The current state this checkpoint is.</param>
    public CheckpointDoorSequenceChangedEventArgs(Interactables.Interobjects.CheckpointDoor door, SequenceState sequenceState)
    {
        CheckpointDoor = CheckpointDoor.Get(door);
        CurrentSequence = sequenceState;
    }

    /// <summary>
    /// Gets the current Checkpoint Door.
    /// </summary>
    public CheckpointDoor CheckpointDoor { get; }

    /// <summary>
    /// Gets the damage value.
    /// </summary>
    public SequenceState CurrentSequence { get; }
}
