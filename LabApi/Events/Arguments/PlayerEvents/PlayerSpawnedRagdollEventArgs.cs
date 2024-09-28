using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SpawnedRagdoll"/> event.
/// </summary>
public class PlayerSpawnedRagdollEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawnedRagdollEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player from who is ragdoll from.</param>
    /// <param name="ragdoll">The spawned ragdoll.</param>
    /// <param name="damageHandler">The damage handler that caused the death of the player.</param>
    public PlayerSpawnedRagdollEventArgs(ReferenceHub player, Ragdoll ragdoll, DamageHandlerBase damageHandler)
    {
        Player = Player.Get(player);
        Ragdoll = ragdoll;
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player from who is ragdoll from.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the spawned ragdoll.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets the damage handler that caused the death of the player.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }
}