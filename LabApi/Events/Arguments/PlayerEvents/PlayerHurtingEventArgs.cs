using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Hurting"/> event.
/// </summary>
public class PlayerHurtingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerHurtingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is attacking.</param>
    /// <param name="target">The player who is being attacked.</param>
    /// <param name="damageHandler">The damage handler.</param>
    public PlayerHurtingEventArgs(ReferenceHub player, ReferenceHub target, DamageHandlerBase damageHandler)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who is attacking.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who is being attacked.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}