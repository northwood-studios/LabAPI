﻿using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DryFiringWeapon"/> event.
/// </summary>
public class PlayerDryFiringWeaponEventArgs : EventArgs, IFirearmItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDryFiringWeaponEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is dry firing.</param>
    /// <param name="weapon">The weapon item.</param>
    public PlayerDryFiringWeaponEventArgs(ReferenceHub hub, Firearm weapon)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        FirearmItem = FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who is dry firing.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon item.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Weapon => FirearmItem;

}