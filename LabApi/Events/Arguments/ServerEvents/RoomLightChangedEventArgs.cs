using LabApi.Features.Wrappers;
using MapGeneration;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.RoomLightChanged"/> event.
/// </summary>
public class RoomLightChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoomLightChangedEventArgs"/> class.
    /// </summary>
    /// <param name="room">The room that is changing its light state.</param>
    /// <param name="newState">The <paramref name="room"/>'s new light state.</param>
    public RoomLightChangedEventArgs(RoomIdentifier room, bool newState)
    {
        Room = Room.Get(room);
        NewState = newState;
    }

    /// <summary>
    /// Gets the current room.
    /// </summary>
    public Room Room { get; }

    /// <summary>
    /// Gets the <see cref="Room"/> new light state.
    /// </summary>
    public bool NewState { get; }
}
