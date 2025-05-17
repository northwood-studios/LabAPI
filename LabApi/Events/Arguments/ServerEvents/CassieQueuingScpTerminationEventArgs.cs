using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Subtitles;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.CassieQueuingScpTermination"/> event.
/// </summary>
public class CassieQueuingScpTerminationEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CassieQueuingScpTerminationEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP player the C.A.S.S.I.E termination announcement is for.</param>
    /// <param name="announcement">The message C.A.S.S.I.E is supposed to say.</param>
    /// <param name="subtitles">The subtitle part array of the message.</param>
    public CassieQueuingScpTerminationEventArgs(ReferenceHub player, string announcement, SubtitlePart[] subtitles, DamageHandlerBase damageHandler)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Announcement = announcement;
        SubtitleParts = [.. subtitles];
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// The SCP player the C.A.S.S.I.E termination announcement is for.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the message C.A.S.S.I.E is supposed to say.
    /// </summary>
    public string Announcement { get; set; }

    /// <summary>
    /// Gets or sets the subtitle parts of the message.
    /// </summary>
    public SubtitlePart[] SubtitleParts { get; set; }    

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
    
    /// <summary>
    /// The Damage Handler responsible for the SCP Termination.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }
}
