using InventorySystem.Items.Coin;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.FlippingCoin"/> event.
/// </summary>
public class PlayerFlippingCoinEventArgs : EventArgs, IPlayerEvent, ICancellableEvent, ICoinItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerFlippingCoinEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is flipping the coin.</param>
    /// <param name="coin">The coin that is being flipped.</param>
    /// <param name="isTails">Whenever the coin flip is tails.</param>
    public PlayerFlippingCoinEventArgs(ReferenceHub player, Coin coin, bool isTails)
    {
        Player = Player.Get(player);
        CoinItem = CoinItem.Get(coin);
        IsTails = isTails;
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player who is flipping the coin.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the coin item that is going to be flipped.
    /// </summary>
    public CoinItem CoinItem { get; }

    /// <summary>
    /// Gets whenever the coin flip is tails.
    /// </summary>
    public bool IsTails { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="CoinItem"/>
    [Obsolete($"Use {nameof(CoinItem)} instead")]
    public Item Item => CoinItem;
}