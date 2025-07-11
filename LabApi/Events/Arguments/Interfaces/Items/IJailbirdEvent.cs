using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces.Items;

/// <summary>
/// Represents an event that involves a jailbird item.
/// </summary>
public interface IJailbirdEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => JailbirdItem;

    /// <summary>
    /// The jailbird item that is involved in the event.
    /// </summary>
    public JailbirdItem? JailbirdItem { get; }
}
