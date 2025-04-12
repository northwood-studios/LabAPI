using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ShotWeapon"/> event.
/// </summary>
public class PlayerShotWeaponEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerShotWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who shot.</param>
    /// <param name="weapon">The firearm that the player shot from.</param>
    public PlayerShotWeaponEventArgs(ReferenceHub player, Firearm weapon)
    {
        Player = Player.Get(player);
        FirearmItem = FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who shot.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the firearm that the player shot from.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;
}