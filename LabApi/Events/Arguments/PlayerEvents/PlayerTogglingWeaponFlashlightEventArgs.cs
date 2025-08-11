using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.TogglingWeaponFlashlight"/> event.
/// </summary>
public class PlayerTogglingWeaponFlashlightEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTogglingWeaponFlashlightEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is toggling the flashlight.</param>
    /// <param name="item">The flashlight item.</param>
    /// <param name="newState">The new state of the flashlight.</param>
    public PlayerTogglingWeaponFlashlightEventArgs(ReferenceHub hub, Firearm item, bool newState)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        FirearmItem = FirearmItem.Get(item);
        NewState = newState;
    }

    /// <summary>
    /// Gets the player who is toggling the flashlight.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the flashlight item.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <summary>
    /// Gets the new state of the flashlight.
    /// </summary>
    public bool NewState { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Item => FirearmItem;
}
