using InventorySystem.Items;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.TogglingWeaponFlashlight"/> event.
/// </summary>
public class PlayerTogglingWeaponFlashlightEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTogglingWeaponFlashlightEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is toggling the flashlight.</param>
    /// <param name="item">The flashlight item.</param>
    /// <param name="newState">The new state of the flashlight.</param>
    public PlayerTogglingWeaponFlashlightEventArgs(ReferenceHub player, ItemBase item, bool newState)
    {
        Player = Player.Get(player);
        Item = Item.Get(item);
        NewState = newState;
    }

    /// <summary>
    /// Gets the player who is toggling the flashlight.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the flashlight item.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// Gets the new state of the flashlight.
    /// </summary>
    public bool NewState { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
