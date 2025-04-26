﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.AddedTarget"/> event.
/// </summary>
public class Scp096AddedTargetEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096AddedTargetEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    /// <param name="target">The target player instance.</param>
    /// <param name="wasLooking">Whether the target looked at SCP-096.</param>
    public Scp096AddedTargetEventArgs(ReferenceHub player, ReferenceHub target, bool wasLooking)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
        WasLooking = wasLooking;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target player instance.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Whether the target was looking at SCP-096
    /// </summary>
    public bool WasLooking { get; }
}
