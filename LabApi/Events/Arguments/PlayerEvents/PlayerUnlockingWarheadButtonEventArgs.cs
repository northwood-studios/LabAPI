using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnlockingWarheadButton"/> event.
/// </summary>
public class PlayerUnlockingWarheadButtonEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnlockingWarheadButtonEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who unlocking the warhead button.</param>
    public PlayerUnlockingWarheadButtonEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player who is unlocking the warhead button.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}