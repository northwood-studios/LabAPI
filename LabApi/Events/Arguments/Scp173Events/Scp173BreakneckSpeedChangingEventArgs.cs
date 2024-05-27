using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.BreakneckSpeedChanging"/> event.
/// </summary>
public class Scp173BreakneckSpeedChangingEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173BreakneckSpeedChangingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-173 player instance.</param>
    /// <param name="active">The new breakneck speed state.</param>
    public Scp173BreakneckSpeedChangingEventArgs(Player player, bool active)
    {
        IsAllowed = true;
        Player = player;
        Active = active;
    }
    
    /// <summary>
    /// Whether the ability is being activated or deactivated.
    /// </summary>
    public bool Active { get; }
    
    /// <summary>
    /// The SCP-173 player instance. 
    /// </summary>
    public Player Player { get; }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}