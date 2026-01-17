using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.Radio.RadioMessages;
using BaseRadioItem = InventorySystem.Items.Radio.RadioItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangedRadioRange"/> event.
/// </summary>
public class PlayerChangedRadioRangeEventArgs : EventArgs, IRadioItemEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangedRadioRangeEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who changed radio range.</param>
    /// <param name="radio">The radio item.</param>
    /// <param name="range">The new range level.</param>
    public PlayerChangedRadioRangeEventArgs(ReferenceHub hub, BaseRadioItem radio, RadioRangeLevel range)
    {
        Player = Player.Get(hub);
        RadioItem = RadioItem.Get(radio);
        Range = range;
    }

    /// <summary>
    /// Gets the player who changed radio range.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio item.
    /// </summary>
    public RadioItem RadioItem { get; }

    /// <summary>
    /// Gets the new range level.
    /// </summary>
    public RadioRangeLevel Range { get; }

    /// <inheritdoc cref="RadioItem"/>
    [Obsolete($"Use {nameof(RadioItem)} instead")]
    public Item Radio => RadioItem;
}