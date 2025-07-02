using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Ragdolls;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SpawningRagdoll"/> event.
/// </summary>
public class PlayerSpawningRagdollEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpawningRagdollEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player from who is ragdoll from.</param>
    /// <param name="ragdoll">The ragdoll which being spawned.</param>
    /// <param name="damageHandler">The damage handler that caused the death of the player.</param>
    public PlayerSpawningRagdollEventArgs(ReferenceHub player, BasicRagdoll ragdoll, DamageHandlerBase damageHandler)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        RagdollPrefab = Ragdoll.Get(ragdoll);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player from who is ragdoll from.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the ragdoll prefab which will be used to instantiate the ragdoll.
    /// </summary>
    [Obsolete($"use {nameof(RagdollPrefab)} instead.")]
    public Ragdoll Ragdoll => RagdollPrefab;

    /// <summary>
    /// Gets the ragdoll prefab which will be used to instantiate the ragdoll.
    /// </summary>
    public Ragdoll RagdollPrefab { get; }

    /// <summary>
    /// Gets the damage handler that caused the death of the player.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}