using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DroppedItem"/> event.
/// </summary>
public class PlayerDroppedItemEventArgs : EventArgs, IPlayerEvent, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDroppedItemEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who dropped the item.</param>
    /// <param name="pickup">The item pickup.</param>
    /// <param name="isThrowing">Whether the item will be thrown.</param>
    public PlayerDroppedItemEventArgs(ReferenceHub hub, ItemPickupBase pickup, bool isThrowing)
    {
        Player = Player.Get(hub);
        Pickup = Pickup.Get(pickup);
        Throw = isThrowing;
    }

    /// <summary>
    /// Gets the player who dropped the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item pickup.
    /// </summary>
    public Pickup Pickup { get; }

    /// <summary>
    /// Gets or sets whether the <see cref="Pickup"/> will be thrown by the <see cref="Player"/>.
    /// </summary>
    public bool Throw { get; set; }
}