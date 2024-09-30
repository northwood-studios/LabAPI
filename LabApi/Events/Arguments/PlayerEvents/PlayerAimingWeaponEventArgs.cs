using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;

using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.AimingWeapon"/> event.
/// </summary>
public class PlayerAimingWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerAimingWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is aiming the weapon.</param>
    /// <param name="weapon">The weapon that the player is aiming.</param>
    public PlayerAimingWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is aiming the weapon.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Item Weapon { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}