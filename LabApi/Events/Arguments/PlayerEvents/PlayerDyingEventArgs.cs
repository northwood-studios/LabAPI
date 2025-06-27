using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Dying"/> event.
/// </summary>
public class PlayerDyingEventArgs : EventArgs, IPlayerEvent, IDamageEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDyingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is dying.</param>
    /// <param name="attacker">The player who attacked.</param>
    /// <param name="damageHandler">The damage handler who is causing death.</param>
    public PlayerDyingEventArgs(ReferenceHub player, ReferenceHub? attacker, DamageHandlerBase damageHandler)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Attacker = Player.Get(attacker);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who is dying.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who attacked.
    /// </summary>
    public Player? Attacker { get; }

    /// <summary>
    /// Gets the damage handler who is causing death.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}