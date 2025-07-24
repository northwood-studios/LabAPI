using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseUsableItem = InventorySystem.Items.Usables.UsableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.CancelledUsingItem"/> event.
/// </summary>
public class PlayerCancelledUsingItemEventArgs : EventArgs, IUsableItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerCancelledUsingItemEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who cancelled using the item.</param>
    /// <param name="item">The item which the player cancelled using.</param>
    public PlayerCancelledUsingItemEventArgs(ReferenceHub hub, BaseUsableItem item)
    {
        Player = Player.Get(hub);
        UsableItem = UsableItem.Get(item);
    }

    /// <summary>
    /// Gets the player who cancelled using the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item which the player cancelled using.
    /// </summary>
    public UsableItem UsableItem { get; }

    /// <inheritdoc cref="UsableItem"/>
    [Obsolete($"Use {nameof(UsableItem)} instead")]
    public BaseUsableItem Item => UsableItem.Base;
}