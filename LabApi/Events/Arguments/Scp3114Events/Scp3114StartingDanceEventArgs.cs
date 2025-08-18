using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp3114Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp3114Events.StartDancing"/> event.
/// </summary>
public class Scp3114StartingDanceEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp3114StartingDanceEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is starting to dance.</param>
    /// <param name="danceId">The index of the animation to play.</param>
    public Scp3114StartingDanceEventArgs(ReferenceHub player, byte danceId)
    {
        Player = Player.Get(player);
        DanceId = danceId;
        IsAllowed = true;
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the index of the animation to play.
    /// Currently there are 7 dance variants in game.
    /// <para>Any value above max length will be moved to equal index via modulo (%) operator.</para>
    /// </summary>
    public byte DanceId { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
