using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReloadedWeapon"/> event.
/// </summary>
public class PlayerReloadedWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReloadedWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who reloaded the weapon.</param>
    /// <param name="weapon">The weapon that was reloaded.</param>
    public PlayerReloadedWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who reloaded the weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon that was reloaded.
    /// </summary>
    public Item Weapon { get; }
}