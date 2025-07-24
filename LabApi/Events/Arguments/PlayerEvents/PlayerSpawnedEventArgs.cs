using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Spawned"/> event.
/// </summary>
public class PlayerSpawnedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawnedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is being spawned.</param>
    /// <param name="role">The role that is being applied.</param>
    /// <param name="useSpawnPoint">If spawnpoint should be used.</param>
    /// <param name="spawnLocation">The default spawn location.</param>
    /// <param name="horizontalRotation">The default spawn horizontal rotation.</param>
    public PlayerSpawnedEventArgs(ReferenceHub hub, PlayerRoleBase role, bool useSpawnPoint, Vector3 spawnLocation, float horizontalRotation)
    {
        Player = Player.Get(hub);
        Role = role;
        UseSpawnPoint = useSpawnPoint;
        SpawnLocation = spawnLocation;
        HorizontalRotation = horizontalRotation;
    }

    /// <summary>
    /// Gets the player who spawned.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the role that was applied.
    /// </summary>
    public PlayerRoleBase Role { get; }

    /// <summary>
    /// Gets or sets if spawn point should be used.
    /// </summary>
    public bool UseSpawnPoint { get; }

    /// <summary>
    /// Gets spawn location.
    /// </summary>
    public Vector3 SpawnLocation { get; }

    /// <summary>
    /// Gets horizontal rotation of spawn.
    /// </summary>
    public float HorizontalRotation { get; }
}