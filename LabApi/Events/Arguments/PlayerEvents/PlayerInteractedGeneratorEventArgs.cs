﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;
using static MapGeneration.Distributors.Scp079Generator;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedGenerator"/> event.
/// </summary>
public class PlayerInteractedGeneratorEventArgs : EventArgs, IPlayerEvent, IGeneratorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who interacted with the generator.</param>
    /// <param name="generator">The generator object.</param>
    /// <param name="colliderId">The collider ID.</param>
    public PlayerInteractedGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator, GeneratorColliderId colliderId)
    {
        Player = Player.Get(hub);
        Generator = Generator.Get(generator);
        ColliderId = colliderId;
    }

    /// <summary>
    /// Gets the player who interacted with the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator object.
    /// </summary>
    public Generator Generator { get; }

    /// <summary>
    /// Gets the collider ID.
    /// </summary>
    public GeneratorColliderId ColliderId { get; }
}