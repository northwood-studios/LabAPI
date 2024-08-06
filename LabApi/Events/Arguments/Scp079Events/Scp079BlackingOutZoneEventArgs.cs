using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;
using MapGeneration;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.BlackingOutZone"/> event.
/// </summary>
public class Scp079BlackingOutZoneEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079BlackingOutZoneEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="zone">The affected zone instance.</param>
    public Scp079BlackingOutZoneEventArgs(Player player, FacilityZone zone)
    {
        Player = player;
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
