using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ThrewItem"/> event.
/// </summary>
public class PlayerThrewItemEventArgs : EventArgs, IPlayerEvent, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerThrewItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who threw the item.</param>
    /// <param name="item">The item that was thrown.</param>
    /// <param name="rigidbody">The rigidbody of the item.</param>
    public PlayerThrewItemEventArgs(ReferenceHub player, ItemPickupBase item, Rigidbody rigidbody)
    {
        Player = Player.Get(player);
        Pickup = Pickup.Get(item);
        Rigidbody = rigidbody;
    }

    /// <summary>
    /// Gets the player who threw the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the Pickup that was thrown.
    /// </summary>
    public Pickup Pickup { get; }

    /// <summary>
    /// Gets the rigidbody of the item.
    /// </summary>
    public Rigidbody Rigidbody { get; }
}