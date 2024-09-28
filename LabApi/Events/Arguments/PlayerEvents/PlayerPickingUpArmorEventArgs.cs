using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Facility;

using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickingUpArmor"/> event.
/// </summary>
public class PlayerPickingUpArmorEventArgs : EventArgs, IPlayerEvent, IPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickingUpArmorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who picked up the armor.</param>
    /// <param name="pickup">The armor pickup.</param>
    public PlayerPickingUpArmorEventArgs(ReferenceHub player, ItemPickupBase pickup)
    {
        Player = Player.Get(player);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who picked up the armor.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the armor pickup.
    /// </summary>
    public Pickup Pickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}