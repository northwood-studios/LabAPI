using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.UsingHunterAtlas"/> event.
/// </summary>
public class Scp106UsingHunterAtlasEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106UsingHunterAtlasEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-106 player instance.</param>
    /// <param name="destinationPosition">The destination position.</param>
    public Scp106UsingHunterAtlasEventArgs(ReferenceHub player, Vector3 destinationPosition)
    {
        Player = Player.Get(player);
        DestinationPosition = destinationPosition;
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-106 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The destination position.
    /// </summary>
    public Vector3 DestinationPosition { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}