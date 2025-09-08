using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Contains the arguments for the <see cref="Handlers.ServerEvents.RoundEnding"/> event.
/// </summary>
public class RoundEndingConditionsCheckEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoundEndingConditionsCheckEventArgs"/> class.
    /// </summary>
    /// <param name="canEnd">Whether the round end conditions are met.</param>
    public RoundEndingConditionsCheckEventArgs(bool canEnd)
    {
        CanEnd = canEnd;
    }

    /// <summary>
    /// Gets or sets whether the round end conditions are met.
    /// </summary>
    public bool CanEnd { get; set; }
}
