using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Death"/> event.
/// </summary>
public class PlayerDeathEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDeathEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who died.</param>
    /// <param name="attacker">The player who caused the death.</param>
    /// <param name="damageHandler">The damage that caused the death.</param>
    public PlayerDeathEventArgs(ReferenceHub player, ReferenceHub attacker, DamageHandlerBase damageHandler)
    {
        Player = Player.Get(player);
        Attacker = Player.Get(attacker);
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who died.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who caused the death.
    /// </summary>
    public Player Attacker { get; }

    /// <summary>
    /// Gets the damage that caused the death.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }
}