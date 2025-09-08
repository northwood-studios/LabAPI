using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.WarheadEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.WarheadEvents.Starting"/> event.
/// </summary>
public class WarheadStartingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarheadStartingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is starting the warhead.</param>
    /// <param name="isAutomatic">Whether the warhead is starting automatically.</param>
    /// <param name="suppressSubtitles">Whether subtitles should be suppressed.</param>
    /// <param name="warheadState">The current state of the alpha warhead.</param>
    public WarheadStartingEventArgs(ReferenceHub hub, bool isAutomatic, bool suppressSubtitles, AlphaWarheadSyncInfo warheadState)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        IsAutomatic = isAutomatic;
        SuppressSubtitles = suppressSubtitles;
        WarheadState = warheadState;
    }

    /// <summary>
    /// Whether the warhead is starting automatically.
    /// </summary>
    public bool IsAutomatic { get; set; }

    /// <summary>
    /// Whether subtitles should be suppressed.
    /// </summary>
    public bool SuppressSubtitles { get; set; }

    /// <summary>
    /// Gets the current state of the alpha warhead.
    /// </summary>
    public AlphaWarheadSyncInfo WarheadState { get; set; }

    /// <summary>
    /// Gets the player who is starting the warhead.
    /// </summary>
    public Player Player { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}