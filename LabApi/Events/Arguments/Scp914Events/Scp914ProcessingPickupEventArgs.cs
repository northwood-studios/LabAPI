using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Pickups;
using Scp914;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when a pickup is being processed by SCP-914.
/// </summary>
public class Scp914ProcessingPickupEventArgs : EventArgs, ICancellableEvent, IScp914Event, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ProcessingPickupEventArgs"/> class.
    /// </summary>
    /// <param name="newPosition">The new position of the pickup.</param>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="pickup">The pickup that is being processed by SCP-914.</param>
    public Scp914ProcessingPickupEventArgs(Vector3 newPosition, Scp914KnobSetting knobSetting, Pickup pickup)
    {
        IsAllowed = true;
        NewPosition = newPosition;
        KnobSetting = knobSetting;
        Pickup = pickup;
    }
    
    /// <summary>
    /// The new position of the pickup.
    /// </summary>
    public Vector3 NewPosition { get; set; }
    
    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; set; }

    /// <summary>
    /// The pickup that is being processed by SCP-914.
    /// </summary>
    public Pickup Pickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}