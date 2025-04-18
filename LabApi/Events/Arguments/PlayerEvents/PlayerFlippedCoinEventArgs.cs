﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using InventorySystem.Items;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.FlippedCoin"/> event.
/// </summary>
public class PlayerFlippedCoinEventArgs : EventArgs, IPlayerEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerFlippingCoinEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who flipped the coin.</param>
    /// <param name="item">The coin that was flipped.</param>
    /// <param name="isTails">Whenever the coin flip is tails.</param>
    public PlayerFlippedCoinEventArgs(ReferenceHub player, ItemBase item, bool isTails)
    {
        Player = Player.Get(player);
        Item = Item.Get(item);
        IsTails = isTails;
    }

    /// <summary>
    /// The player who flipped the coin.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Whenever the coin flip is tails.
    /// </summary>
    public bool IsTails { get; }

    /// <inheritdoc />
    public Item Item { get; }
}