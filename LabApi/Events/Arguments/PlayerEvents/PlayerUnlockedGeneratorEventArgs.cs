using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnlockedGenerator"/> event.
/// </summary>
public class PlayerUnlockedGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnlockedGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who unlocked the generator.</param>
    /// <param name="generator">The generator that the player has unlocked.</param>
    public PlayerUnlockedGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator)
    {
        Player = Player.Get(hub);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who has unlocked the generator.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Generator Generator { get; }
}
