using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchedPickup"/> event.
/// </summary>
public class PlayerSearchedPickupEventArgs : EventArgs, IPlayerEvent, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchedPickupEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who searched for pickup.</param>
    /// <param name="pickup">The item pickup.</param>
    public PlayerSearchedPickupEventArgs(ReferenceHub hub, ItemPickupBase pickup)
    {
        Player = Player.Get(hub);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who searched for pickup.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item pickup.
    /// </summary>
    public Pickup Pickup { get; }
}