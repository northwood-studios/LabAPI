using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.WarheadEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.WarheadEvents.Detonating"/> event.
/// </summary>
public class WarheadDetonatingEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarheadDetonatingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is detonating the warhead.</param>
    public WarheadDetonatingEventArgs(Player player)
    {
        IsAllowed = true;
        Player = player;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// Gets the player who is detonating the warhead.
    /// </summary>
    public Player Player { get; set; }
}