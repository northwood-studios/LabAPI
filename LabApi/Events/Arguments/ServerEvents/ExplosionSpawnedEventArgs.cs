using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ExplosionSpawned"/> event.
/// </summary>
public class ExplosionSpawnedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExplosionSpawnedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player which caused this explosion.</param>
    /// <param name="position">The position of explosion.</param>
    /// <param name="settingsReference">The projectile which caused explosion.</param>
    /// <param name="explosionType">The type of this explosion.</param>
    /// <param name="destroyDoors">Whether the explosion was allowed to destroy doors.</param>
    public ExplosionSpawnedEventArgs(ReferenceHub? hub, Vector3 position, ExplosionGrenade settingsReference, ExplosionType explosionType, bool destroyDoors)
    {
        Player = Player.Get(hub);
        Position = position;
        Settings = settingsReference;
        ExplosionType = explosionType;
        DestroyDoors = destroyDoors;
    }

    /// <summary>
    /// Gets the player which caused this explosion.
    /// </summary>
    public Player? Player { get; }

    /// <summary>
    /// Gets the position of explosion.
    /// </summary>
    public Vector3 Position { get; }

    /// <summary>
    /// Gets the projectile which will cause explosion.
    /// </summary>
    public ExplosionGrenade Settings { get; }

    /// <summary>
    /// Gets the type of this explosion.
    /// </summary>
    public ExplosionType ExplosionType { get; }

    /// <summary>
    /// Gets whether the explosion should destroy doors.
    /// </summary>
    public bool DestroyDoors { get; }
}
