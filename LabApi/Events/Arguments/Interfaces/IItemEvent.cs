using LabApi.Features.Wrappers.Items;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an item.
/// </summary>
public interface IItemEvent
{
    /// <summary>
    /// The item that is involved in the event.
    /// </summary>
    public Item Item { get; }
}