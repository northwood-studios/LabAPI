using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseRadioItem = InventorySystem.Items.Radio.RadioItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.TogglingRadio"/> event.
/// </summary>
public class PlayerTogglingRadioEventArgs : EventArgs, IPlayerEvent, IRadioItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTogglingRadioEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is toggling a radio.</param>
    /// <param name="radio">The radio item.</param>
    /// <param name="newState">New state of the radio being turned off or on.</param>
    public PlayerTogglingRadioEventArgs(ReferenceHub hub, BaseRadioItem radio, bool newState)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        RadioItem = RadioItem.Get(radio);
        NewState = newState;
    }

    /// <summary>
    /// Gets the player who is toggling a radio.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio item.
    /// </summary>
    public RadioItem RadioItem { get; }

    /// <summary>
    /// Gets the new state of the radio being turned off or on.
    /// </summary>
    public bool NewState { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="RadioItem"/>
    [Obsolete($"Use {nameof(RadioItem)} instead")]
    public BaseRadioItem Radio => RadioItem.Base;
}