using Interactables.Interobjects;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.PriedGate"/> event.
/// </summary>
public class Scp096PriedGateEventArgs : EventArgs, IPlayerEvent, IGateEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096PriedGateEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    /// <param name="gate">The affected pryable door instance.</param>
    public Scp096PriedGateEventArgs(ReferenceHub player, PryableDoor gate)
    {
        Player = Player.Get(player);
        Gate = Gate.Get(gate);
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected pryable door instance.
    /// </summary>
    public Gate Gate { get; }
}