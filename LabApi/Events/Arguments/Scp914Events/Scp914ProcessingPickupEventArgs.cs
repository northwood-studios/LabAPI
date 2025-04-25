using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;
using UnityEngine;
using InventorySystem.Items.Pickups;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when a pickup is being processed by SCP-914.
/// </summary>
public class Scp914ProcessingPickupEventArgs : EventArgs, IScp914Event, IPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ProcessingPickupEventArgs"/> class.
    /// </summary>
    /// <param name="newPosition">The new position of the pickup.</param>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="pickup">The pickup that is being processed by SCP-914.</param>
    public Scp914ProcessingPickupEventArgs(Vector3 newPosition, Scp914KnobSetting knobSetting, ItemPickupBase pickup)
    {
        IsAllowed = true;
        NewPosition = newPosition;
        KnobSetting = knobSetting;
        Pickup = Pickup.Get(pickup);
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