﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.BlackingOutZone"/> event.
/// </summary>
public class Scp079BlackingOutZoneEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079BlackingOutZoneEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-079 player instance.</param>
    /// <param name="zone">The affected zone instance.</param>
    public Scp079BlackingOutZoneEventArgs(ReferenceHub hub, FacilityZone zone)
    {
        Player = Player.Get(hub);
        Zone = zone;
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected <see cref="FacilityZone"/> type.
    /// </summary>
    public FacilityZone Zone { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
