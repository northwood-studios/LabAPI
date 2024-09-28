using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Spawning"/> event.
/// </summary>
public class PlayerSpawningEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawningEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is being spawned.</param>
    /// <param name="role">The role that is being applied.</param>
    public PlayerSpawningEventArgs(ReferenceHub player, RoleTypeId role)
    {
        Player = Player.Get(player);
        Role = role;
    }

    /// <summary>
    /// Gets the player who is being spawned.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the role that is being applied.
    /// </summary>
    public RoleTypeId Role { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}