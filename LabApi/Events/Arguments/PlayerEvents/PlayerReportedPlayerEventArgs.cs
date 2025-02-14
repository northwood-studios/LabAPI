using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReportedPlayer"/> event.
/// </summary>
public class PlayerReportedPlayerEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReportedPlayerEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who sent reported.</param>
    /// <param name="target">The player who was reported.</param>
    /// <param name="reason">The reason why was the player reported.</param>
    public PlayerReportedPlayerEventArgs(ReferenceHub player, ReferenceHub target, string reason)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
        Reason = reason;
    }

    /// <summary>
    /// Gets the player who sent reported.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who was reported.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets the reason why was the player reported.
    /// </summary>
    public string Reason { get; }
}