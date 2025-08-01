using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ZoneChanged"/> event.
/// </summary>
public class PlayerZoneChangedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerZoneChangedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose zone changed.</param>
    /// <param name="oldZone">The old zone.</param>
    /// <param name="newZone">The new zone</param>
    public PlayerZoneChangedEventArgs(ReferenceHub player, FacilityZone oldZone, FacilityZone newZone)
    {
        Player = Player.Get(player);
        OldZone = oldZone;
        NewZone = newZone;
    }

    /// <summary>
    /// Gets the player whose last known room changed.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old zone. May be <see cref="FacilityZone.None"/> if the player just spawned, went through void and such.
    /// </summary>
    public FacilityZone OldZone { get; }

    /// <summary>
    /// Gets the new zone. May be <see cref="FacilityZone.None"/> if the player went into void, died and such.
    /// </summary>
    public FacilityZone NewZone { get; }
}
