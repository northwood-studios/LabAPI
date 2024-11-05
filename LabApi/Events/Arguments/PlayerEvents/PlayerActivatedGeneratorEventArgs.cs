using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ActivatedGenerator"/> event.
/// </summary>
public class PlayerActivatedGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerActivatedGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who activated the generator.</param>
    /// <param name="generator">The generator that the player has activated.</param>
    public PlayerActivatedGeneratorEventArgs(ReferenceHub player, Scp079Generator generator)
    {
        Player = Player.Get(player);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who activated the generator.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Generator Generator { get; }
}