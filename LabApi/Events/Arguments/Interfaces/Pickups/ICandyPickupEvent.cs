using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a candy pickup.
/// </summary>
public interface ICandyPickupEvent : IPickupEvent
{
    /// <inheritdoc />
    Pickup? IPickupEvent.Pickup => CandyPickup;

    /// <summary>
    /// The candy pickup that is involved in the event.
    /// </summary>
    public Scp330Pickup? CandyPickup { get; }
}
