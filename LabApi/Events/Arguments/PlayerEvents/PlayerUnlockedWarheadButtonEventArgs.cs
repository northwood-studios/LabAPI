using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnlockedWarheadButton"/> event.
/// </summary>
public class PlayerUnlockedWarheadButtonEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnlockedWarheadButtonEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who has unlocked the warhead button.</param>
    public PlayerUnlockedWarheadButtonEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <summary>
    /// Gets the player who has unlocked the warhead button.
    /// </summary>
    public Player Player { get; }
}