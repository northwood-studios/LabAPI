using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UsingItem"/> event.
/// </summary>
public class PlayerUsingItemEventArgs : EventArgs, IPlayerEvent, IItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUsingItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player using the item.</param>
    /// <param name="item">The item that is being used.</param>
    public PlayerUsingItemEventArgs(ReferenceHub player, ItemBase item)
    {
        Player = Player.Get(player);
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the player using the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that is being used.
    /// </summary>
    public Item Item { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}