using System;
using LabApi.Events.Arguments.Interfaces;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Contains the arguments for the <see cref="Handlers.ServerEvents.RoundEnding"/> event.
/// </summary>
public class RoundEndingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoundEndingEventArgs"/> class.
    /// </summary>
    /// <param name="leadingTeam">The leading team of the round.</param>
    public RoundEndingEventArgs(RoundSummary.LeadingTeam leadingTeam)
    {
        IsAllowed = true;
        LeadingTeam = leadingTeam;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
    
    /// <summary>
    /// The team that is leading or winning the round.
    /// </summary>
    public RoundSummary.LeadingTeam LeadingTeam { get; set; }
}