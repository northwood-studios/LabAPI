using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingDoor"/> event.
/// </summary>
public class PlayerInteractingDoorEventArgs : EventArgs, IDoorEvent, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractingDoorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is interacting with the door.</param>
    /// <param name="door">The door that is being interacted with.</param>
    /// <param name="canOpen">Whenever player can open the door.</param>
    public PlayerInteractingDoorEventArgs(ReferenceHub hub, DoorVariant door, bool canOpen)
    {
        IsAllowed = true;
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
    /// Gets whether player can open the door.
    /// </summary>
    public bool CanOpen { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}