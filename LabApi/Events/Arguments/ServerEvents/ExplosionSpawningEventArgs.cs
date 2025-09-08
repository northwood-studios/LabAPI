﻿using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.ServerEvents
{
    /// <summary>
    /// Represents the arguments for the <see cref="Handlers.ServerEvents.ExplosionSpawning"/> event.
    /// </summary>
    public class ExplosionSpawningEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExplosionSpawningEventArgs"/> class.
        /// </summary>
        /// <param name="hub">The player which caused this explosion.</param>
        /// <param name="position">The position of explosion.</param>
        /// <param name="settingsReference">The projectile which will cause the explosion.</param>
        /// <param name="explosionType">The type of this explosion.</param>
        /// <param name="destroyDoors">Whether the explosion should destroy doors.</param>
        public ExplosionSpawningEventArgs(ReferenceHub? hub, Vector3 position, ExplosionGrenade settingsReference, ExplosionType explosionType, bool destroyDoors)
        {
            Player = Player.Get(hub);
            Position = position;
            Settings = settingsReference;
            ExplosionType = explosionType;
            DestroyDoors = destroyDoors;

            IsAllowed = true;
        }

        /// <summary>
        /// Gets or sets the player which caused this explosion.
        /// </summary>
        public Player? Player { get; set; }

        /// <summary>
        /// Gets or sets the position of explosion.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the projectile which will cause explosion.
        /// </summary>
        public ExplosionGrenade Settings { get; set; }

        /// <summary>
        /// Gets or sets the type of this explosion.
        /// </summary>
        public ExplosionType ExplosionType { get; set; }

        /// <summary>
        /// Gets or sets whether the explosion should destroy doors.
        /// </summary>
        public bool DestroyDoors { get; set; }

        /// <inheritdoc/>
        public bool IsAllowed { get; set; }
    }
}
