using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangingItem"/> event.
/// </summary>
public class PlayerChangingItemEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangingItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is changing item.</param>
    /// <param name="oldItem">The old item.</param>
    /// <param name="newItem">The new item that is being equipped.</param>
    public PlayerChangingItemEventArgs(ReferenceHub player, ItemBase? oldItem, ItemBase? newItem)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        OldItem = Item.Get(oldItem);
        NewItem = Item.Get(newItem);
    }

    /// <summary>
    /// Gets the player who is changing item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old item.
    /// </summary>
    public Item? OldItem { get; }

    /// <summary>
    /// Gets the new item that is being equipped.
    /// <para>Item is null if player is equipping nothing.</para>
    /// </summary>
    public Item? NewItem { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}