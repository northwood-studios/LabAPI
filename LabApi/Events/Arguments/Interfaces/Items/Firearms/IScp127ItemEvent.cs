using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves SCP-127.
/// </summary>
public interface IScp127ItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => Scp127Item;

    /// <summary>
    /// The SCP-127 item that is involved in the event.
    /// </summary>
    public Scp127Firearm? Scp127Item { get; }
}

