using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a light item.
/// </summary>
public interface ILightItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => LightItem;

    /// <summary>
    /// The light item that is involved in the event.
    /// </summary>
    public LightItem? LightItem { get; }
}