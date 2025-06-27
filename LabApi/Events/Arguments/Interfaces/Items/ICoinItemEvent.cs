using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a coin item.
/// </summary>
public interface ICoinItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => CoinItem;

    /// <summary>
    /// The coin that is involved in the event.
    /// </summary>
    public CoinItem? CoinItem { get; }
}