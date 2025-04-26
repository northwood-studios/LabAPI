using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an ammo pickup.
/// </summary>
public interface IAmmoPickupEvent : IPickupEvent
{
    /// <inheritdoc />
    Pickup? IPickupEvent.Pickup => AmmoPickup;

    /// <summary>
    /// The ammo pickup that is involved in the event.
    /// </summary>
    public AmmoPickup? AmmoPickup { get; }
}
