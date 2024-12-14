using LabApi.Events.Arguments.Interfaces;
using Respawning;
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
    /// <param name="chosenTeam">The team that is being selected.</param>
    public WaveTeamSelectingEventArgs()
    {
        IsAllowed = true;
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}