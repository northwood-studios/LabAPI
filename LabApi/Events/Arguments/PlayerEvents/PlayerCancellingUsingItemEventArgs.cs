using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseUsableItem = InventorySystem.Items.Usables.UsableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.CancellingUsingItem"/> event.
/// </summary>
public class PlayerCancellingUsingItemEventArgs : EventArgs, IUsableItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerCancellingUsingItemEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is cancelling using the item.</param>
    /// <param name="item">The item which the player cancels using.</param>
    public PlayerCancellingUsingItemEventArgs(ReferenceHub hub, BaseUsableItem item)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        UsableItem = UsableItem.Get(item);
    }

    /// <summary>
    /// Gets the player who is cancelling using the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item which the player cancels using.
    /// </summary>
    public UsableItem UsableItem { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="UsableItem"/>
    [Obsolete($"Use {nameof(UsableItem)} instead")]
    public BaseUsableItem Item => UsableItem.Base;
}