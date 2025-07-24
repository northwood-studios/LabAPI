using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.Teleporting"/> event.
/// </summary>
public class Scp173TeleportingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173TeleportingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-173 player.</param>
    /// <param name="position">The target position to teleport to.</param>
    public Scp173TeleportingEventArgs(ReferenceHub hub, Vector3 position)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the target player position to teleport SCP-173 player to.<para/>
    /// Note that this position is on the floor and the <see cref="Player"/> is then teleported 1/2 of its character height up to prevent clipping through floor.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
