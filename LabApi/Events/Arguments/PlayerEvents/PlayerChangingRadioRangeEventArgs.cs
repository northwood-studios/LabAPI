using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.Radio.RadioMessages;
using BaseRadioItem = InventorySystem.Items.Radio.RadioItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangingRadioRange"/> event.
/// </summary>
public class PlayerChangingRadioRangeEventArgs : EventArgs, IPlayerEvent, IRadioItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangingRadioRangeEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is changing radio range.</param>
    /// <param name="radio">The radio item.</param>
    /// <param name="range">The range level that is radio being changed to.</param>
    public PlayerChangingRadioRangeEventArgs(ReferenceHub player, BaseRadioItem radio, RadioRangeLevel range)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        RadioItem = RadioItem.Get(radio);
        Range = range;
    }

    /// <summary>
    /// Gets the player who is changing radio range.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio item.
    /// </summary>
    public RadioItem RadioItem { get; }

    /// <summary>
    /// Gets the range level that is radio being changed to.
    /// </summary>
    public RadioRangeLevel Range { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="RadioItem"/>
    [Obsolete($"Use {nameof(RadioItem)} instead")]
    public Item Radio => RadioItem;
}