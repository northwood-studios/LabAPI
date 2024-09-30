using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Facility;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Banning"/> event.
/// </summary>
public class PlayerBanningEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerBanningEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is getting the ban.</param>
    /// <param name="issuer">The player who issued the ban.</param>
    /// <param name="reason">The reason of the ban.</param>
    /// <param name="duration">The duration of the ban.</param>
    public PlayerBanningEventArgs(ReferenceHub player, ReferenceHub issuer, string reason, long duration)
    {
        IsAllowed = true;
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
    /// Gets or sets the reason of the ban.
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// Gets or sets the duration of the ban.
    /// </summary>
    public long Duration { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}