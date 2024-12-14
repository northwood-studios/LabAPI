using InventorySystem.Items.Radio;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using RadioItem = InventorySystem.Items.Radio.RadioItem;
namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UsingRadio"/> event.
/// </summary>
public class PlayerUsingRadioEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUsingRadioEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is using the radio.</param>
    /// <param name="radio">Radio item that is being used.</param>
    /// <param name="drain">Battery drain amount per second.</param>
    public PlayerUsingRadioEventArgs(ReferenceHub player, RadioItem radio, float drain)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Radio = radio;
        Drain = drain;
    }

    /// <summary>
    /// Gets the player who is using the radio.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio that is being used.
    /// </summary>
    public RadioItem Radio { get; }

    /// <summary>
    /// Gets the battery drain amount per second.
    /// </summary>
    public float Drain { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}