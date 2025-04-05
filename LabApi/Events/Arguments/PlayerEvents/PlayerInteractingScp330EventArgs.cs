using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using CustomPlayerEffects;
using InventorySystem.Items.Usables.Scp330;

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
    /// <param name="type">Type of the candy which will be given to the player.</param>
    public PlayerInteractingScp330EventArgs(ReferenceHub player, int uses, bool playSound, bool allowPunishment, CandyKindID type)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Uses = uses;
        PlaySound = playSound;
        AllowPunishment = allowPunishment;
        CandyType = type;
    }

    /// <summary>
    /// Gets the player who is interacting with SCP-330.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the amount of uses that target player has used.
    /// </summary>
    public int Uses { get; set; }

    /// <summary>
    /// Gets whether the sound should be played of pickup up candy.
    /// </summary>
    public bool PlaySound { get; set; }

    /// <summary>
    /// Gets whether the <see cref="SeveredHands"/> effect should be applied.
    /// </summary>
    public bool AllowPunishment { get; set; }

    /// <summary>
    /// Gets or sets the type of the candy that is given to the player. 
    /// <para>
    /// Set <see cref="IsAllowed"/> or this to <see cref="CandyKindID.None"/> if you don't want to give candy to the player.
    /// </para>
    /// </summary>
    public CandyKindID CandyType { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
