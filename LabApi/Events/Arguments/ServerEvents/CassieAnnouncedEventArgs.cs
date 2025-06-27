using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CassieAnnounced"/> event.
/// </summary>
public class CassieAnnouncedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CassieAnnouncedEventArgs"/> class.
    /// </summary>
    /// <param name="words">The sentence C.A.S.S.I.E. is supposed to say.</param>
    /// <param name="makeHold">For most cases you wanna keep it true. Sets a minimal 3-second moment of silence before the announcement.</param>
    /// <param name="makeNoise">The background noises before playing.</param>
    /// <param name="customAnnouncement">If thats custom announcement? Custom announcements show subtitles</param>
    /// <param name="customSubtitles">Custom subtitles text.</param>
    public CassieAnnouncedEventArgs(string words, bool makeHold, bool makeNoise, bool customAnnouncement, string customSubtitles)
    {
        Words = words;
        MakeHold = makeHold;
        MakeNoise = makeNoise;
        CustomAnnouncement = customAnnouncement;
        CustomSubtitles = customSubtitles;
    }

    /// <summary>
    /// Gets sentece which C.A.S.S.I.E. said.
    /// </summary>
    public string Words { get; }

    /// <summary>
    /// Gets if announce had delay.
    /// </summary>
    public bool MakeHold { get; }

    /// <summary>
    /// Gets if announce had background noises.
    /// </summary>
    public bool MakeNoise { get; }

    /// <summary>
    /// Gets if announce had custom subtitles.
    /// </summary>
    public bool CustomAnnouncement { get; }

    /// <summary>
    /// Gets the custom subtitles with the announcement.
    /// </summary>
    public string CustomSubtitles { get; }
}
