using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a keycard item.
/// </summary>
public interface IKeycardItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => KeycardItem;

    /// <summary>
    /// The keycard item that is involved in the event.
    /// </summary>
    public KeycardItem KeycardItem { get; }
}

