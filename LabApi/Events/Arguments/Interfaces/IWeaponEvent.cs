using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a weapon.
/// </summary>
public interface IWeaponEvent
{
    /// <summary>
    /// The weapon that is involved in the event.
    /// </summary>
    public Item Weapon { get; }
}