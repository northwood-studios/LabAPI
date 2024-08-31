using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.BlackedOutZone"/> event.
/// </summary>
public class Scp079BlackedOutZoneEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079BlackedOutRoomEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="zone">The affected zone instance.</param>
    public Scp079BlackedOutZoneEventArgs(Player player, FacilityZone zone)
    {
        Player = player;
        Zone = zone;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected <see cref="FacilityZone"/> type.
    /// </summary>
    public FacilityZone Zone { get; }
}
