using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Joined"/> event.
/// </summary>
public class PlayerJoinedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerJoinedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who joined.</param>
    public PlayerJoinedEventArgs(ReferenceHub hub)
    {
        Player = Player.Get(hub);
    }

    /// <summary>
    /// Gets the player who joined.
    /// </summary>
    public Player Player { get; }
}