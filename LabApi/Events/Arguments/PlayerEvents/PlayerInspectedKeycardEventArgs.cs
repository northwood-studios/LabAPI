using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseKeycardItem = InventorySystem.Items.Keycards.KeycardItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InspectedKeycard"/> event.
/// </summary>
public class PlayerInspectedKeycardEventArgs : EventArgs, IPlayerEvent, IKeycardItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInspectingKeycardEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who inspected the <see cref="KeycardItem"/>.</param>
    /// <param name="keycardItem">The keycard item.</param>
    public PlayerInspectedKeycardEventArgs(ReferenceHub hub, BaseKeycardItem keycardItem)
    {
        Player = Player.Get(hub);
        KeycardItem = KeycardItem.Get(keycardItem);
    }

    /// <summary>
    /// Gets the player who inspected the <see cref="KeycardItem"/>.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the keycard item.
    /// </summary>
    public KeycardItem KeycardItem { get; }
}
