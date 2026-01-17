using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DeactivatingGenerator"/> event.
/// </summary>
public class PlayerDeactivatingGeneratorEventArgs : EventArgs, IGeneratorEvent, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDeactivatingGeneratorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is deactivating the generator.</param>
    /// <param name="generator">The generator.</param>
    public PlayerDeactivatingGeneratorEventArgs(ReferenceHub hub, Scp079Generator generator)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the player who is deactivating the generator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the generator.
    /// </summary>
    public Generator Generator { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}