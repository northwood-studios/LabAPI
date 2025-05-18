using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a camera.
/// </summary>
public interface ICameraEvent
{
    /// <summary>
    /// The camera that is involved in the event.
    /// </summary>
    public Camera? Camera { get; }
}