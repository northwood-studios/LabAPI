using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Hurt"/> event.
/// </summary>
public class PlayerHurtEventArgs : EventArgs, IPlayerEvent, IDamageEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerHurtEventArgs"/> class.
    /// </summary>
    /// <param name="attacker">The player who attacked.</param>
    /// <param name="victim">The player who was hurt.</param>
    /// <param name="damageHandler">The damage handler.</param>
    public PlayerHurtEventArgs(ReferenceHub? attacker, ReferenceHub victim, DamageHandlerBase damageHandler)
    {
        Player = Player.Get(victim);
        Attacker = Player.Get(attacker);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who was hurt.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who attacked.
    /// </summary>
    public Player? Attacker { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }
}