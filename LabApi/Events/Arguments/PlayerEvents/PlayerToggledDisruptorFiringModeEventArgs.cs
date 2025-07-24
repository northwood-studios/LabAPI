using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ToggledDisruptorFiringMode"/> event.
/// </summary>
public class PlayerToggledDisruptorFiringModeEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerToggledDisruptorFiringModeEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who toggled the firing mode.</param>
    /// <param name="weapon">The weapon that the player toggled.</param>
    /// <param name="singleShot">Whether the mode is now single shot.</param>
    public PlayerToggledDisruptorFiringModeEventArgs(ReferenceHub hub, Firearm weapon, bool singleShot)
    {
        Player = Player.Get(hub);
        FirearmItem = FirearmItem.Get(weapon);
        SingleShotMode = singleShot;
    }

    /// <summary>
    /// Gets the player who toggled the firing mode.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the weapon that the player toggled.
    /// </summary>
    public FirearmItem FirearmItem { get; }

    /// <summary>
    /// Gets whether the disruptor is in single shot mode.
    /// </summary>
    public bool SingleShotMode { get; }
}

