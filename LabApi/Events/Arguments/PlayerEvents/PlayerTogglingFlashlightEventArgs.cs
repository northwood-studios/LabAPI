using InventorySystem.Items.ToggleableLights;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.TogglingFlashlight"/> event.
/// </summary>
public class PlayerTogglingFlashlightEventArgs : EventArgs, IPlayerEvent, ILightItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTogglingFlashlightEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is toggling the flashlight.</param>
    /// <param name="item">The flashlight that is being toggled.</param>
    /// <param name="newState">Whenever the flashlight is being toggled to on or off state.</param>
    public PlayerTogglingFlashlightEventArgs(ReferenceHub hub, ToggleableLightItemBase item, bool newState)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        LightItem = LightItem.Get(item);
        NewState = newState;
    }

    /// <summary>
    /// Gets the player who is toggling the flashlight.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the flashlight that is being toggled.
    /// </summary>
    public LightItem LightItem { get; }

    /// <summary>
    /// Gets whether the flashlight is being toggled to on or off state.
    /// </summary>
    public bool NewState { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="LightItem"/>
    [Obsolete($"Use {nameof(LightItem)} instead")]
    public Item Item => LightItem;
}