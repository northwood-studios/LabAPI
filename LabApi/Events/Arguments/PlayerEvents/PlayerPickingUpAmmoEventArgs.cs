using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickingUpAmmo"/> event.
/// </summary>
public class PlayerPickingUpAmmoEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickingUpAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is pickup the the ammo pickup.</param>
    /// <param name="ammoType">Type of the ammo./param>
    /// <param name="ammoAmount">Amount of ammo that is being picked up.</param>
    /// <param name="pickup">Ammo pickup.</param>
    public PlayerPickingUpAmmoEventArgs(ReferenceHub player, ItemType ammoType, ushort ammoAmount, ItemPickupBase pickup)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        AmmoType = ammoType;
        AmmoAmount = ammoAmount;
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who is pickup the the ammo pickup.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets type of the ammo.
    /// </summary>
    public ItemType AmmoType { get; }

    /// <summary>
    /// Gets the amount of ammo that is being picked up.
    /// </summary>
    public ushort AmmoAmount { get; set; }

    /// <summary>
    /// Gets the ammo pickup.
    /// </summary>
    public Pickup Pickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}