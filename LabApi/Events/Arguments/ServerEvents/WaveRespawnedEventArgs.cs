using System;
using System.Collections.Generic;
using LabApi.Features.Wrappers;
using Respawning;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveRespawned"/> event.
/// </summary>
public class WaveRespawnedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaveRespawnedEventArgs"/> class.
    /// </summary>
    /// <param name="team">The team that was respawned.</param>
    /// <param name="players">The players that were respawned.</param>
    public WaveRespawnedEventArgs(SpawnableTeamType team, List<Player> players)
    {
        Team = team;
        Players = players;
    }
    
    /// <summary>
    /// The team that was respawned.
    /// </summary>
    public SpawnableTeamType Team { get; }
    
    /// <summary>
    /// The players that were respawned.
    /// </summary>
    public IReadOnlyList<Player> Players { get; }
}