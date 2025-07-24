using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Banned"/> event.
/// </summary>
public class PlayerBannedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerBannedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is getting the ban.</param>
    /// <param name="playerId">The ID of the player who is getting the ban.</param>
    /// <param name="issuer">The player who issued the ban.</param>
    /// <param name="reason">The reason of the ban.</param>
    /// <param name="duration">The duration of the ban.</param>
    public PlayerBannedEventArgs(ReferenceHub? hub, string playerId, ReferenceHub issuer, string reason, long duration)
    {
        Player = Player.Get(hub);
        PlayerId = playerId;
        Issuer = Player.Get(issuer);
        Reason = reason;
        Duration = duration;
    }

    /// <summary>
    /// Gets the player who is being banned.
    /// </summary>
    public Player? Player { get; }

    /// <summary>
    /// Gets the ID of the player who is being banned.
    /// </summary>
    public string PlayerId { get; }

    /// <summary>
    /// Gets the player who issued the ban.
    /// </summary>
    public Player Issuer { get; }

    /// <summary>
    /// Gets the reason of the ban.
    /// </summary>
    public string Reason { get; }

    /// <summary>
    /// Gets the duration of the ban.
    /// </summary>
    public long Duration { get; }
}