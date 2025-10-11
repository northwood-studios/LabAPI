using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.Snapping"/> event.
/// </summary>
public class Scp173SnappingEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173SnappingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-173 player.</param>
    /// <param name="target">The player to snap.</param>
    public Scp173SnappingEventArgs(ReferenceHub hub, ReferenceHub target)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Target = Player.Get(target);
    }

    /// <summary>
    /// Gets the SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the player to snap.
    /// </summary>
    public Player Target { get; set; }

    /// <summary>
    /// Gets or sets whether the SCP-173 player can snap the target.<para/>
    /// </summary>
    public bool IsAllowed { get; set; }
}
