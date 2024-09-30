using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReceivingLoadout"/> event.
/// </summary>
public class PlayerReceivingLoadoutEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReceivingLoadoutEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player instance.</param>
    /// <param name="items">The items which player received.</param>
    /// <param name="ammo">The ammo which player received.</param>
    /// <param name="inventoryReset">If players inventory will be cleared.</param>
    public PlayerReceivingLoadoutEventArgs(ReferenceHub player, List<ItemType> items, Dictionary<ItemType, ushort> ammo, bool inventoryReset)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Items = items;
        Ammo = ammo;
        InventoryReset = inventoryReset;
    }

    /// <summary>
    /// Gets player which receives this loadout.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets items which player will receive.
    /// </summary>
    public List<ItemType> Items { get; }

    /// <summary>
    /// Gets ammo which player will receive.
    /// </summary>
    public Dictionary<ItemType, ushort> Ammo { get; }

    /// <summary>
    /// Gets or sets whether players inventory should be cleared before getting loadout.
    /// </summary>
    public bool InventoryReset { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// Adds item of specifc type to this loadout.
    /// </summary>
    /// <param name="type">The type of item.</param>
    public void AddItem(ItemType type) => Items.Add(type);

    /// <summary>
    /// Adds ammo to this loadout.
    /// </summary>
    /// <param name="ammoType">The type of ammo.</param>
    /// <param name="ammoAmount">The amount of ammo.</param>
    public void AddAmmo(ItemType ammoType, ushort ammoAmount)
    {
        if (Ammo.ContainsKey(ammoType))
            Ammo[ammoType] += ammoAmount;
        else
            Ammo.Add(ammoType, ammoAmount);
    }

    /// <summary>
    /// Sets ammo for this loadout.
    /// </summary>
    /// <param name="ammoType">The type of ammo.</param>
    /// <param name="ammoAmount">The amount of ammo.</param>
    public void SetAmmo(ItemType ammoType, ushort ammoAmount)
    {
        if (Ammo.ContainsKey(ammoType))
            Ammo[ammoType] = ammoAmount;
        else
            Ammo.Add(ammoType, ammoAmount);
    }

    /// <summary>
    /// Clears ammo specified in this loadout.
    /// </summary>
    public void ClearAmmo() => Ammo.Clear();

    /// <summary>
    /// Clears items specified in this loadout.
    /// </summary>
    public void ClearItems() => Items.Clear();
}
