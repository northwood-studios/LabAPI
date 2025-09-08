using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Cuffed"/> event.
/// </summary>
public class PlayerCuffedEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerCuffedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who detained another one.</param>
    /// <param name="target">The player who was detained.</param>
    public PlayerCuffedEventArgs(ReferenceHub hub, ReferenceHub target)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
    }

    /// <summary>
    /// Gets the player who detained another one.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who was detained.
    /// </summary>
    public Player Target { get; }
}