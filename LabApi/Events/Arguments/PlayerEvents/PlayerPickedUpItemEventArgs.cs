using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickedUpItem"/> event.
/// </summary>
public class PlayerPickedUpItemEventArgs : EventArgs, IPlayerEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickedUpItemEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who picked up the item.</param>
    /// <param name="item">The item that was picked up.</param>
    public PlayerPickedUpItemEventArgs(ReferenceHub hub, ItemBase item)
    {
        Player = Player.Get(hub);
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the player who picked up the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that was picked up.
    /// </summary>
    public Item Item { get; }
}