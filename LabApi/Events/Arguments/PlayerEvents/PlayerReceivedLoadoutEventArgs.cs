using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReceivedLoadout"/> event.
/// </summary>
public class PlayerReceivedLoadoutEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReceivedLoadoutEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player instance.</param>
    /// <param name="items">The items which player received.</param>
    /// <param name="ammo">The ammo which player received.</param>
    /// <param name="inventoryReset">If players inventory did reset.</param>
    public PlayerReceivedLoadoutEventArgs(ReferenceHub player, List<ItemType> items, Dictionary<ItemType, ushort> ammo, bool inventoryReset)
    {
        Player = Player.Get(player);
        Items = items;
        Ammo = ammo;
        InventoryReset = inventoryReset;
    }

    /// <summary>
    /// Gets the gets player which received this loadout.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the items which player received.
    /// </summary>
    public List<ItemType> Items { get; }

    /// <summary>
    /// Gets ammo which player received.
    /// </summary>
    public Dictionary<ItemType, ushort> Ammo { get; }

    /// <summary>
    /// Gets whether the player's inventory did reset before getting loadout.
    /// </summary>
    public bool InventoryReset { get; }
}