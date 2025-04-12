using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReloadedWeapon"/> event.
/// </summary>
public class PlayerReloadedWeaponEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReloadedWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who reloaded the weapon.</param>
    /// <param name="weapon">The weapon that was reloaded.</param>
    public PlayerReloadedWeaponEventArgs(ReferenceHub player, Firearm weapon)
    {
        Player = Player.Get(player);
        FirearmItem = FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who reloaded the weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon that was reloaded.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;
}