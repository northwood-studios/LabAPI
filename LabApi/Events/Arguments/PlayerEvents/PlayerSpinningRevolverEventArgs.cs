using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SpinningRevolver"/> event.
/// </summary>
public class PlayerSpinningRevolverEventArgs : EventArgs, IPlayerEvent, IRevolverItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpinningRevolverEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is attempting to spin the revolver.</param>
    /// <param name="weapon">The revolver firearm.</param>
    public PlayerSpinningRevolverEventArgs(ReferenceHub hub, Firearm weapon)
    {
        Player = Player.Get(hub);
        Revolver = (RevolverFirearm)FirearmItem.Get(weapon);
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player who is attempting to spin the revolver.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the revolver item.
    /// </summary>
    public RevolverFirearm Revolver { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
