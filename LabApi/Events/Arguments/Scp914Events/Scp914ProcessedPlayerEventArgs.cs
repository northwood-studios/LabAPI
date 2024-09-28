using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when a player is processed by SCP-914.
/// </summary>
public class Scp914ProcessedPlayerEventArgs : EventArgs, IScp914Event, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ProcessedPlayerEventArgs"/> class.
    /// </summary>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="newPosition">The new position that the player has been processed into.</param>
    /// <param name="player">The player that has been processed by SCP-914.</param>
    public Scp914ProcessedPlayerEventArgs(Vector3 newPosition, Scp914KnobSetting knobSetting, ReferenceHub player)
    {
        NewPosition = newPosition;
        KnobSetting = knobSetting;
        Player = Player.Get(player);
    }

    /// <summary>
    /// The new position that the player has been processed into.
    /// </summary>
    public Vector3 NewPosition { get; }

    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; }

    /// <summary>
    /// The player that has been processed by SCP-914.
    /// </summary>
    public Player Player { get; }
}