using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a radio item.
/// </summary>
public interface IRadioItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => RadioItem;

    /// <summary>
    /// The radio that is involved in the event.
    /// </summary>
    public RadioItem? RadioItem { get; }
}
