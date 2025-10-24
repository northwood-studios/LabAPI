using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.DoorDamaged"/> event.
/// </summary>
public class DoorDamagedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoorDamagedEventArgs"/> class.
    /// </summary>
    /// <param name="door">The door that was damaged.</param>
    /// <param name="hp">The damage to apply this door.</param>
    /// <param name="type">The type of damage this door received.</param>
    public DoorDamagedEventArgs(DoorVariant door, float hp, DoorDamageType type)
    {
        Door = Door.Get(door);
        Damage = hp;
        DamageType = type;
    }

    /// <summary>
    /// Gets the current Door.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets the damage value.
    /// </summary>
    public float Damage { get; }

    /// <summary>
    /// Gets the damage type.
    /// </summary>
    public DoorDamageType DamageType { get; }
}
