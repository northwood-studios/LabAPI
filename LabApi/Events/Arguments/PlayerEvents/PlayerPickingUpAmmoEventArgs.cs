using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseAmmoPickup = InventorySystem.Items.Firearms.Ammo.AmmoPickup;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickingUpAmmo"/> event.
/// </summary>
public class PlayerPickingUpAmmoEventArgs : EventArgs, IPlayerEvent, IAmmoPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickingUpAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is pickup the ammo pickup.</param>
    /// <param name="ammoType">Type of the ammo.</param>
    /// <param name="ammoAmount">Amount of ammo that is being picked up.</param>
    /// <param name="pickup">Ammo pickup.</param>
    public PlayerPickingUpAmmoEventArgs(ReferenceHub hub, ItemType ammoType, ushort ammoAmount, BaseAmmoPickup pickup)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        AmmoType = ammoType;
        AmmoAmount = ammoAmount;
        AmmoPickup = AmmoPickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who is picking up the ammo pickup.
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
    public AmmoPickup AmmoPickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="AmmoPickup"/>
    [Obsolete($"Use {nameof(AmmoPickup)} instead")]
    public Pickup Pickup => AmmoPickup;
}