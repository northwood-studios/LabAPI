using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.DoorRepairing"/> event.
/// </summary>
public class DoorRepairingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoorRepairingEventArgs"/> class.
    /// </summary>
    /// <param name="door">The door that is repairing.</param>
    /// <param name="remainingHealth">The remaining health for this door.</param>
    public DoorRepairingEventArgs(DoorVariant door, float remainingHealth)
    {
        Door = Door.Get(door);
        RemainingHealth = remainingHealth;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets the current Door.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets or sets the remaining health of the <see cref="Door"/>.
    /// </summary>
    public float RemainingHealth { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
