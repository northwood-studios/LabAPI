using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Spawned"/> event.
/// </summary>
public class PlayerSpawnedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawnedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who spawned.</param>
    /// <param name="role">The role that was applied.</param>
    public PlayerSpawnedEventArgs(ReferenceHub player, RoleTypeId role)
    {
        Player = Player.Get(player);
        Role = role;
    }

    /// <summary>
    /// Gets the player who spawned.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the role that was applied.
    /// </summary>
    public RoleTypeId Role { get; set; }
}