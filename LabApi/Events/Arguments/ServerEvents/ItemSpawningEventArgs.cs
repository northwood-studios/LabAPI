using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ItemSpawning"/> event.
/// </summary>
public class ItemSpawningEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemSpawningEventArgs"/> class.
    /// </summary>
    /// <param name="type">The type of item which will spawn on map</param>
    public ItemSpawningEventArgs(ItemType type)
    {
        IsAllowed = true;
        ItemType = type;
    }

    /// <summary>
    /// Gets or sets type of item which will spawn.
    /// </summary>
    public ItemType ItemType { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
