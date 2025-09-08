using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ActivatingGenerator"/> event.
/// </summary>
public class PlayerActivatingGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerActivatingGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is activating the generator.</param>
    /// <param name="generator">The generator that the player is activating.</param>
    public PlayerActivatingGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who is activating the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator activating.
    /// </summary>
    public Generator Generator { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}