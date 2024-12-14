using InventorySystem.Items.Radio;
using LabApi.Features.Wrappers;
using System;
using RadioItem = InventorySystem.Items.Radio.RadioItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ToggledRadio"/> event.
/// </summary>
public class PlayerToggledRadioEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerToggledRadioEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who toggled the radio.</param>
    /// <param name="radio">The radio item.</param>
    /// <param name="newState">New state of the radio.</param>
    public PlayerToggledRadioEventArgs(ReferenceHub player, RadioItem radio, bool newState)
    {
        Player = Player.Get(player);
        Radio = radio;
        NewState = newState;
    }

    /// <summary>
    /// Gets the player who toggled the radio.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio item.
    /// </summary>
    public RadioItem Radio { get; }

    /// <summary>
    /// Gets the new state of the radio.
    /// </summary>
    public bool NewState { get; }
}
