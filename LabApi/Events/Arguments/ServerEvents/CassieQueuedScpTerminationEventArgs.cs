using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using Subtitles;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CassieQueuedScpTermination"/> event.
/// </summary>
public class CassieQueuedScpTerminationEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CassieQueuedScpTerminationEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP player the C.A.S.S.I.E termination announcement is for.</param>
    /// <param name="announcement">The message C.A.S.S.I.E is supposed to say.</param>
    /// <param name="subtitles">The subtitle part array of the message.</param>
    public CassieQueuedScpTerminationEventArgs(ReferenceHub player, string announcement, SubtitlePart[] subtitles, DamageHandlerBase damageHandler)
    {
        Player = Player.Get(player);
        Announcement = announcement;
        SubtitleParts = subtitles;
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// The SCP player the C.A.S.S.I.E termination announcement is for.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the message C.A.S.S.I.E is supposed to say.
    /// </summary>
    public string Announcement { get; }

    /// <summary>
    /// Gets or sets the subtitle parts of the message.
    /// </summary>
    public SubtitlePart[] SubtitleParts { get; }

    /// <summary>
    /// The Damage Handler responsible for the SCP Termination.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }
}
