using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DroppingItem"/> event.
/// </summary>
public class PlayerDroppingItemEventArgs : EventArgs, ICancellableEvent, IItemEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDroppingItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is dropping the item.</param>
    /// <param name="item">The item being dropped.</param>
    /// <param name="isThrowing">Whether the item will be thrown.</param>
    public PlayerDroppingItemEventArgs(ReferenceHub player, ItemBase item, bool isThrowing)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Item = Item.Get(item);
        Throw = isThrowing;
    }

    /// <summary>
    /// Gets the player who is dropping the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item being dropped.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// Gets or sets whether the <see cref="Pickup"/> will be thrown by the <see cref="Player"/>.
    /// </summary>
    public bool Throw { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}