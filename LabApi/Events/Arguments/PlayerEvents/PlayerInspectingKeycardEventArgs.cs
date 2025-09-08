using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseKeycardItem = InventorySystem.Items.Keycards.KeycardItem;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InspectingKeycard"/> event.
/// </summary>
public class PlayerInspectingKeycardEventArgs : EventArgs, IPlayerEvent, IKeycardItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInspectingKeycardEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who wants to inspect the <see cref="KeycardItem"/>.</param>
    /// <param name="keycardItem">The keycard item.</param>
    public PlayerInspectingKeycardEventArgs(ReferenceHub hub, BaseKeycardItem keycardItem)
    {
        Player = Player.Get(hub);
        KeycardItem = KeycardItem.Get(keycardItem);
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player who wants to inspect <see cref="KeycardItem"/>.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the keycard item.
    /// </summary>
    public KeycardItem KeycardItem { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
