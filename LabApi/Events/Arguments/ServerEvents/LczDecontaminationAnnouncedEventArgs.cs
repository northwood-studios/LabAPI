using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Contains the arguments for the <see cref="Handlers.ServerEvents.LczDecontaminationAnnounced"/> event.
/// </summary>
public class LczDecontaminationAnnouncedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LczDecontaminationAnnouncedEventArgs"/> class.
    /// </summary>
    /// <param name="phase">The phase of decontamination.</param>
    public LczDecontaminationAnnouncedEventArgs(int phase)
    {
        Phase = phase;
    }

    /// <summary>
    /// Gets phase of decontamination.
    /// </summary>
    public int Phase { get; }
}