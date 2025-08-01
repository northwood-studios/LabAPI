using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.WarheadEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.WarheadEvents.Detonating"/> event.
/// </summary>
public class WarheadDetonatingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarheadDetonatingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is detonating the warhead.</param>
    public WarheadDetonatingEventArgs(ReferenceHub hub)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// Gets the player who is detonating the warhead.
    /// </summary>
    public Player Player { get; set; }
}