using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a candy item.
/// </summary>
public interface ICandyItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => CandyItem;

    /// <summary>
    /// The candy that is involved in the event.
    /// </summary>
    public Scp330Item? CandyItem { get; }
}
