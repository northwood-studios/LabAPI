using LabApi.Features.Wrappers;
using MapGeneration;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.RoomColorChanged"/> event.
/// </summary>
public class RoomColorChangedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoomColorChangedEventArgs"/> class.
    /// </summary>
    /// <param name="room">The room that is changing its color.</param>
    /// <param name="newState">The <paramref name="room"/>'s new color.</param>
    public RoomColorChangedEventArgs(RoomIdentifier room, Color newState)
    {
        Room = Room.Get(room);
        NewState = newState;
    }

    /// <summary>
    /// Gets the current room.
    /// </summary>
    public Room Room { get; }

    /// <summary>
    /// Gets the <see cref="Room"/> new color.
    /// </summary>
    public Color NewState { get; }
}
