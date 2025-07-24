using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.OpenedGenerator"/> event.
/// </summary>
public class PlayerOpenedGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerOpenedGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who opened the generator.</param>
    /// <param name="generator">The generator that was opened.</param>
    public PlayerOpenedGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator)
    {
        Player = Player.Get(hub);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who opened the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator that was opened.
    /// </summary>
    public Generator Generator { get; }
}