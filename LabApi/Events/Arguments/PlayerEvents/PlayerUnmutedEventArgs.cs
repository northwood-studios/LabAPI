using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Unmuted"/> event.
/// </summary>
public class PlayerUnmutedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnmutedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who was unmuted.</param>
    /// <param name="issuer">The player who issued the unmute.</param>
    /// <param name="isIntercom">Whenever the unmute was for intercom.</param>
    public PlayerUnmutedEventArgs(ReferenceHub hub, ReferenceHub issuer, bool isIntercom)
    {
        Player = Player.Get(hub);
        Issuer = Player.Get(issuer);
        IsIntercom = isIntercom;
    }

    /// <summary>
    /// Gets the player who was unmuted.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who issued the unmute.
    /// </summary>
    public Player Issuer { get; }

    /// <summary>
    /// Gets whether the unmute was for intercom.
    /// </summary>
    public bool IsIntercom { get; }
}