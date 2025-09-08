using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.RemovedObserver"/> event.
/// </summary>
public class Scp173RemovedObserverEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173RemovedObserverEventArgs"/> class.
    /// </summary>
    /// <param name="target">The player that was observing the SCP-173 player.</param>
    /// <param name="hub">The SCP-173 player instance.</param>
    public Scp173RemovedObserverEventArgs(ReferenceHub target, ReferenceHub hub)
    {
        Target = Player.Get(target);
        Player = Player.Get(hub);
    }

    /// <summary>
    /// The player that was observing the SCP-173 player.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }
}