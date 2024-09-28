using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnloadedWeapon"/> event.
/// </summary>
public class PlayerUnloadedWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnloadedWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who unloaded the weapon.</param>
    /// <param name="weapon">The weapon that was unloaded.</param>
    public PlayerUnloadedWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who unloaded the weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon that was unloaded.
    /// </summary>
    public Item Weapon { get; }
}