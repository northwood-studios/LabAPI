using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DryFiringWeapon"/> event.
/// </summary>
public class PlayerDryFiringWeaponEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDryFiringWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is dry firing.</param>
    /// <param name="weapon">The weapon item.</param>
    public PlayerDryFiringWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is dry firing.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon item.
    /// </summary>
    public Item Weapon { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}