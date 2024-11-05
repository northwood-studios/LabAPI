using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a generator.
/// </summary>
public interface IGeneratorEvent
{
    /// <summary>
    /// The generator that is involved in the event.
    /// </summary>
    public Generator Generator { get; }
}