using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InspectingItem"/> event.
/// </summary>
public class PlayerInspectingItemEventArgs : EventArgs, IPlayerEvent, IItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInspectingItemEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who wants to inspect the <paramref name="item"/>.</param>
    /// <param name="item">The inspecting item.</param>
    public PlayerInspectingItemEventArgs(ReferenceHub hub, ItemBase item)
    {
        Player = Player.Get(hub);
        Item = Item.Get(item);
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player who wants to inspect <see cref="Features.Wrappers.Item"/>.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item to be inspected.
    /// </summary>
    public Item Item { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
