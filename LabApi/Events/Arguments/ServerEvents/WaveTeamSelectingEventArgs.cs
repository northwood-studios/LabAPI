using LabApi.Events.Arguments.Interfaces;
using Respawning;
using Respawning.Waves;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveTeamSelecting"/> event.
/// </summary>
public class WaveTeamSelectingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaveTeamSelectingEventArgs"/> class.
    /// </summary>
    /// <param name="wave">The wave that is about to be selected.</param>
    public WaveTeamSelectingEventArgs(SpawnableWaveBase wave)
    {
        IsAllowed = true;
        Wave = wave;
    }

    /// <summary>
    /// Gets or sets the spawnable wave. See <see cref="SpawnableWaveBase"/> and its subclasses.<br/> 
    /// Use the <see cref="WaveManager.Waves"/> to set it to a different value.
    /// </summary>
    public SpawnableWaveBase Wave { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}