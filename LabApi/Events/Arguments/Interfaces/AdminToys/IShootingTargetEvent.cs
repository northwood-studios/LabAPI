namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a shooting target.
/// </summary>
public interface IShootingTargetEvent : IAdminToyEvent
{
    /// <inheritdoc />
    AdminToys.AdminToyBase? IAdminToyEvent.AdminToy => ShootingTarget;

    /// <summary>
    /// The shooting target that is involved in the event.
    /// </summary>
    // TODO: use wrapper instead.
    AdminToys.ShootingTarget? ShootingTarget { get; }
}
