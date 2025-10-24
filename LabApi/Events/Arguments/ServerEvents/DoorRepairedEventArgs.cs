using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.DoorRepaired"/> event.
/// </summary>
public class DoorRepairedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoorRepairedEventArgs"/> class.
    /// </summary>
    /// <param name="door">The door that was repaired.</param>
    /// <param name="remainingHealth">The remaining health for this door.</param>
    public DoorRepairedEventArgs(DoorVariant door, float remainingHealth)
    {
        Door = Door.Get(door);
        RemainingHealth = remainingHealth;
    }

    /// <summary>
    /// Gets the current Door.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets the remaining health of the <see cref="Door"/>.
    /// </summary>
    public float RemainingHealth { get; }
}
