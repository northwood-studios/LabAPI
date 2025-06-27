using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a body armor item.
/// </summary>
public interface IBodyArmorItemEvent : IItemEvent
{
    /// <inheritdoc />
    Item? IItemEvent.Item => BodyArmorItem;

    /// <summary>
    /// The body armor that is involved in the event.
    /// </summary>
    public BodyArmorItem? BodyArmorItem { get; }
}