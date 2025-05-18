using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.GeneratorActivating"/> event.
/// </summary>
public class GeneratorActivatingEventArgs : EventArgs, IGeneratorEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneratorActivatingEventArgs"/> class.
    /// </summary>
    /// <param name="generator">The generator which is being activated.</param>
    public GeneratorActivatingEventArgs(Scp079Generator generator)
    {
        IsAllowed = true;
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the generator which is being activated.
    /// </summary>
    public Generator Generator { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}