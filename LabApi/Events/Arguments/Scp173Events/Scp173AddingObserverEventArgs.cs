using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.AddingObserver"/> event.
/// </summary>
public class Scp173AddingObserverEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173AddingObserverEventArgs"/> class.
    /// </summary>
    /// <param name="target">The player that is observing the SCP-173 player.</param>
    /// <param name="player">The SCP-173 player instance.</param>
    public Scp173AddingObserverEventArgs(Player target, Player player)
    {
        IsAllowed = true;
        Target = target;
        Player = player;
    }
    
    /// <summary>
    /// The player that is observing the SCP-173 player.
    /// </summary>
    public Player Target { get; }
    
    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}