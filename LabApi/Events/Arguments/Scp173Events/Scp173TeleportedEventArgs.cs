using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.Teleported"/> event.
/// </summary>
public class Scp173TeleportedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173TeleportingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-173 player.</param>
    /// <param name="position">The target position to teleport to.</param>
    public Scp173TeleportedEventArgs(ReferenceHub hub, Vector3 position)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Position = position;
    }

    /// <summary>
    /// Gets the SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the target player position that the SCP-173 has teleported to.
    /// Note that this position is on the floor and the <see cref="Player"/> has been teleported 1/2 of its character height up to prevent clipping through floor.
    /// </summary>
    public Vector3 Position { get; }

    /// <summary>
    /// Gets or sets whether the SCP-173 player can teleport.<para/>
    /// This even is fired even if the charge ability is not ready so you may override it on the server aswell.
    /// </summary>
    public bool IsAllowed { get; set; }
}
