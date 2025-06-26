using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Enums;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.Pinging"/> event.
/// </summary>
public class Scp079PingingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079PingingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="position">The world position of the ping.</param>
    /// <param name="normal">Normal vector for the ping.</param>
    /// <param name="index">The index of the ping type.</param>
    public Scp079PingingEventArgs(ReferenceHub player, Vector3 position, Vector3 normal, byte index)
    {
        Player = Player.Get(player);
        Position = position;
        Normal = normal;
        PingType = (Scp079PingType)index;
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the world ping position.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// Gets or sets the ping normal vector.
    /// </summary>
    public Vector3 Normal { get; set; }

    /// <summary>
    /// Gets or sets the type of the ping used the icon.
    /// </summary>
    public Scp079PingType PingType { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
