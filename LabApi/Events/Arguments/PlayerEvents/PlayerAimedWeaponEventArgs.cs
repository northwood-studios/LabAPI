using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.AimedWeapon"/> event.
/// </summary>
public class PlayerAimedWeaponEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerAimedWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who aimed the weapon.</param>
    /// <param name="weapon">The weapon that the player aimed.</param>
    /// <param name="aiming">Whether the player was aiming or unaiming their weapon.</param>
    public PlayerAimedWeaponEventArgs(ReferenceHub hub, Firearm weapon, bool aiming)
    {
        Player = Player.Get(hub);
        FirearmItem = FirearmItem.Get(weapon);
        Aiming = aiming;
    }

    /// <summary>
    /// Gets the player who aimed the weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Whether the player is aiming or unaiming.
    /// </summary>
    public bool Aiming { get; }

    /// <summary>
    /// Gets the weapon being aimed.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;
}