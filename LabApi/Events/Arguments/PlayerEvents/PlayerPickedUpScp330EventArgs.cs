using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseScp330Pickup = InventorySystem.Items.Usables.Scp330.Scp330Pickup;
using BaseScp330Bag = InventorySystem.Items.Usables.Scp330.Scp330Bag;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickedUpScp330"/> event.
/// </summary>
public class PlayerPickedUpScp330EventArgs : EventArgs, IPlayerEvent, ICandyItemEvent, ICandyPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickedUpScp330EventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who picked up SCP-330.</param>
    /// <param name="pickup">The pickup item.</param>
    /// <param name="item">SCP-330 bag item of the player.</param>
    public PlayerPickedUpScp330EventArgs(ReferenceHub hub, BaseScp330Pickup pickup, BaseScp330Bag item)
    {
        Player = Player.Get(hub);
        CandyPickup = Scp330Pickup.Get(pickup);
        CandyItem = Scp330Item.Get(item);
    }

    /// <summary>
    /// Gets the player who picked up SCP-330.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the SCP-330 pickup.
    /// </summary>
    public Scp330Pickup CandyPickup { get; }

    /// <summary>
    /// Gets the SCP-330 bag item of the player.
    /// </summary>
    public Scp330Item CandyItem { get; }

    /// <inheritdoc cref="CandyPickup"/>
    [Obsolete($"Use {nameof(CandyPickup)} instead")]
    public Pickup Pickup => CandyPickup;

    /// <inheritdoc cref="CandyItem"/>
    [Obsolete($"Use {nameof(CandyItem)} instead")]
    public Item Item => CandyItem;
}