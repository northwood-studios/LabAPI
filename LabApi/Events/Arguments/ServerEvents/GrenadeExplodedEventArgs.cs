using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.GrenadeExploded"/> event.
/// </summary>
public class GrenadeExplodedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GrenadeExplodedEventArgs"/> class.
    /// </summary>
    /// <param name="grenade">The grenade which will cause explosion.</param>
    /// <param name="player">The player which threw that grenade.</param>
    /// <param name="position">The position of explosion.</param>
    public GrenadeExplodedEventArgs(ExplosionGrenade grenade, ReferenceHub player, Vector3 position)
    {
        Grenade = grenade;
        Player = Player.Get(player);
        Position = position;
    }

    /// <summary>
    /// Gets grenade which caused this explosion.
    /// </summary>
    public ExplosionGrenade Grenade { get; }

    /// <summary>
    /// Gets who threw this grenade.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets position of explosion.
    /// </summary>
    public Vector3 Position { get; }

    /// <summary>
    /// Gets if grenade should destroy doors.
    /// </summary>    
    public bool ExplodeDoors { get; }
}
