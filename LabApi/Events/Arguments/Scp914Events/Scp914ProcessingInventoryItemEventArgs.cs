using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;
using InventorySystem.Items;
using System;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when an item is being processed by SCP-914.
/// </summary>
public class Scp914ProcessingInventoryItemEventArgs : EventArgs, IScp914Event, IItemEvent, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ProcessingInventoryItemEventArgs"/> class.
    /// </summary>
    /// <param name="item">The item that is being processed by SCP-914.</param>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="player">The owner of the item.</param>
    public Scp914ProcessingInventoryItemEventArgs(ItemBase item, Scp914KnobSetting knobSetting, ReferenceHub player)
    {
        IsAllowed = true;
        Item = Item.Get(item);
        KnobSetting = knobSetting;
        Player = Player.Get(player);
    }

    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; set; }

    /// <summary>
    /// The item that is being processed by SCP-914.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// The owner of the item.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}