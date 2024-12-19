using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;

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
    public WaveRespawnedEventArgs(Team team, List<Player> players)
    {
        Players = players;
        Team = team;
    }

    /// <summary>
    /// The team that respawned.
    /// </summary>
    public Team Team { get; }

    /// <summary>
    /// The players that were respawned.<br/>
    /// Be aware that every plugin share the same instance.
    /// </summary>
    public IReadOnlyList<Player> Players { get; }
}