using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Muting"/> event.
/// </summary>
public class PlayerMutingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMutingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is being muted.</param>
    /// <param name="issuer">The player who is issuing the mute.</param>
    /// <param name="isIntercom">Whenever mute is being applied to intercom.</param>
    public PlayerMutingEventArgs(ReferenceHub hub, ReferenceHub issuer, bool isIntercom)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Issuer = Player.Get(issuer);
        IsIntercom = isIntercom;
    }

    /// <summary>
    /// Gets the player who is being muted.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who is issuing the mute.
    /// </summary>
    public Player Issuer { get; }

    /// <summary>
    /// Gets whether mute is being applied to intercom.
    /// </summary>
    public bool IsIntercom { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}