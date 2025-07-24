using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseLocker = MapGeneration.Distributors.Locker;
using BaseLockerChamber = MapGeneration.Distributors.LockerChamber;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingLocker"/> event.
/// </summary>
public class PlayerInteractingLockerEventArgs : EventArgs, IPlayerEvent, ILockerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractingLockerEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is interacting with the locker.</param>
    /// <param name="locker">The locker that is being interacted with.</param>
    /// <param name="chamber">The chamber that is being targeted.</param>
    /// <param name="canOpen">Whether the player is allowed to open it.</param>
    public PlayerInteractingLockerEventArgs(ReferenceHub hub, BaseLocker locker, BaseLockerChamber chamber, bool canOpen)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Locker = Locker.Get(locker);
        Chamber = LockerChamber.Get(chamber);
        CanOpen = canOpen;
    }

    /// <summary>
    /// Gets the player who is interacting with the locker.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the locker that is being interacted with.
    /// </summary>
    public Locker Locker { get; }

    /// <summary>
    /// Gets the chamber that is being targeted.
    /// </summary>
    public LockerChamber Chamber { get; }

    /// <summary>
    /// Gets whether the player is allowed to open it.
    /// </summary>
    public bool CanOpen { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}