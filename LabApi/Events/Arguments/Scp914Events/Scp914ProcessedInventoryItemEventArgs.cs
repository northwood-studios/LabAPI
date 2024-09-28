using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;
using System;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when an item is processed by SCP-914.
/// </summary>
public class Scp914ProcessedInventoryItemEventArgs : EventArgs, IScp914Event, IItemEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ProcessedInventoryItemEventArgs"/> class.
    /// </summary>
    /// <param name="oldItemType">The old item type.</param>
    /// <param name="item">The new item that has been processed by SCP-914.</param>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="player">The owner of the item.</param>
    public Scp914ProcessedInventoryItemEventArgs(ItemType oldItemType, Item item, Scp914KnobSetting knobSetting, ReferenceHub player)
    {
        OldItemType = oldItemType;
        Item = item;
        KnobSetting = knobSetting;
        Player = Player.Get(player);
    }

    /// <summary>
    /// Gets the old item type.
    /// </summary>
    public ItemType OldItemType { get; }

    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; }

    /// <summary>
    /// The new item that has been processed by SCP-914.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// The owner of the item.
    /// </summary>
    public Player Player { get; }
}