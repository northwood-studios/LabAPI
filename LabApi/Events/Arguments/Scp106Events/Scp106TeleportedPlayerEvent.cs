using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.TeleportedPlayer"/> event.
/// </summary>
public class Scp106TeleportedPlayerEvent : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106TeleportedPlayerEvent"/> class.
    /// </summary>
    /// <param name="hub">The SCP-106 player instance.</param>
    /// <param name="target">The player that was teleported.</param>
    public Scp106TeleportedPlayerEvent(ReferenceHub hub, ReferenceHub target)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
    }

    /// <summary>
    /// Gets the SCP-106 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player that was teleported.
    /// </summary>
    public Player Target { get; }
}