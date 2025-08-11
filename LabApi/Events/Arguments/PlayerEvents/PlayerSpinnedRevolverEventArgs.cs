using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SpinnedRevolver"/> event.
/// </summary>
public class PlayerSpinnedRevolverEventArgs : EventArgs, IPlayerEvent, IRevolverItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSpinnedRevolverEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who spinned the revolver.</param>
    /// <param name="weapon">The revolver firearm.</param>
    public PlayerSpinnedRevolverEventArgs(ReferenceHub hub, Firearm weapon)
    {
        Player = Player.Get(hub);
        Revolver = (RevolverFirearm)FirearmItem.Get(weapon);
    }

    /// <summary>
    /// Gets the player who spinned the revolver.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the revolver item.
    /// </summary>
    public RevolverFirearm Revolver { get; }
}
