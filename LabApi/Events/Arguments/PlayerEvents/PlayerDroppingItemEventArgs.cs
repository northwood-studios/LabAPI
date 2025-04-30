using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DroppingItem"/> event.
/// </summary>
public class PlayerDroppingItemEventArgs : EventArgs, ICancellableEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDroppingItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is dropping the item.</param>
    /// <param name="item">The item being dropped.</param>
    public PlayerDroppingItemEventArgs(ReferenceHub player, ItemBase item)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the player who is dropping the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item being dropped.
    /// </summary>
    public Item Item { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}