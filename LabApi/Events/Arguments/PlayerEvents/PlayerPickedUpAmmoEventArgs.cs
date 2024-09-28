using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickedUpAmmo"/> event.
/// </summary>
public class PlayerPickedUpAmmoEventArgs : EventArgs, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickedUpAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player that picked up the ammo.</param>
    /// <param name="ammoType">Type of the ammo.</param>
    /// <param name="ammoAmount">The amount that is was picked up.</param>
    /// <param name="pickup">The pickup object.</param>
    public PlayerPickedUpAmmoEventArgs(ReferenceHub player, ItemType ammoType, ushort ammoAmount, ItemPickupBase pickup)
    {
        Player = Player.Get(player);
        AmmoType = ammoType;
        AmmoAmount = ammoAmount;
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player that picked up the ammo.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets type of the ammo.
    /// </summary>
    public ItemType AmmoType { get; }

    /// <summary>
    /// Gets the amount that is was picked up.
    /// </summary>
    public ushort AmmoAmount { get; }

    /// <summary>
    /// Gets the pickup object.
    /// </summary>
    public Pickup? Pickup { get; }
}