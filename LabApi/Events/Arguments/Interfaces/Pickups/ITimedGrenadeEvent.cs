using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that is related to a timed grenade.
/// </summary>
public interface ITimedGrenadeEvent : IProjectileEvent
{
    /// <inheritdoc />
    Projectile? IProjectileEvent.Projectile => TimedGrenade;

    /// <summary>
    /// Gets the timed grenade that is involved in the event.
    /// </summary>
    public TimedGrenadeProjectile? TimedGrenade { get; }
}