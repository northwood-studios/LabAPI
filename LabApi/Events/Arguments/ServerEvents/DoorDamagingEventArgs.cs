using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.DoorDamaging"/> event.
/// </summary>
public class DoorDamagingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoorDamagingEventArgs"/> class.
    /// </summary>
    /// <param name="door">The door that is damaging.</param>
    /// <param name="hp">The damage to apply this door.</param>
    /// <param name="type">The type of damage this door received.</param>
    public DoorDamagingEventArgs(DoorVariant door, float hp, DoorDamageType type)
    {
        Door = Door.Get(door);
        Damage = hp;
        DamageType = type;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets the current Door.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets or sets the damage value.
    /// </summary>
    public float Damage { get; set; }

    /// <summary>
    /// Gets or sets the damage type.
    /// </summary>
    public DoorDamageType DamageType { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
