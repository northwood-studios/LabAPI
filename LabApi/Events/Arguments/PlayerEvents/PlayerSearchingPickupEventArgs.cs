﻿using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchingPickup"/> event.
/// </summary>
public class PlayerSearchingPickupEventArgs : EventArgs, IPlayerEvent, IPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchingPickupEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is searching for the pickup.</param>
    /// <param name="pickup">The pickup being searched.</param>
    public PlayerSearchingPickupEventArgs(ReferenceHub hub, ItemPickupBase pickup)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who is searching for the pickup.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the pickup being searched.
    /// </summary>
    public Pickup Pickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}