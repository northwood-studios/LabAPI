using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseUsableItem = InventorySystem.Items.Usables.UsableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ItemUsageEffectsApplying"/> event.
/// </summary>
public class PlayerItemUsageEffectsApplyingEventArgs : EventArgs, IPlayerEvent, IUsableItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerItemUsageEffectsApplyingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> component of the player using the item.</param>
    /// <param name="item">The item that is being used.</param>
    public PlayerItemUsageEffectsApplyingEventArgs(ReferenceHub hub, BaseUsableItem item)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        UsableItem = UsableItem.Get(item);
    }

    /// <summary>
    /// Gets the player using the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that is being used.
    /// </summary>
    public UsableItem UsableItem { get; }

    /// <summary>
    /// Get or sets whether or not to continue the using item process when the event is canceled.
    /// </summary>
    public bool ContinueProcess { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
