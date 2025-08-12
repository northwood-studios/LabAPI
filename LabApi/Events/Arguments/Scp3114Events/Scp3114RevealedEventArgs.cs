using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp3114Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp3114Events.Revealed"/> event.
/// </summary>
public class Scp3114RevealedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp3114RevealedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player SCP so is undisguised.</param>
    /// <param name="forced">Bool whether the reveal is forced by timer running out or if it was player's request.</param>
    public Scp3114RevealedEventArgs(ReferenceHub player, bool forced)
    {
        Player = Player.Get(player);
        Forced = forced;
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <summary>
    /// Gets whether the reveal was forced by timer running out or if it was player's request.
    /// </summary>
    public bool Forced { get; }
}
