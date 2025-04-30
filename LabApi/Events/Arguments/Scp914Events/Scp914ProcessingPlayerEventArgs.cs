using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when a player is being processed by SCP-914.
/// </summary>
public class Scp914ProcessingPlayerEventArgs : EventArgs, IScp914Event, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ProcessingPlayerEventArgs"/> class.
    /// </summary>
    /// <param name="newPosition">The new position that the player will be processed into.</param>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="player">The player that is being processed by SCP-914.</param>
    public Scp914ProcessingPlayerEventArgs(Vector3 newPosition, Scp914KnobSetting knobSetting, ReferenceHub player)
    {
        IsAllowed = true;
        NewPosition = newPosition;
        KnobSetting = knobSetting;
        Player = Player.Get(player);
    }

    /// <summary>
    /// The new position that the player will be processed into.
    /// </summary>
    public Vector3 NewPosition { get; set; }

    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; set; }

    /// <summary>
    /// The player that is being processed by SCP-914.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}