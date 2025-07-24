using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Muted"/> event.
/// </summary>
public class PlayerMutedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMutedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who was muted.</param>
    /// <param name="issuer">The player who issued the mute.</param>
    /// <param name="isIntercom">Whenever mute was applied to intercom.</param>
    public PlayerMutedEventArgs(ReferenceHub hub, ReferenceHub issuer, bool isIntercom)
    {
        Player = Player.Get(hub);
        Issuer = Player.Get(issuer);
        IsIntercom = isIntercom;
    }

    /// <summary>
    /// Gets the player who was muted.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who issued the mute.
    /// </summary>
    public Player Issuer { get; }

    /// <summary>
    /// Gets whether mute was applied to intercom.
    /// </summary>
    public bool IsIntercom { get; }
}