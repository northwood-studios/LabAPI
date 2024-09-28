using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.GrenadeExploding"/> event.
/// </summary>
public class GrenadeExplodingEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GreandeExplodingEventArgs"/> class.
    /// </summary>
    /// <param name="grenade">The grenade which will cause explosion.</param>
    /// <param name="player">The player which threw that grenade.</param>
    /// <param name="position">The position of explosion.</param>
    public GrenadeExplodingEventArgs(ExplosionGrenade grenade, ReferenceHub player, Vector3 position)
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
    /// Gets or sets position of explosion.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// Gets or sets if grenade should destroy doors.
    /// </summary>    
    public bool ExplodeDoors { get; set; } = true;

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
