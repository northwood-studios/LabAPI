using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.TeleportingPlayer"/> event.
/// </summary>
public class Scp106TeleportingPlayerEvent : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106TeleportingPlayerEvent"/> class.
    /// </summary>
    /// <param name="hub">The SCP-106 player instance.</param>
    /// <param name="target">The player that is being teleported.</param>
    public Scp106TeleportingPlayerEvent(ReferenceHub hub, ReferenceHub target)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Target = Player.Get(target);
    }

    /// <summary>
    /// Gets the SCP-106 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player that is being teleported.
    /// </summary>
    public Player Target { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}