using Interactables.Interobjects;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static Interactables.Interobjects.ElevatorChamber;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ElevatorSequenceChanged"/> event.
/// </summary>
public class ElevatorSequenceChangedEventArgs : EventArgs, IElevatorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElevatorSequenceChangedEventArgs"/> class.
    /// </summary>
    /// <param name="elevator">The elevator whose sequence has changed.</param>
    /// <param name="oldSequence"> The old sequence the elevator was in before.</param>
    /// <param name="newSequence">The new sequence the elevator has transitioned into.</param>
    public ElevatorSequenceChangedEventArgs(ElevatorChamber elevator, ElevatorSequence oldSequence, ElevatorSequence newSequence)
    {
        Elevator = Elevator.Get(elevator);
        OldSequence = oldSequence;
        NewSequence = newSequence;
    }

    /// <inheritdoc/>
    public Elevator Elevator { get; }

    /// <summary>
    /// The old sequence the elevator was in before.
    /// </summary>
    public ElevatorSequence OldSequence { get; }

    /// <summary>
    /// The new sequence the elevator has transitioned into.
    /// </summary>
    public ElevatorSequence NewSequence { get; }
}
