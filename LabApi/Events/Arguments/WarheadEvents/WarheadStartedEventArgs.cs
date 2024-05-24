using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.WarheadEvents;

/// <summary>
/// Represents the event arguments for the <see cref="Handlers.WarheadEvents.Started"/> event.
/// </summary>
public class WarheadStartedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarheadStartedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who started the warhead.</param>
    /// <param name="isAutomatic">Whether the warhead is starting automatically.</param>
    /// <param name="suppressSubtitles">Whether subtitles should be suppressed.</param>
    /// <param name="warheadState">The current state of the alpha warhead.</param>
    public WarheadStartedEventArgs(Player player, bool isAutomatic, bool suppressSubtitles, AlphaWarheadSyncInfo warheadState)
    {
        Player = player;
        IsAutomatic = isAutomatic;
        SuppressSubtitles = suppressSubtitles;
        WarheadState = warheadState;
    }
    
    /// <summary>
    /// Whether the warhead is starting automatically.
    /// </summary>
    public bool IsAutomatic { get; }
    
    /// <summary>
    /// Whether subtitles should be suppressed.
    /// </summary>
    public bool SuppressSubtitles { get; }
    
    /// <summary>
    /// Gets the current state of the alpha warhead.
    /// </summary>
    public AlphaWarheadSyncInfo WarheadState { get; }
    
    /// <summary>
    /// Gets the player who started the warhead.
    /// </summary>
    public Player Player { get; }
}