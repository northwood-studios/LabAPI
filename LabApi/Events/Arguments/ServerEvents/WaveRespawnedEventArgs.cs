using LabApi.Features.Wrappers;
using Respawning.Waves;
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
    /// <param name="wave">The wave that is respawning.</param>
    /// <param name="players">The players that were respawned.</param>
    public WaveRespawnedEventArgs(SpawnableWaveBase wave, List<Player> players)
    {
        Players = players;
        Wave = RespawnWaves.Get(wave);
    }

    /// <summary>
    /// Team wave is respawning.
    /// </summary>
    public RespawnWave? Wave { get; }

    /// <summary>
    /// The players that were respawned.<br/>
    /// Be aware that every plugin share the same instance.
    /// </summary>
    public IReadOnlyList<Player> Players { get; }
}