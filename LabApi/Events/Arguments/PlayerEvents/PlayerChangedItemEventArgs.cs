using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangedItem"/> event.
/// </summary>
public class PlayerChangedItemEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangedItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who changed current item.</param>
    /// <param name="oldItem">The old item which player changed to.</param>
    /// <param name="oldItem">The new item which player changed to.</param>
    public PlayerChangedItemEventArgs(ReferenceHub player, ItemBase oldItem, ItemBase newItem)
    {
        Player = Player.Get(player);
        OldItem = Item.Get(oldItem);
        NewItem = Item.Get(newItem);
    }

    /// <summary>
    /// Gets the player who changed current item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old item which player changed.
    /// </summary>
    public Item OldItem { get; }

    /// <summary>
    /// Gets the new item which player changed to.
    /// </summary>
    public Item NewItem { get; }
}