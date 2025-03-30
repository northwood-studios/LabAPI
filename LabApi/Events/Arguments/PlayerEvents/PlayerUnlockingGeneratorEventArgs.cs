using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UnlockingGenerator"/> event.
/// </summary>
public class PlayerUnlockingGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUnlockingGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is unlocking the generator.</param>
    /// <param name="generator">The generator that the player is unlocking.</param>
    public PlayerUnlockingGeneratorEventArgs(ReferenceHub player, Scp079Generator generator)
    {
        Player = Player.Get(player);
        Generator = Generator.Get(generator);
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player who is unlocking the generator.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Generator Generator { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}

