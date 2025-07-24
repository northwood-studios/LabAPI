using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseUsableItem = InventorySystem.Items.Usables.UsableItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UsedItem"/> event.
/// </summary>
public class PlayerUsedItemEventArgs : EventArgs, IPlayerEvent, IUsableItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUsedItemEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who used the item.</param>
    /// <param name="item">Item that was used.</param>
    public PlayerUsedItemEventArgs(ReferenceHub hub, BaseUsableItem item)
    {
        Player = Player.Get(hub);
        UsableItem = UsableItem.Get(item);
    }

    /// <summary>
    /// Gets the player that used the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that was used.
    /// </summary>
    public UsableItem UsableItem { get; }

    /// <inheritdoc cref="UsableItem"/>
    [Obsolete($"Use {nameof(UsableItem)} instead")]
    public Item Item => UsableItem;
}