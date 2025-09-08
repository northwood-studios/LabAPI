using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedDoor"/> event.
/// </summary>
public class PlayerInteractedDoorEventArgs : EventArgs, IPlayerEvent, IDoorEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedDoorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is interacting with the door.</param>
    /// <param name="door">The door that is being interacted with.</param>
    /// <param name="canOpen">Whenever player can open the door.</param>
    public PlayerInteractedDoorEventArgs(ReferenceHub hub, DoorVariant door, bool canOpen)
    {
        Player = Player.Get(hub);
        Door = Door.Get(door);
        CanOpen = canOpen;
    }

    /// <summary>
    /// Gets the player who is interacting with the door.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the door that is being interacted with.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets boolean whether player can open the door.
    /// </summary>
    public bool CanOpen { get; }
}