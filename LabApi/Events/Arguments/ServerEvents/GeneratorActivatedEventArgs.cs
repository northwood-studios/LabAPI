using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.GeneratorActivated"/> event.
/// </summary>
public class GeneratorActivatedEventArgs : EventArgs, IGeneratorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneratorActivatedEventArgs"/> class.
    /// </summary>
    /// <param name="generator">The generator which was activated.</param>
    public GeneratorActivatedEventArgs(Scp079Generator generator)
    {
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the generator which was activated.
    /// </summary>
    public Generator Generator { get; }
}
