using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.BlastDoorChanging"/> event.
/// </summary>
public class BlastDoorChangingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlastDoorChangingEventArgs"/> class.
    /// </summary>
    /// <param name="blastDoor">The door that is changing it's state.</param>
    /// <param name="newState">The <paramref name="blastDoor"/>'s new state.</param>
    public BlastDoorChangingEventArgs(BlastDoor blastDoor, bool newState)
    {
        BlastDoor = blastDoor;
        NewState = newState;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets the current Blast Door.
    /// </summary>
    public BlastDoor BlastDoor { get; }

    /// <summary>
    /// Gets or sets the <see cref="BlastDoor"/> new state.
    /// </summary>
    public bool NewState { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
