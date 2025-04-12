using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a structure.
/// </summary>
public interface IStructureEvent
{
    /// <summary>
    /// The structure that is involved in the event.
    /// </summary>
    public Structure? Structure { get; }
}