using System;
using LabApi.Events.Arguments.Interfaces;
using Respawning;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveTeamSelecting"/> event.
/// </summary>
public class WaveTeamSelectingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaveTeamSelectingEventArgs"/> class.
    /// </summary>
    /// <param name="chosenTeam">The team that is being selected.</param>
    public WaveTeamSelectingEventArgs(SpawnableTeamType chosenTeam)
    {
        IsAllowed = true;
        ChosenTeam = chosenTeam;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
    
    /// <summary>
    /// The team that is being selected.
    /// </summary>
    public SpawnableTeamType ChosenTeam { get; set; }
}