using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a generator structure.
/// </summary>
public interface IGeneratorEvent : IStructureEvent
{
    /// <inheritdoc />
    Structure? IStructureEvent.Structure => Generator;

    /// <summary>
    /// The generator that is involved in the event.
    /// </summary>
    public Generator? Generator { get; }
}
