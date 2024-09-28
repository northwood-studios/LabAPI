using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnloadingWeapon"/> event.
/// </summary>
public class PlayerUnloadingWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnloadingWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is unloading a weapon.</param>
    /// <param name="weapon">The weapon that is being unloaded.</param>
    public PlayerUnloadingWeaponEventArgs(ReferenceHub player, ItemBase weapon)
    {
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is unloading a weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the weapon that is being unloaded.
    /// </summary>
    public Item Weapon { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}