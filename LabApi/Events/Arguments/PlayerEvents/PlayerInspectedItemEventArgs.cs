using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InspectingItem"/> event.
/// </summary>
public class PlayerInspectedItemEventArgs : EventArgs, IPlayerEvent, IItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInspectedItemEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who inspected the <paramref name="item"/>.</param>
    /// <param name="item">The inspected item.</param>
    public PlayerInspectedItemEventArgs(ReferenceHub hub, ItemBase item)
    {
        Player = Player.Get(hub);
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the player who inspected the <see cref="Item"/>.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the item that was inspected.
    /// </summary>
    public Item Item { get; }
}
