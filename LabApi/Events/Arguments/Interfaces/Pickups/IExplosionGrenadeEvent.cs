using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an explosive grenade.
/// </summary>
public interface IExplosionGrenadeEvent : IProjectileEvent
{
    /// <inheritdoc />
    Projectile? IProjectileEvent.Projectile => ExplosiveGrenade;

    /// <summary>
    /// The explosive grenade that is involved in the event.
    /// </summary>
    public ExplosiveGrenadeProjectile? ExplosiveGrenade { get; }
}
