using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a projectile.
/// </summary>
public interface IProjectileEvent : IPickupEvent
{
    /// <inheritdoc />
    Pickup? IPickupEvent.Pickup => Projectile;

    /// <summary>
    /// The projectile that is involved in the event.
    /// </summary>
    public Projectile? Projectile { get; }
}