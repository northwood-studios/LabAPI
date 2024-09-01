using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.ChangedStalkMode"/> event.
/// </summary>
public class Scp106ChangedStalkModeEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106ChangedStalkModeEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-106 player instance.</param>
    /// <param name="active">Whether the ability was activated or deactivated.</param>
    public Scp106ChangedStalkModeEventArgs(Player player, bool active)
    {
        Player = player;
        IsStalkActive = active;
    }
    
    /// <summary>
    /// Whether the ability was activated or deactivated.
    /// </summary>
    public bool IsStalkActive { get; }
    
    /// <summary>
    /// The SCP-106 player instance.
    /// </summary>
    public Player Player { get; }
}