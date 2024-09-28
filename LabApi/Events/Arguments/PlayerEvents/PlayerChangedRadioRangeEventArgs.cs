using InventorySystem.Items;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.Radio.RadioMessages;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangedRadioRange"/> event.
/// </summary>
public class PlayerChangedRadioRangeEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangedRadioRangeEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who changed radio range.</param>
    /// <param name="radio">The radio item.</param>
    /// <param name="range">The new range level.</param>
    public PlayerChangedRadioRangeEventArgs(ReferenceHub player, ItemBase radio, RadioRangeLevel range)
    {
        Player = Player.Get(player);
        Radio = Item.Get(radio);
        Range = range;
    }

    /// <summary>
    /// Gets tets the player who changed radio range.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio item.
    /// </summary>
    public Item Radio { get; }

    /// <summary>
    /// Gets the new range level.
    /// </summary>
    public RadioRangeLevel Range { get; }
}