using InventorySystem.Items.Coin;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.FlippedCoin"/> event.
/// </summary>
public class PlayerFlippedCoinEventArgs : EventArgs, IPlayerEvent, ICoinItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerFlippingCoinEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who flipped the coin.</param>
    /// <param name="coin">The coin that was flipped.</param>
    /// <param name="isTails">Whenever the coin flip is tails.</param>
    public PlayerFlippedCoinEventArgs(ReferenceHub player, Coin coin, bool isTails)
    {
        Player = Player.Get(player);
        CoinItem = CoinItem.Get(coin);
        IsTails = isTails;
    }

    /// <summary>
    /// The player who flipped the coin.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the coin item which was flipped.
    /// </summary>
    public CoinItem CoinItem { get; }

    /// <summary>
    /// Whenever the coin flip is tails.
    /// </summary>
    public bool IsTails { get; }

    /// <inheritdoc cref="CoinItem"/>
    [Obsolete($"Use {nameof(CoinItem)} instead")]
    public Item Item => CoinItem;
}