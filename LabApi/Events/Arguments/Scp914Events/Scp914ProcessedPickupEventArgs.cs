using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Pickups;
using Scp914;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when a pickup has been processed by SCP-914.
/// </summary>
public class Scp914ProcessedPickupEventArgs : EventArgs, IScp914Event, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ProcessedPickupEventArgs"/> class.
    /// </summary>
    /// <param name="oldItemType">The old item type of the pickup.</param>
    /// <param name="newPosition">The new position of the pickup.</param>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="pickup">The new pickup that has been processed by SCP-914.</param>
    public Scp914ProcessedPickupEventArgs(ItemType oldItemType, Vector3 newPosition, Scp914KnobSetting knobSetting, Pickup pickup)
    {
        OldItemType = oldItemType;
        NewPosition = newPosition;
        KnobSetting = knobSetting;
        Pickup = pickup;
    }
    
    /// <summary>
    /// Gets the old item type of the pickup.
    /// </summary>
    public ItemType OldItemType { get; }
    
    /// <summary>
    /// The new position of the pickup.
    /// </summary>
    public Vector3 NewPosition { get; }
    
    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; }

    /// <summary>
    /// The new pickup that has been processed by SCP-914.
    /// </summary>
    public Pickup Pickup { get; }
}