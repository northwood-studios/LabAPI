using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Hurt"/> event.
/// </summary>
public class PlayerHurtEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerHurtEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who attacked.</param>
    /// <param name="target">The player who was attacked.</param>
    /// <param name="damageHandler">The damage handler.</param>
    public PlayerHurtEventArgs(ReferenceHub? player, ReferenceHub target, DamageHandlerBase damageHandler)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who attacked.
    /// </summary>
    public Player? Player { get; }

    /// <summary>
    /// Gets the player who was attacked.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }

    /// <summary>
    /// Gets the damage dealt if <see cref="DamageHandler"/> is <see cref="StandardDamageHandler"/>.
    /// </summary>
    public float Damage => DamageHandler is StandardDamageHandler sdh ? sdh.DealtHealthDamage : 0.0f;
}