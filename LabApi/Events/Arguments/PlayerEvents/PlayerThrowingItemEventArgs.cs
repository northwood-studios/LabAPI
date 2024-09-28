using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ThrowingItem"/> event.
/// </summary>
public class PlayerThrowingItemEventArgs : EventArgs, IPlayerEvent, IItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerThrowingItemEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is throwing the item.</param>
    /// <param name="item">The item that is being thrown.</param>
    /// <param name="rigidbody">The rigidbody of the item.</param>
    public PlayerThrowingItemEventArgs(ReferenceHub player, Item item, Rigidbody rigidbody)
    {
        Player = Player.Get(player);
        Item = item;
        Rigidbody = rigidbody;
    }

    /// <summary>
    /// Gets the player who is throwing the item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that is being thrown.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// Gets the rigidbody of the item.
    /// </summary>
    public Rigidbody Rigidbody { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}