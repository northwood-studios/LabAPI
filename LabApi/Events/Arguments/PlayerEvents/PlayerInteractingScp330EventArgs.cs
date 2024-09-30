using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using CustomPlayerEffects;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingScp330"/> event.
/// </summary>
public class PlayerInteractingScp330EventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractingScp330EventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is interacting with SCP-330.</param>
    /// <param name="uses">The amount of uses that target player did.</param>
    /// <param name="playSound">Whenever the sound should be played of pickup up candy.</param>
    /// <param name="allowPunishment">Whenever the <see cref="SeveredHands"/> effect should be applied.</param>
    public PlayerInteractingScp330EventArgs(ReferenceHub player, int uses, bool playSound, bool allowPunishment)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Uses = uses;
        PlaySound = playSound;
        AllowPunishment = allowPunishment;
    }

    /// <summary>
    /// Gets the player who is interacting with SCP-330.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the amount of uses that target player did.
    /// </summary>
    public int Uses { get; }

    /// <summary>
    /// Gets whether the sound should be played of pickup up candy.
    /// </summary>
    public bool PlaySound { get; set; }

    /// <summary>
    /// Gets whether the <see cref="SeveredHands"/> effect should be applied.
    /// </summary>
    public bool AllowPunishment { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}