using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UsedItem"/> event.
/// </summary>
public class PlayerUsedItemEventArgs : EventArgs, IPlayerEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUsedItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who used the item.</param>
    /// <param name="item">Item that was used.</param>
    public PlayerUsedItemEventArgs(ReferenceHub player, ItemBase item)
    {
        Player = Player.Get(player);
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the player that used the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that was used.
    /// </summary>
    public Item Item { get; }
}