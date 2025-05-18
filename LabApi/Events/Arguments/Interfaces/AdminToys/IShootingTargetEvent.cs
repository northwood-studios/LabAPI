using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a shooting target.
/// </summary>
public interface IShootingTargetEvent : IAdminToyEvent
{
    /// <inheritdoc />
    AdminToy? IAdminToyEvent.AdminToy => ShootingTarget;

    /// <summary>
    /// The shooting target that is involved in the event.
    /// </summary>
    public ShootingTargetToy? ShootingTarget { get; }
}