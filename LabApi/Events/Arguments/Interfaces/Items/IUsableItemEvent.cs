using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a usable item.
/// </summary>
public interface IUsableItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => UsableItem;

    /// <summary>
    /// The usable that is involved in the event.
    /// </summary>
    public UsableItem? UsableItem { get; }
}
