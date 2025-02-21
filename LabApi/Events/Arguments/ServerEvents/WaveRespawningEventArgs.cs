using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using Respawning.Waves;
using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveRespawning"/> event.
/// </summary>
public class WaveRespawningEventArgs : EventArgs, ICancellableEvent
{
    private readonly Dictionary<ReferenceHub, RoleTypeId> _roles;

    /// <summary>
    /// Initializes a new instance of the <see cref="WaveRespawningEventArgs"/> class.
    /// </summary>
    /// <param name="wave">The wave that is respawning.</param>
    /// <param name="roles">The players that are respawning and roles they will spawn as.</param>
    public WaveRespawningEventArgs(SpawnableWaveBase wave, Dictionary<ReferenceHub, RoleTypeId> roles)
    {
        IsAllowed = true;
        Wave = RespawnWaves.Get(wave);
        Roles = DictionaryPool<Player, RoleTypeId>.Get();

        foreach (KeyValuePair<ReferenceHub, RoleTypeId> kvp in roles)
            Roles.Add(Player.Get(kvp.Key), kvp.Value);
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// Team wave is respawning.
    /// </summary>
    public RespawnWave Wave { get; }

    /// <summary>
    /// Gets all players that are about to respawn.
    /// </summary>
    public IEnumerable<Player> SpawningPlayers => Roles.Keys;

    /// <summary>
    /// The dictionary containing players and their roles with will spawn with.<br/>
    /// <b>Note that this dictionary is pooled and will be returned to one after this event runs. Do not save it outside of this event's scope.</b>
    /// </summary>
    public Dictionary<Player, RoleTypeId> Roles { get; private set; }
}