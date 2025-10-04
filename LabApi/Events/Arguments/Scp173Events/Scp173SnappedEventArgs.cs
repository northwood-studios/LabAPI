using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.Snapped"/> event.
/// </summary>
public class Scp173SnappedEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173SnappedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-173 player.</param>
    /// <param name="target">The player that has been snapped.</param>
    public Scp173SnappedEventArgs(ReferenceHub hub, ReferenceHub target)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
    }

    /// <summary>
    /// Gets the SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player that has been snapped.
    /// </summary>
    public Player Target { get; }
}
