using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Respawning;
using System;
using System.Collections.Generic;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveRespawning"/> event.
/// </summary>
public class WaveRespawningEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaveRespawningEventArgs"/> class.
    /// </summary>
    /// <param name="team">The team that is respawning.</param>
    /// <param name="players">The players that are respawning.</param>
    public WaveRespawningEventArgs(SpawnableTeamType team, List<Player> players)
    {
        IsAllowed = true;
        Team = team;
        Players = players;
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// The team that is respawning.
    /// </summary>
    public SpawnableTeamType Team { get; set; }

    /// <summary>
    /// The players that are respawning.
    /// </summary>
    public List<Player> Players { get; }
}