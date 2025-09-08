using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a revolver.
/// </summary>
public interface IRevolverItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => Revolver;

    /// <summary>
    /// The revolver that is involved in the event.
    /// </summary>
    public RevolverFirearm? Revolver { get; }
}