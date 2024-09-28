using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Facility;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.OpeningGenerator"/> event.
/// </summary>
public class PlayerOpeningGeneratorEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerOpeningGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is opening the generator.</param>
    /// <param name="generator">The generator.</param>
    public PlayerOpeningGeneratorEventArgs(ReferenceHub player, Scp079Generator generator)
    {
        Player = Player.Get(player);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who is opening the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator.
    /// </summary>
    public Generator Generator { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}