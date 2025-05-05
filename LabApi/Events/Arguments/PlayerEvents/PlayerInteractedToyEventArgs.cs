using AdminToys;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedToy"/> event.
/// </summary>
public class PlayerInteractedToyEventArgs : EventArgs, IPlayerEvent, IInteractableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedToyEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who interacted with the toy.</param>
    /// <param name="toy">The toy instance that was interacted with.</param>
    public PlayerInteractedToyEventArgs(ReferenceHub player, InvisibleInteractableToy toy)
    {
        Player = Player.Get(player);
        Interactable = InteractableToy.Get(toy);
    }

    /// <summary>
    /// Gets the player that interacted with the toy.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the toy the player interacted with.
    /// </summary>
    public InteractableToy Interactable { get; }
}
