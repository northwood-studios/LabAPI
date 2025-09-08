using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnloadingWeapon"/> event.
/// </summary>
public class PlayerUnloadingWeaponEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnloadingWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is unloading a weapon.</param>
    /// <param name="weapon">The weapon that is being unloaded.</param>
    public PlayerUnloadingWeaponEventArgs(ReferenceHub hub, Firearm weapon)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        FirearmItem = FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is unloading a weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the weapon that is being unloaded.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;
}