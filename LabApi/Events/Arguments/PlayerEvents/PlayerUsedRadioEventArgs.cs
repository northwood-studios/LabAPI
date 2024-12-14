using InventorySystem.Items.Radio;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using RadioItem = InventorySystem.Items.Radio.RadioItem;
namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UsedRadio"/> event.
/// </summary>
public class PlayerUsedRadioEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUsedRadioEventArgs"/> class.
    /// </summary>
    /// <param name="player">Player that used the radio.</param>
    /// <param name="radio">Radio that was being used.</param>
    /// <param name="drain">Drain amount of the battery per second.</param>
    public PlayerUsedRadioEventArgs(ReferenceHub player, RadioItem radio, float drain)
    {
        Player = Player.Get(player);
        Radio = radio;
        Drain = drain;
    }

    /// <summary>
    /// Gets the player that used the radio.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the radio that was being used.
    /// </summary>
    public RadioItem Radio { get; }

    /// <summary>
    /// Gets the drain amount of the battery per second.
    /// </summary>
    public float Drain { get; }
}