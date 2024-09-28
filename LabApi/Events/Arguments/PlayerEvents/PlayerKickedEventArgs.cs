using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.OnKicked"/> event.
/// </summary>
public class PlayerKickedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerKickedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who was kicked.</param>
    /// <param name="issuer">The player who issued the kick.</param>
    /// <param name="reason">The reason for which is player being kicked.</param>
    public PlayerKickedEventArgs(ReferenceHub player, ReferenceHub issuer, string reason)
    {
        Player = Player.Get(player);
        Issuer = Player.Get(issuer);
        Reason = reason;
    }

    /// <summary>
    /// Gets the player who was kicked.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who issued the kick.
    /// </summary>
    public Player Issuer { get; }

    /// <summary>
    /// Gets the reason for which is player being kicked.
    /// </summary>
    public string Reason { get; }
}
