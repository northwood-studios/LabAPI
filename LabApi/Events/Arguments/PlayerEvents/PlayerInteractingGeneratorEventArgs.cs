using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Facility;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;
using static MapGeneration.Distributors.Scp079Generator;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingGenerator"/> event.
/// </summary>
public class PlayerInteractingGeneratorEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractingGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is interacting with the generator.</param>
    /// <param name="generator">The generator object.</param>
    /// <param name="colliderId">The collider ID.</param>
    public PlayerInteractingGeneratorEventArgs(ReferenceHub player, Scp079Generator generator, GeneratorColliderId colliderId)
    {
        Player = Player.Get(player);
        Generator = Generator.Get(generator);
        ColliderId = colliderId;
    }

    /// <summary>
    /// Gets the player who is interacting with the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator object.
    /// </summary>
    public Generator Generator { get; }

    /// <summary>
    /// Gets the collider ID.
    /// </summary>
    public GeneratorColliderId ColliderId { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}