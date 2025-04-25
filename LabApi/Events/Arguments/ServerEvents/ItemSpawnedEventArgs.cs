using InventorySystem.Items.Pickups;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ItemSpawned"/> event.
/// </summary>
public class ItemSpawnedEventArgs : EventArgs, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemSpawnedEventArgs"/> class.
    /// </summary>
    /// <param name="pickup">The pickup which spawned on map.</param>
    public ItemSpawnedEventArgs(ItemPickupBase pickup)
    {
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the pickup which spawned on map.
    /// </summary>
    public Pickup Pickup { get; }
}
