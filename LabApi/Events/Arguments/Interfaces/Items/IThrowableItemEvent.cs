using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a throwable item.
/// </summary>
public interface IThrowableItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => ThrowableItem;

    /// <summary>
    /// The throwable item that is involved in the event.
    /// </summary>
    public ThrowableItem? ThrowableItem { get; }
}
