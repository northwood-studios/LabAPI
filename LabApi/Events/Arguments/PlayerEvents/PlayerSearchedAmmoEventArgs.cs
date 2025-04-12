using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseAmmoPickup = InventorySystem.Items.Firearms.Ammo.AmmoPickup;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchedAmmo"/> event.
/// </summary>
public class PlayerSearchedAmmoEventArgs : EventArgs, IPlayerEvent, IAmmoPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchedAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who searched for ammo pickup.</param>
    /// <param name="pickup">The ammo pickup.</param>
    public PlayerSearchedAmmoEventArgs(ReferenceHub player, BaseAmmoPickup pickup)
    {
        Player = Player.Get(player);
        AmmoPickup = AmmoPickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who searched for ammo pickup.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the ammo pickup.
    /// </summary>
    public AmmoPickup AmmoPickup { get; }

    /// <inheritdoc cref="AmmoPickup"/>
    [Obsolete($"Use {nameof(AmmoPickup)} instead")]
    public Pickup Pickup => AmmoPickup;
}