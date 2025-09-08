using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ShootingWeapon"/> event.
/// </summary>
public class PlayerShootingWeaponEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerShootingWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is shooting.</param>
    /// <param name="weapon">The firearm that the player shooting from.</param>
    public PlayerShootingWeaponEventArgs(ReferenceHub hub, Firearm weapon)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        FirearmItem = FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is shooting.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the firearm that the player shooting from.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;
}