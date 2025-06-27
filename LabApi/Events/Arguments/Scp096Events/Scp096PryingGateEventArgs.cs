using Interactables.Interobjects;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.PryingGate"/> event.
/// </summary>
public class Scp096PryingGateEventArgs : EventArgs, IPlayerEvent, IGateEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096PryingGateEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    /// <param name="gate">The affected pryable door instance.</param>
    public Scp096PryingGateEventArgs(ReferenceHub player, PryableDoor gate)
    {
        Player = Player.Get(player);
        Gate = Gate.Get(gate);
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected pryable door instance.
    /// </summary>
    public Gate Gate { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}