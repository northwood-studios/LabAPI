using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using LiteNetLib;
using Mirror.LiteNetLib4Mirror;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Left"/> event.
/// </summary>
public class PlayerLeftEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeftEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who left.</param>
    public PlayerLeftEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
        Reason = LiteNetLib4MirrorCore.LastDisconnectReason;
    }

    /// <summary>
    /// Gets the player who left.
    /// </summary>
    public Player Player { get; }
    
    /// <summary>
    /// Gets the reason why the player left.
    /// </summary>
    public DisconnectReason Reason { get; }
}