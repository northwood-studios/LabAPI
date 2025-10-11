using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.CheckedHitmarker"/> event.
/// </summary>
public class PlayerCheckedHitmarkerEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance for the <see cref="PlayerCheckedHitmarkerEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player that hitmarker should send.</param>
    /// <param name="adh">The damage dealt to the <paramref name="victim"/>.</param>
    /// <param name="victim">The player who got hurt by <paramref name="hub"/>.</param>
    /// <param name="result">The result of the check.</param>
    public PlayerCheckedHitmarkerEventArgs(ReferenceHub hub, AttackerDamageHandler adh, ReferenceHub victim, bool result)
    {
        Player = Player.Get(hub);
        DamageHandler = adh;
        Victim = Player.Get(victim);
        Result = result;
    }

    /// <summary>
    /// Gets the player that the hitmarker is being sent to.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player that was hit.
    /// </summary>
    public Player Victim { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public AttackerDamageHandler DamageHandler { get; }

    /// <summary>
    /// Gets the check result.
    /// </summary>
    public bool Result { get; }
}
