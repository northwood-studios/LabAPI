using Interactables.Interobjects;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedDoor"/> event.
/// </summary>
public class PlayerInteractedElevatorEventArgs : EventArgs, IPlayerEvent, IElevatorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedElevatorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who interacted with elevator panel.</param>
    /// <param name="elevator">The elevator chamber.</param>
    /// <param name="panel">The elevator panel that was interaction done with.</param>
    public PlayerInteractedElevatorEventArgs(ReferenceHub hub, ElevatorChamber elevator, ElevatorPanel panel)
    {
        Player = Player.Get(hub);
        Elevator = Elevator.Get(elevator);
        Panel = panel;
    }

    /// <summary>
    /// Gets the player who interacted with elevator panel.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the elevator chamber.
    /// </summary>
    public Elevator Elevator { get; }

    /// <summary>
    /// Gets the elevator panel that was interaction done with.
    /// </summary>
    public ElevatorPanel Panel { get; }
}
