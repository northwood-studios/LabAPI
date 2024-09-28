using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Voice;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UsedIntercom"/> event.
/// </summary>
public class PlayerUsedIntercomEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUsedIntercomEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who used the intercom.</param>
    /// <param name="state">State of the intercom.</param>
    public PlayerUsedIntercomEventArgs(ReferenceHub player, IntercomState state)
    {
        Player = Player.Get(player);
        State = state;
    }

    /// <summary>
    /// Gets the player who used the intercom.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the state of the intercom.
    /// </summary>
    public IntercomState State { get; }
}