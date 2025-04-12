using Hazards;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.CreatedTantrum"/> event.
/// </summary>
public class Scp173CreatedTantrumEventArgs : EventArgs, IPlayerEvent, ITantrumEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173CreatedTantrumEventArgs"/> class.
    /// </summary>
    /// <param name="tantrumInstance">The tantrum instance.</param>
    /// <param name="player">The SCP-173 player instance.</param>
    public Scp173CreatedTantrumEventArgs(TantrumEnvironmentalHazard tantrumInstance, ReferenceHub player)
    {
        Tantrum = TantrumHazard.Get(tantrumInstance);
        Player = Player.Get(player);
    }

    /// <summary>
    /// The tantrum instance created by SCP-173.
    /// </summary>
    public TantrumHazard Tantrum { get; }

    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc cref="Tantrum"/>
    [Obsolete($"Use {nameof(Tantrum)} instead")]
    public TantrumEnvironmentalHazard TantrumInstance => Tantrum.Base;
}