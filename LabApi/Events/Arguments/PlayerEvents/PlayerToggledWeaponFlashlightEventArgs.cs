using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ToggledWeaponFlashlight"/> event.
/// </summary>
public class PlayerToggledWeaponFlashlightEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerToggledWeaponFlashlightEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who toggled the flashlight.</param>
    /// <param name="item">The flashlight item.</param>
    /// <param name="newState">The new state of the flashlight.</param>
    public PlayerToggledWeaponFlashlightEventArgs(ReferenceHub player, Firearm item, bool newState)
    {
        Player = Player.Get(player);
        FirearmItem = FirearmItem.Get(item);
        NewState = newState;
    }

    /// <summary>
    /// Gets the player who toggled the flashlight.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the flashlight item.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <summary>
    /// Gets the new state of the flashlight.
    /// </summary>
    public bool NewState { get; }

    /// <inheritdoc cref="FirearmItem"/>
    [Obsolete($"Use {nameof(FirearmItem)} instead")]
    public Item Item => FirearmItem;
}