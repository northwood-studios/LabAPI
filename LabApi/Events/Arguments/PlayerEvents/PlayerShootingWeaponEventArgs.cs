using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ShootingWeapon"/> event.
/// </summary>
public class PlayerShootingWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerShootingWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is shooting.</param>
    /// <param name="weapon">The firearm that the player shooting from.</param>
    public PlayerShootingWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is shooting.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the firearm that the player shooting from.
    /// </summary>
    public Item Weapon { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}