using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchedAmmo"/> event.
/// </summary>
public class PlayerSearchedAmmoEventArgs : EventArgs, IPlayerEvent, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchedAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who searched for ammo pickup.</param>
    /// <param name="pickup">The ammo pickup.</param>
    public PlayerSearchedAmmoEventArgs(ReferenceHub player, ItemPickupBase pickup)
    {
        Player = Player.Get(player);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who searched for ammo pickup.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the ammo pickup.
    /// </summary>
    public Pickup Pickup { get; }
}