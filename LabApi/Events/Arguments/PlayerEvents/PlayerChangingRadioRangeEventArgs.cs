using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.Radio.RadioMessages;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangingRadioRange"/> event.
/// </summary>
public class PlayerChangingRadioRangeEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangingRadioRangeEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is changing radio range.</param>
    /// <param name="radio">The radio item.</param>
    /// <param name="range">The range level that is radio being changed to.</param>
    public PlayerChangingRadioRangeEventArgs(ReferenceHub player, ItemBase radio, RadioRangeLevel range)
    {
        Player = Player.Get(player);
        Radio = Item.Get(radio);
        Range = range;
    }

    /// <summary>
    /// Gets the player who is changing radio range.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio item.
    /// </summary>
    public Item Radio { get; }

    /// <summary>
    /// Gets the range level that is radio being changed to.
    /// </summary>
    public RadioRangeLevel Range { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}