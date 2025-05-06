using AdminToys;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchedToy"/> event.
/// </summary>
public class PlayerSearchedToyEventArgs : EventArgs, IPlayerEvent, IInteractableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchedToyEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who searched the toy.</param>
    /// <param name="toy">The toy that was searched.</param>
    public PlayerSearchedToyEventArgs(ReferenceHub player, InvisibleInteractableToy toy)
    {
        Player = Player.Get(player);
        Interactable = InteractableToy.Get(toy);
    }

    /// <summary>
    /// Gets the player who searched the toy.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the toy that was searched.
    /// </summary>
    public InteractableToy Interactable { get; }
}
