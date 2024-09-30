using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Spawning"/> event.
/// </summary>
public class PlayerSpawningEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    Vector3 _spawnLocation;
    float _horizontalRotation;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawningEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is being spawned.</param>
    /// <param name="role">The role that is being applied.</param>
    /// <param name="useSpawnPoint">If spawnpoint should be used.</param>
    /// <param name="spawnLocation">The default spawn location.</param>
    /// <param name="horizontalRotation">The default spawn horizontal rotation.</param>
    public PlayerSpawningEventArgs(ReferenceHub player, PlayerRoleBase role, bool useSpawnPoint, Vector3 spawnLocation, float horizontalRotation)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Role = role;
        UseSpawnPoint = useSpawnPoint;
        _spawnLocation = spawnLocation;
        _horizontalRotation = horizontalRotation;
    }

    /// <summary>
    /// Gets the player who is being spawned.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the role that is being applied.
    /// </summary>
    public PlayerRoleBase Role { get; }

    /// <summary>
    /// Gets or sets if spawn point should be used.
    /// </summary>
    public bool UseSpawnPoint { get; set; }

    /// <summary>
    /// Gets or sets spawn location.
    /// </summary>
    public Vector3 SpawnLocation
    {
        get => _spawnLocation;
        set
        {
            UseSpawnPoint = true;

            Role.ServerSpawnFlags |= RoleSpawnFlags.UseSpawnpoint;

            _spawnLocation = value;
        }
    }

    /// <summary>
    /// Gets or sets horizontal rotation of spawn.
    /// </summary>
    public float HorizontalRotation
    {
        get => _horizontalRotation;
        set
        {
            UseSpawnPoint = true;

            Role.ServerSpawnFlags |= RoleSpawnFlags.UseSpawnpoint;

            _horizontalRotation = value;
        }
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// Sets custom spawnpoint where player should spawn.
    /// </summary>
    /// <param name="position">The location of spawnpoint.</param>
    /// <param name="horizontalRotation">The rotation of spawn.</param>
    public void SetSpawnpoint(Vector3 position, float horizontalRotation = 0f)
    {
        UseSpawnPoint = true;

        Role.ServerSpawnFlags |= RoleSpawnFlags.UseSpawnpoint;

        _spawnLocation = position;
        _horizontalRotation = horizontalRotation;
    }
}