using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.RemovingObserver"/> event.
/// </summary>
public class Scp173RemovingObserverEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173RemovingObserverEventArgs"/> class.
    /// </summary>
    /// <param name="target">The player that was observing the SCP-173 player.</param>
    /// <param name="player">The SCP-173 player instance.</param>
    public Scp173RemovingObserverEventArgs(ReferenceHub target, ReferenceHub player)
    {
        IsAllowed = true;
        Target = Player.Get(target);
        Player = Player.Get(player);
    }

    /// <summary>
    /// The player that was observing the SCP-173 player.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}