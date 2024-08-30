using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that is related to a ragdoll.
/// </summary>
public interface IRagdollEvent
{
    /// <summary>
    /// Gets the ragdoll that is related to the event.
    /// </summary>
    public Ragdoll Ragdoll { get; }
}