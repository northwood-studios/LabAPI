﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.AddingTarget"/> event.
/// </summary>
public class Scp096AddingTargetEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096AddingTargetEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-096 player instance.</param>
    /// <param name="target">The target player instance.</param>
    /// <param name="wasLooking">Whether the target looked at SCP-096.</param>
    public Scp096AddingTargetEventArgs(ReferenceHub hub, ReferenceHub target, bool wasLooking)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
        WasLooking = wasLooking;
        IsAllowed = true;
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

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
