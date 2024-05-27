using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.BreakneckSpeedChanged"/> event.
/// </summary>
public class Scp173BreakneckSpeedChangedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173BreakneckSpeedChangedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-173 player instance.</param>
    /// <param name="active">The new breakneck speed state.</param>
    public Scp173BreakneckSpeedChangedEventArgs(Player player, bool active)
    {
        Player = player;
        Active = active;
    }
    
    /// <summary>
    /// Whether the SCP-173 player is currently in breakneck speed mode.
    /// </summary>
    public bool Active { get; }
    
    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }
}