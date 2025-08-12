using CustomPlayerEffects;
using InventorySystem.Items.Usables.Scp330;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedScp330"/> event.
/// </summary>
public class PlayerInteractedScp330EventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedScp330EventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who interacted with SCP-330.</param>
    /// <param name="uses">The amount of uses that target player did.</param>
    /// <param name="playSound">Whenever pickup sound should have been.</param>
    /// <param name="allowPunishment">Whenever the <see cref="SeveredHands"/> effect was applied.</param>
    /// <param name="type">Type of the candy which was given to the player.</param>
    public PlayerInteractedScp330EventArgs(ReferenceHub hub, int uses, bool playSound, bool allowPunishment, CandyKindID type)
    {
        Player = Player.Get(hub);
        Uses = uses;
        PlaySound = playSound;
        AllowPunishment = allowPunishment;
        CandyType = type;
    }

    /// <summary>
    /// Gets the player who interacted with SCP-330.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the amount of uses that target player did.
    /// </summary>
    public int Uses { get; }

    /// <summary>
    /// Gets the boolean whenever pickup sound has been played.
    /// </summary>
    public bool PlaySound { get; }

    /// <summary>
    /// Gets the bool value whether the <see cref="SeveredHands"/> effect was applied.
    /// </summary>
    public bool AllowPunishment { get; }

    /// <summary>
    /// Gets the type of the candy that has been given to the player.
    /// </summary>
    public CandyKindID CandyType { get; }
}