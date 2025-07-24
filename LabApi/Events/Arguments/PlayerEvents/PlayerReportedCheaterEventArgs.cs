using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReportedCheater"/> event.
/// </summary>
public class PlayerReportedCheaterEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReportedCheaterEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who sent the report.</param>
    /// <param name="target">The reported player.</param>
    /// <param name="reason">The reason why is the player being reported.</param>
    public PlayerReportedCheaterEventArgs(ReferenceHub hub, ReferenceHub target, string reason)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
        Reason = reason;
    }

    /// <summary>
    /// Gets the player who sent the report.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the reported player.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets the reason why is the player being reported.
    /// </summary>
    public string Reason { get; }
}