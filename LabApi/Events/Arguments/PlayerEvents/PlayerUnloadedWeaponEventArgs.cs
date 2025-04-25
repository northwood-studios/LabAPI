using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnloadedWeapon"/> event.
/// </summary>
public class PlayerUnloadedWeaponEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnloadedWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who unloaded the weapon.</param>
    /// <param name="weapon">The weapon that was unloaded.</param>
    public PlayerUnloadedWeaponEventArgs(ReferenceHub player, Firearm weapon)
    {
        Player = Player.Get(player);
        FirearmItem = FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who unloaded the weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon that was unloaded.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;
}