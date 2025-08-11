using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.UsedHunterAtlas"/> event.
/// </summary>
public class Scp106UsedHunterAtlasEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106UsedHunterAtlasEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-106 player instance.</param>
    /// <param name="originalPosition">The original position.</param>
    public Scp106UsedHunterAtlasEventArgs(ReferenceHub player, Vector3 originalPosition)
    {
        Player = Player.Get(player);
        OriginalPosition = originalPosition;
    }

    /// <summary>
    /// The SCP-106 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The original position.
    /// </summary>
    public Vector3 OriginalPosition { get; }
}