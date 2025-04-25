using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a body armor pickup.
/// </summary>
public interface IBodyArmorPickupEvent : IPickupEvent
{
    /// <inheritdoc />
    Pickup? IPickupEvent.Pickup => BodyArmorPickup;

    /// <summary>
    /// The body armor pickup that is involved in the event.
    /// </summary>
    public BodyArmorPickup? BodyArmorPickup { get; }
}
