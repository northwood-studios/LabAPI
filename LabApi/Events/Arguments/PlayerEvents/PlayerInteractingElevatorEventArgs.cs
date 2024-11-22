using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using Interactables.Interobjects;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingElevator"/> event.
/// </summary>
public class PlayerInteractingElevatorEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractingElevatorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is interacting with the elevator.</param>
    /// <param name="elevator">The elevator.</param>
    /// <param name="panel">The elevator panel.</param>
    public PlayerInteractingElevatorEventArgs(ReferenceHub player, ElevatorChamber elevator, ElevatorPanel panel)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Elevator = Elevator.Get(elevator);
        Panel = panel;
    }

    /// <summary>
    /// Gets the player who is interacting with the elevator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the elevator.
    /// </summary>
    public Elevator Elevator { get; }

    /// <summary>
    /// Gets the elevator panel.
    /// </summary>
    public ElevatorPanel Panel { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}