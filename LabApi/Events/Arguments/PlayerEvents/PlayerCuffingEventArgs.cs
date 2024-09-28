using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Cuffing"/> event.
/// </summary>
public class PlayerCuffingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerCuffingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is detaining another one.</param>
    /// <param name="target">The player who is being detained.</param>
    public PlayerCuffingEventArgs(ReferenceHub player, ReferenceHub target)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
    }

    /// <summary>
    /// Gets the player who is detaining another one.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who is being detained.
    /// </summary>
    public Player Target { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}