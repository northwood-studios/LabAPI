using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;

using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.AimedWeapon"/> event.
/// </summary>
public class PlayerAimedWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerAimedWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who aimed the weapon.</param>
    /// <param name="weapon">The weapon that the player aimed.</param>
    /// <param name="aiming">Whether or not the player was aiming or unaiming their weapon.</param>
    public PlayerAimedWeaponEventArgs(ReferenceHub player, ItemBase weapon, bool aiming)
    {
        Player = Player.Get(player);
        Weapon = Item.Get(weapon);
        Aiming = aiming;
    }

    /// <summary>
    /// Gets the player who aimed the weapon.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Whether or not the player is aiming or unaiming.
    /// </summary>
    public bool Aiming { get; }

    /// <inheritdoc />
    public Item Weapon { get; }
}