using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;

using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

// TODO: implement cancellable aiming.
///// <summary>
///// Represents the arguments for the <see cref="Handlers.PlayerEvents.AimingWeapon"/> event.
///// </summary>
//public class PlayerAimingWeaponEventArgs : EventArgs, IPlayerEvent, IWeaponEvent, ICancellableEvent
//{
//    /// <summary>
//    /// Initializes a new instance of the <see cref="PlayerAimingWeaponEventArgs"/> class.
//    /// </summary>
//    /// <param name="player">The player who is aiming the weapon.</param>
//    /// <param name="weapon">The weapon that the player is aiming.</param>
//    /// <param name="aiming">Whether or not the player is aiming or unaiming their weapon.</param>
//    public PlayerAimingWeaponEventArgs(ReferenceHub player, ItemBase weapon, bool aiming)
//    {
//        IsAllowed = true;
//        Player = Player.Get(player);
//        Weapon = Item.Get(weapon);
//        Aiming = aiming;
//    }

//    /// <summary>
//    /// Gets the player who is aiming the weapon.
//    /// </summary>
//    public Player Player { get; }

//    /// <inheritdoc />
//    public Item Weapon { get; }

//    /// <summary>
//    /// Whether or not the player is aiming or unaiming.
//    /// </summary>
//    public bool Aiming { get; }

//    /// <inheritdoc />
//    public bool IsAllowed { get; set; }
//}