using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a weapon.
/// </summary>
public interface IFirearmItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => FirearmItem;

    /// <summary>
    /// The weapon that is involved in the event.
    /// </summary>
    public FirearmItem? FirearmItem { get; }
}
