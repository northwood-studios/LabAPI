using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.PickupCreated"/> event.
/// </summary>
public class PickupCreatedEventArgs : EventArgs, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PickupCreatedEventArgs"/> class.
    /// </summary>
    /// <param name="pickup">The pickup which was created.</param>
    public PickupCreatedEventArgs(Pickup pickup)
    {
        Pickup = pickup;
    }

    /// <summary>
    /// Gets the pickup which was created.
    /// </summary>
    public Pickup Pickup { get; }
}
