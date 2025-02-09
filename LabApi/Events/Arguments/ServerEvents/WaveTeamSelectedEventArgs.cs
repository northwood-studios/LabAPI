using Respawning.Waves;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveTeamSelected"/> event.
/// </summary>
public class WaveTeamSelectedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaveTeamSelectedEventArgs"/> class.
    /// </summary>
    /// <param name="wave">The wave that was selected.</param>
    public WaveTeamSelectedEventArgs(SpawnableWaveBase wave)
    {
        Wave = wave;
    }

    /// <summary>
    /// Gets the spawnable wave. See <see cref="SpawnableWaveBase"/> and its subclasses for more info. 
    /// </summary>
    public SpawnableWaveBase Wave { get; }
}