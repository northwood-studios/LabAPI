using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ToggledFlashlight"/> event.
/// </summary>
public class PlayerToggledFlashlightEventArgs : EventArgs, IPlayerEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerToggledFlashlightEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who toggled the flashlight.</param>
    /// <param name="item">The flashlight item.</param>
    /// <param name="newState">New state of the flashlight.</param>
    public PlayerToggledFlashlightEventArgs(ReferenceHub player, ItemBase item, bool newState)
    {
        Player = Player.Get(player);
        Item = Item.Get(item);
        NewState = newState;
    }

    /// <summary>
    /// Gets the player who toggled the flashlight.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the flashlight item.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// Gets the new state of the flashlight.
    /// </summary>
    public bool NewState { get; }
}