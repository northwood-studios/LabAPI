using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static Interactables.Interobjects.CheckpointDoor;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CheckpointDoorSequenceChanging"/> event.
/// </summary>
public class CheckpointDoorSequenceChangingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CheckpointDoorSequenceChangingEventArgs"/> class.
    /// </summary>
    /// <param name="door">The current checkpoint door.</param>
    /// <param name="oldState">The current state this checkpoint is.</param>
    /// <param name="sequenceState">The new state this checkpoint changing.</param>
    public CheckpointDoorSequenceChangingEventArgs(Interactables.Interobjects.CheckpointDoor door, SequenceState oldState, SequenceState sequenceState)
    {
        CheckpointDoor = CheckpointDoor.Get(door);
        CurrentSequence = oldState;
        NewSequence = sequenceState;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets the current Checkpoint Door.
    /// </summary>
    public CheckpointDoor CheckpointDoor { get; }

    /// <summary>
    /// Gets the current sequence state.
    /// </summary>
    public SequenceState CurrentSequence { get; }

    /// <summary>
    /// Gets or sets the new sequence state.
    /// </summary>
    public SequenceState NewSequence { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
