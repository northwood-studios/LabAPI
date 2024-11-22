using InventorySystem.Items.Usables;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.CancellingUsingItem"/> event.
/// </summary>
public class PlayerCancellingUsingItemEventArgs : EventArgs, IUsableItem, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerCancellingUsingItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is cancelling using the item.</param>
    /// <param name="item">The item which the player cancels using.</param>
    public PlayerCancellingUsingItemEventArgs(ReferenceHub player, UsableItem item)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Item = item;
    }

    /// <summary>
    /// Gets the player who is cancelling using the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item which the player cancels using.
    /// </summary>
    public UsableItem Item { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}