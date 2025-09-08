using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.OpeningGenerator"/> event.
/// </summary>
public class PlayerOpeningGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerOpeningGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is opening the generator.</param>
    /// <param name="generator">The generator.</param>
    public PlayerOpeningGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
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

    /// <summary>
    /// Gets or sets whether to play the denied permissions sound.
    /// </summary>
    public bool PlayDeniedAnimation { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}