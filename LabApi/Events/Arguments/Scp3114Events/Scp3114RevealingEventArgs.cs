using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp3114Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp3114Events.Revealing"/> event.
/// </summary>
public class Scp3114RevealingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp3114RevealingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player SCP who is undisguising./param>
    /// <param name="forced">Bool whether the reveal is forced by timer running out or if it was player's request.</param>
    public Scp3114RevealingEventArgs(ReferenceHub player, bool forced)
    {
        Player = Player.Get(player);
        Forced = forced;
        IsAllowed = true;
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <summary>
    /// Gets whether the reveal is forced by timer running out or if it was player's request.
    /// </summary>
    public bool Forced { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
