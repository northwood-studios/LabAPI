using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickedUpArmor"/> event.
/// </summary>
public class PlayerPickedUpArmorEventArgs : EventArgs, IPlayerEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickedUpArmorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who picked up armor.</param>
    /// <param name="item">The armor that was picked up.</param>
    public PlayerPickedUpArmorEventArgs(ReferenceHub player, ItemBase? item)
    {
        Player = Player.Get(player);
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the player who picked up armor.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the armor that was picked up.
    /// </summary>
    public Item? Item { get; }
}