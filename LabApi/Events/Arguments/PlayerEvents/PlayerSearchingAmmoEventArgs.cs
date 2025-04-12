using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseAmmoPickup = InventorySystem.Items.Firearms.Ammo.AmmoPickup;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchingAmmo"/> event.
/// </summary>
public class PlayerSearchingAmmoEventArgs : EventArgs, IPlayerEvent, IAmmoPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchingAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player searching for ammo pickup.</param>
    /// <param name="pickup">The ammo pickup.</param>
    public PlayerSearchingAmmoEventArgs(ReferenceHub player, BaseAmmoPickup pickup)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        AmmoPickup = AmmoPickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player searching for ammo pickup.
    /// </summary>
    public Player Player { get; }

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