using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DryFiredWeapon"/> event.
/// </summary>
public class PlayerDryFiredWeaponEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDryFiredWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who dry fired.</param>
    /// <param name="weapon">The weapon item.</param>
    public PlayerDryFiredWeaponEventArgs(ReferenceHub player, Firearm weapon)
    {
        Player = Player.Get(player);
        FirearmItem = FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who dry fired.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon item.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;
}