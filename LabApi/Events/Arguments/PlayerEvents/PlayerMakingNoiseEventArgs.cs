using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.MakingNoise"/> event.
/// </summary>
public class PlayerMakingNoiseEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMakingNoiseEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is making noise.</param>
    public PlayerMakingNoiseEventArgs(ReferenceHub? player)
    {
        IsAllowed = true;
        Player = Player.Get(player);
    }

    /// <summary>
    /// Gets the player who is making noise.
    /// </summary>
    public Player? Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}