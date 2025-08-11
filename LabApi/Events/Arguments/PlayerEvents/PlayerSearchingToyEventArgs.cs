using AdminToys;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchingToy"/>.
/// </summary>
public class PlayerSearchingToyEventArgs : EventArgs, IPlayerEvent, IInteractableEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchingToyEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is searching the toy.</param>
    /// <param name="toy">The toy that is going to be searched.</param>
    public PlayerSearchingToyEventArgs(ReferenceHub player, InvisibleInteractableToy toy)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Interactable = InteractableToy.Get(toy);
    }

    /// <summary>
    /// Gets the player that is searching the toy.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the toy the player is searching.
    /// </summary>
    public InteractableToy Interactable { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
