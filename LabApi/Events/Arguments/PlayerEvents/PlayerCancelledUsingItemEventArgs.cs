using InventorySystem.Items.Usables;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UsableItem = InventorySystem.Items.Usables.UsableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.CancelledUsingItem"/> event.
/// </summary>
public class PlayerCancelledUsingItemEventArgs : EventArgs, IUsableItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerCancelledUsingItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who cancelled using the item.</param>
    /// <param name="item">The item which the player cancelled using.</param>
    public PlayerCancelledUsingItemEventArgs(ReferenceHub player, UsableItem item)
    {
        Player = Player.Get(player);
        Item = item;
    }

    /// <summary>
    /// Gets the player who cancelled using the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item which the player cancelled using.
    /// </summary>
    public UsableItem Item { get; }
}