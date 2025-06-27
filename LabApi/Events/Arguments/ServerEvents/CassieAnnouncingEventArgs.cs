using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CassieAnnouncing"/> event.
/// </summary>
public class CassieAnnouncingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CassieAnnouncingEventArgs"/> class.
    /// </summary>
    /// <param name="words">The sentence C.A.S.S.I.E. is supposed to say.</param>
    /// <param name="makeHold">For most cases you wanna keep it true. Sets a minimal 3-second moment of silence before the announcement.</param>
    /// <param name="makeNoise">The background noises before playing.</param>
    /// <param name="customAnnouncement">If thats custom announcement? Custom announcements show subtitles</param>
    /// <param name="customSubtitles">Custom subtitles text to appear instead of original text.</param>
    public CassieAnnouncingEventArgs(string words, bool makeHold, bool makeNoise, bool customAnnouncement, string customSubtitles)
    {
        IsAllowed = true;
        Words = words;
        MakeHold = makeHold;
        MakeNoise = makeNoise;
        CustomAnnouncement = customAnnouncement;
        CustomSubtitles = customSubtitles;
    }

    /// <summary>
    /// Gets or sets sentece which C.A.S.S.I.E. will say.
    /// </summary>
    public string Words { get; set; }

    /// <summary>
    /// Gets or sets if announce should have delay.
    /// </summary>
    public bool MakeHold { get; set; }

    /// <summary>
    /// Gets or sets if announce should have background noise.
    /// </summary>
    public bool MakeNoise { get; set; }

    /// <summary>
    /// Gets or sets if announce should have custom subtitles.
    /// </summary>
    public bool CustomAnnouncement { get; set; }

    /// <summary>
    /// Gets or sets the custom subtitles text. Set to <see cref="string.Empty"/> or null if you wish keep the <see cref="Words"/> as subtitles text.
    /// </summary>
    public string CustomSubtitles { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
