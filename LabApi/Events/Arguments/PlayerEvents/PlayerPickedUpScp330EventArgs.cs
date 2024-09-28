using InventorySystem.Items.Usables.Scp330;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickedUpScp330"/> event.
/// </summary>
public class PlayerPickedUpScp330EventArgs : EventArgs, IPlayerEvent, IItemEvent, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickedUpScp330EventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who picked up SCP-330.</param>
    /// <param name="pickup">The pickup item.</param>
    /// <param name="item">SCP-330 bag item of the player.</param>
    public PlayerPickedUpScp330EventArgs(ReferenceHub player, Scp330Pickup pickup, Scp330Bag item)
    {
        Player = Player.Get(player);
        Pickup = Pickup.Get(pickup);
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the player who picked up SCP-330.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the pickup item.
    /// </summary>
    public Pickup Pickup { get; }

    /// <summary>
    /// Gets the SCP-330 bag item of the player.
    /// </summary>
    public Item Item { get; }
}