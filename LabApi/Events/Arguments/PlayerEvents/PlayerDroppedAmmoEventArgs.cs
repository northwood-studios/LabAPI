using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseAmmoPickup = InventorySystem.Items.Firearms.Ammo.AmmoPickup;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DroppedAmmo"/> event.
/// </summary>
public class PlayerDroppedAmmoEventArgs : EventArgs, IPlayerEvent, IAmmoPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDroppedAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is dropping the ammo.</param>
    /// <param name="type">The type of ammo being dropped.</param>
    /// <param name="amount">The amount of ammo being dropped.</param>
    /// <param name="pickup">The ammo pickup.</param>
    public PlayerDroppedAmmoEventArgs(ReferenceHub player, ItemType type, int amount, BaseAmmoPickup pickup)
    {
        Player = Player.Get(player);
        Type = type;
        Amount = amount;
        AmmoPickup = AmmoPickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who is dropping the ammo.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the type of ammo being dropped.
    /// </summary>
    public ItemType Type { get; }

    /// <summary>
    /// Gets the amount of ammo being dropped.
    /// </summary>
    public int Amount { get; }

    /// <summary>
    /// Gets the ammo pickup.
    /// </summary>
    public AmmoPickup AmmoPickup { get; }

    /// <inheritdoc cref="AmmoPickup"/>
    [Obsolete($"Use {nameof(AmmoPickup)} instead")]
    public Pickup Pickup => AmmoPickup;
}