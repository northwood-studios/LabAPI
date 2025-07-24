using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ClosedGenerator"/> event.
/// </summary>
public class PlayerClosedGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerClosedGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who closed the generator.</param>
    /// <param name="generator">The generator.</param>
    public PlayerClosedGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator)
    {
        Player = Player.Get(hub);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who closed the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator.
    /// </summary>
    public Generator Generator { get; }
}