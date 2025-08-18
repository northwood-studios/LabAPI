using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp3114Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp3114Events.Dance"/> event.
/// </summary>
public class Scp3114StartedDanceEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp3114StartingDanceEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is starting to dance.</param>
    /// <param name="danceId">The index of the animation to play.</param>
    public Scp3114StartedDanceEventArgs(ReferenceHub player, byte danceId)
    {
        Player = Player.Get(player);
        DanceId = danceId;
    }

    /// <summary>
    /// Gets or sets the index of the animation to play.
    /// </summary>
    public byte DanceId { get; }

    /// <inheritdoc/>
    public Player Player { get; }
}
