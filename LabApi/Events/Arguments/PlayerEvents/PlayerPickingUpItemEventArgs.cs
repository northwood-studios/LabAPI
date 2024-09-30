using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Facility;

using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickingUpItem"/> event.
/// </summary>
public class PlayerPickingUpItemEventArgs : EventArgs, IPlayerEvent, IPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickingUpItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who picked up the item.</param>
    /// <param name="pickup">The item pickup.</param>
    public PlayerPickingUpItemEventArgs(ReferenceHub player, ItemPickupBase pickup)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who picked up the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item pickup.
    /// </summary>
    public Pickup Pickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}