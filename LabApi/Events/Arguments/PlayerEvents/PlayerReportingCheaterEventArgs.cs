using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReportingCheater"/> event.
/// </summary>
public class PlayerReportingCheaterEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReportingCheaterEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is reporting.</param>
    /// <param name="target">The reported player.</param>
    /// <param name="reason">The reason why is the player being reported.</param>
    public PlayerReportingCheaterEventArgs(ReferenceHub player, ReferenceHub target, string reason)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
        Reason = reason;
    }

    /// <summary>
    /// Gets the player who is reporting.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the reported player.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets the reason why is player being reported.
    /// </summary>
    public string Reason { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}