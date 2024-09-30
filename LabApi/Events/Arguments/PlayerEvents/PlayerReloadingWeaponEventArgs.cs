using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReloadingWeapon"/> event.
/// </summary>
public class PlayerReloadingWeaponEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReloadingWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is reloading the weapon.</param>
    /// <param name="weapon">The weapon that is being reloaded.</param>
    public PlayerReloadingWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is reloading the weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon that is being reloaded.
    /// </summary>
    public Item Weapon { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}