using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.PickupDestroyed"/> event.
/// </summary>
public class PickupDestroyedEventArgs : EventArgs, IPickupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PickupDestroyedEventArgs"/> class.
    /// </summary>
    /// <param name="pickup">The pickup which is being destroyed.</param>
    public PickupDestroyedEventArgs(Pickup pickup)
    {
        Pickup = pickup;
    }

    /// <summary>
    /// Gets the pickup which is being destroyed.
    /// </summary>
    /// <remarks>
    /// Unity destroys objects at the end of the frame so using the pickup here is fine.
    /// </remarks>
    public Pickup Pickup { get; }
}