using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Facility;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.MadeNoise"/> event.
/// </summary>
public class PlayerMadeNoiseEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMadeNoiseEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is making noise.</param>
    public PlayerMadeNoiseEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <summary>
    /// Gets the player who is making noise.
    /// </summary>
    public Player Player { get; }
}