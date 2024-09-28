using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Facility;

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
    /// <param name="player">The player who is getting the ban.</param>
    /// <param name="issuer">The player who issued the ban.</param>
    /// <param name="reason">The reason of the ban.</param>
    /// <param name="duration">The duration of the ban.</param>
    public PlayerBannedEventArgs(ReferenceHub player, ReferenceHub issuer, string reason, long duration)
    {
        Player = Player.Get(player);
        Issuer = Player.Get(issuer);
        Reason = reason;
        Duration = duration;
    }

    /// <summary>
    /// Gets the player who is being banned.
    /// </summary>
    public Player Player { get; }

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