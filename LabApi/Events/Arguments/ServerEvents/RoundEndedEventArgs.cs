using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Contains the arguments for the <see cref="Handlers.ServerEvents.RoundEnded"/> event.
/// </summary>
public class RoundEndedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoundEndedEventArgs"/> class.
    /// </summary>
    /// <param name="leadingTeam">The leading team of the round.</param>
    public RoundEndedEventArgs(RoundSummary.LeadingTeam leadingTeam)
    {
        ShowSummary = true;
        LeadingTeam = leadingTeam;
    }
    
    /// <summary>
    /// The team that is leading or winning the round.
    /// </summary>
    public RoundSummary.LeadingTeam LeadingTeam { get; }
    
    /// <summary>
    /// Whether to show the round summary.
    /// </summary>
    public bool ShowSummary { get; set; }
}