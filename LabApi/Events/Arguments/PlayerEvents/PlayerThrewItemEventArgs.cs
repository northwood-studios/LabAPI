using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ThrewItem"/> event.
/// </summary>
public class PlayerThrewItemEventArgs : EventArgs, IPlayerEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerThrewItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who threw the item.</param>
    /// <param name="item">The item that was thrown.</param>
    /// <param name="rigidbody">The rigidbody of the item.</param>
    public PlayerThrewItemEventArgs(ReferenceHub player, ItemBase item, Rigidbody rigidbody)
    {
        Player = Player.Get(player);
        Item = Item.Get(item);
        Rigidbody = rigidbody;
    }

    /// <summary>
    /// Gets the player who threw the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that was thrown.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// Gets the rigidbody of the item.
    /// </summary>
    public Rigidbody Rigidbody { get; }
}