using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.FlippingCoin"/> event.
/// </summary>
public class PlayerFlippingCoinEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerFlippingCoinEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is flipping the coin.</param>
    /// <param name="isTails">Whenever the coin flip is tails.</param>
    public PlayerFlippingCoinEventArgs(ReferenceHub player, bool isTails)
    {
        Player = Player.Get(player);
        IsTails = isTails;
    }

    /// <summary>
    /// Gets the player who is flipping the coin.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets whenever the coin flip is tails.
    /// </summary>
    public bool IsTails { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}