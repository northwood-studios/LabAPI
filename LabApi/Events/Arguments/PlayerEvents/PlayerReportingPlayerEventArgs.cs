using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReportingPlayer"/> event.
/// </summary>
public class PlayerReportingPlayerEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReportingPlayerEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is reporting.</param>
    /// <param name="target">The player who is being reported.</param>
    /// <param name="reason">The reason why was player reported.</param>
    public PlayerReportingPlayerEventArgs(ReferenceHub player, ReferenceHub target, string reason)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Target = Player.Get(target);
        Reason = reason;
    }

    /// <summary>
    /// Gets the player who is reporting.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who is being reported.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets the reason why was player reported.
    /// </summary>
    public string Reason { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}