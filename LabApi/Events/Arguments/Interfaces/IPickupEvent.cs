using LabApi.Features.Wrappers;
namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a pickup.
/// </summary>
public interface IPickupEvent
{
    /// <summary>
    /// The pickup that is involved in the event.
    /// </summary>
    public Pickup Pickup { get; }
}