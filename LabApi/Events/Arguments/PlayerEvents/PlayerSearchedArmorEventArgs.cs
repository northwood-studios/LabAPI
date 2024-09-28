using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchedArmor"/> event.
/// </summary>
public class PlayerSearchedArmorEventArgs : EventArgs, IPlayerEvent, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchedArmorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who searched for armor pickup.</param>
    /// <param name="pickup">The armor pickup.</param>
    public PlayerSearchedArmorEventArgs(ReferenceHub player, ItemPickupBase pickup)
    {
        Player = Player.Get(player);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who searched for armor pickup.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the armor pickup.
    /// </summary>
    public Pickup Pickup { get; }
}