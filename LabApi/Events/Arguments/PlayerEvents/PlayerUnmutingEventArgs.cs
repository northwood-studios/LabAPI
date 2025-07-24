using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Unmuting"/> event.
/// </summary>
public class PlayerUnmutingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnmutingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is being unmuted.</param>
    /// <param name="issuer">The player who issued the unmute action.</param>
    /// <param name="isIntercom">Whenever is unmute for intercom.</param>
    public PlayerUnmutingEventArgs(ReferenceHub hub, ReferenceHub issuer, bool isIntercom)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Issuer = Player.Get(issuer);
        IsIntercom = isIntercom;
    }

    /// <summary>
    /// Gets the player who is being unmuted.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who issued the unmute action.
    /// </summary>
    public Player Issuer { get; }

    /// <summary>
    /// Gets or sets whether is unmute for intercom.
    /// </summary>
    public bool IsIntercom { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}