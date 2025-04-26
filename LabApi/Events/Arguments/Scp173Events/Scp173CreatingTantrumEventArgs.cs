using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.CreatingTantrum"/> event.
/// </summary>
public class Scp173CreatingTantrumEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp173CreatingTantrumEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-173 player instance.</param>
    public Scp173CreatingTantrumEventArgs(ReferenceHub player)
    {
        IsAllowed = true;
        Player = Player.Get(player);
    }

    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}