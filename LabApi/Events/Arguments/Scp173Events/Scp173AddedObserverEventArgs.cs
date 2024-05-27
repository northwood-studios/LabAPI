using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.AddedObserver"/> event.
/// </summary>
public class Scp173AddedObserverEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173AddedObserverEventArgs"/> class.
    /// </summary>
    /// <param name="target">The player that is observing the SCP-173 player.</param>
    /// <param name="player">The SCP-173 player instance.</param>
    public Scp173AddedObserverEventArgs(Player target, Player player)
    {
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
}