using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.BlastDoorChanged"/> event.
/// </summary>
public class BlastDoorChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlastDoorChangedEventArgs"/> class.
    /// </summary>
    /// <param name="blastDoor">The door that is chaning it's state.</param>
    /// <param name="newState">The <paramref name="blastDoor"/>'s new state.</param>
    public BlastDoorChangedEventArgs(BlastDoor blastDoor, bool newState)
    {
        BlastDoor = blastDoor;
        NewState = newState;
    }

    /// <summary>
    /// Gets the current Blast Door.
    /// </summary>
    public BlastDoor BlastDoor { get; }

    /// <summary>
    /// Gets the <see cref="BlastDoor"/> new state.
    /// </summary>
    public bool NewState { get; }
}
