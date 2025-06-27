using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Hurting"/> event.
/// </summary>
public class PlayerHurtingEventArgs : EventArgs, IPlayerEvent, IDamageEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerHurtingEventArgs"/> class.
    /// </summary>
    /// <param name="attacker">The player who is attacking.</param>
    /// <param name="victim">The player who is being attacked.</param>
    /// <param name="damageHandler">The damage handler.</param>
    public PlayerHurtingEventArgs(ReferenceHub? attacker, ReferenceHub victim, DamageHandlerBase damageHandler)
    {
        IsAllowed = true;
        Player = Player.Get(victim);
        Attacker = Player.Get(attacker);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who is being hurt.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who is attacking.
    /// </summary>
    public Player? Attacker { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}