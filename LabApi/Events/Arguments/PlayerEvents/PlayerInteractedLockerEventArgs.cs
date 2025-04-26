using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseLocker = MapGeneration.Distributors.Locker;
using BaseLockerChamber = MapGeneration.Distributors.LockerChamber;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedLocker"/> event.
/// </summary>
public class PlayerInteractedLockerEventArgs : EventArgs, IPlayerEvent, ILockerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedLockerEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is interacted with the locker.</param>
    /// <param name="locker">The locker that was interacted with.</param>
    /// <param name="chamber">The chamber that was targeted.</param>
    /// <param name="canOpen">Whether the player was allowed to open it.</param>
    public PlayerInteractedLockerEventArgs(ReferenceHub player, BaseLocker locker, BaseLockerChamber chamber, bool canOpen)
    {
        Player = Player.Get(player);
        Locker = Locker.Get(locker);
        Chamber = LockerChamber.Get(chamber);
        CanOpen = canOpen;
    }

    /// <summary>
    /// Gets the player who is interacted with the locker.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the locker that was interacted with.
    /// </summary>
    public Locker Locker { get; }

    /// <summary>
    /// Gets the chamber that was targeted.
    /// </summary>
    public LockerChamber Chamber { get; }

    /// <summary>
    /// Gets the boolean whether the player was allowed to open it.
    /// </summary>
    public bool CanOpen { get; }
}