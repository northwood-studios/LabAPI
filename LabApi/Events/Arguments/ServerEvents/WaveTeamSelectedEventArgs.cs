using System;
using Respawning;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveTeamSelected"/> event.
/// </summary>
public class WaveTeamSelectedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaveTeamSelectedEventArgs"/> class.
    /// </summary>
    /// <param name="chosenTeam">The team that was selected.</param>
    public WaveTeamSelectedEventArgs(SpawnableTeamType chosenTeam)
    {
        ChosenTeam = chosenTeam;
    }
    
    /// <summary>
    /// The team that was selected.
    /// </summary>
    public SpawnableTeamType ChosenTeam { get; }
}