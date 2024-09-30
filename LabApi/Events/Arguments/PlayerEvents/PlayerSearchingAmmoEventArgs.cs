using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchingAmmo"/> event.
/// </summary>
public class PlayerSearchingAmmoEventArgs : EventArgs, IPlayerEvent, IPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchingAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player searching for ammo pickup.</param>
    /// <param name="pickup">The ammo pickup.</param>
    public PlayerSearchingAmmoEventArgs(ReferenceHub player, ItemPickupBase pickup)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player searching for ammo pickup.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the ammo pickup.
    /// </summary>
    public Pickup Pickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}