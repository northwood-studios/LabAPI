using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingLocker"/> event.
/// </summary>
public class PlayerInteractingLockerEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractingLockerEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is interacting with the locker.</param>
    /// <param name="locker">The locker that is being interacted with.</param>
    /// <param name="chamber">The chamber that is being targered.</param>
    /// <param name="canOpen">Whenever the player is allowed to open it.</param>
    public PlayerInteractingLockerEventArgs(ReferenceHub player, Locker locker, LockerChamber chamber, bool canOpen)
    {
        Player = Player.Get(player);
        Locker = locker;
        Chamber = chamber;
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
    /// Gets the chamber that is being targered.
    /// </summary>
    public LockerChamber Chamber { get; }

    /// <summary>
    /// Gets whether the player is allowed to open it.
    /// </summary>
    public bool CanOpen { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}