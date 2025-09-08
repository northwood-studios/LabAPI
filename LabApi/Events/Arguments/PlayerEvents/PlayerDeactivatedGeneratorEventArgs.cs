using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DeactivatedGenerator"/> event.
/// </summary>
public class PlayerDeactivatedGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDeactivatedGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who deactivated the generator.</param>
    /// <param name="generator">The generator.</param>
    public PlayerDeactivatedGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator)
    {
        Player = Player.Get(hub);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who deactivated the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator.
    /// </summary>
    public Generator Generator { get; }
}