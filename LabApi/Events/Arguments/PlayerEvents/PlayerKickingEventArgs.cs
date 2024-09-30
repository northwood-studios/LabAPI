using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.OnKicking"/> event.
/// </summary>
public class PlayerKickingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerKickingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is being kicked.</param>
    /// <param name="issuer">The player who is issuing the kick.</param>
    /// <param name="reason">The reason for which is player being kicked.</param>
    public PlayerKickingEventArgs(ReferenceHub player, ReferenceHub issuer, string reason)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Issuer = Player.Get(issuer);
        Reason = reason;
    }

    /// <summary>
    /// Gets the player who is being kicked.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who is issuing the kick.
    /// </summary>
    public Player Issuer { get; }

    /// <summary>
    /// Gets the reason for which is player being kicked.
    /// </summary>
    public string Reason { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
