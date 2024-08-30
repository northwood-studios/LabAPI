using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.UsedSense"/> event.
/// </summary>
public class Scp049UsedSenseEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049UsedSenseEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-049 player instance.</param>
    /// <param name="target">The player that SCP-049 has used sense on.</param>
    public Scp049UsedSenseEventArgs(Player player, Player target)
    {
        Player = player;
        Target = target;
    }
    
    /// <summary>
    /// The player that SCP-049 has used sense on.
    /// </summary>
    public Player Target { get;}
    
    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }
}