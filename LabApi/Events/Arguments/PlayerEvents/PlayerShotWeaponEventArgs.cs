using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ShotWeapon"/> event.
/// </summary>
public class PlayerShotWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerShotWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who shot.</param>
    /// <param name="weapon">The firearm that the player shot from.</param>
    public PlayerShotWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who shot.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the firearm that the player shot from.
    /// </summary>
    public Item Weapon { get; }
}