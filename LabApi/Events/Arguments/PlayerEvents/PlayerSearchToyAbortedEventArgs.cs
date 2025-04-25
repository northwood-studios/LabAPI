using AdminToys;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchToyAborted"/> event.
/// </summary>
public class PlayerSearchToyAbortedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance for the <see cref="PlayerSearchToyAbortedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player that canceled the search on the toy.</param>
    /// <param name="toy">The toy that was being searched.</param>
    public PlayerSearchToyAbortedEventArgs(ReferenceHub player, InvisibleInteractableToy toy)
    {
        Player = Player.Get(player);
        Toy = toy;
    }

    /// <summary>
    /// Gets the player that canceled the search on the toy.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the toy that was being searched.
    /// </summary>
    public InvisibleInteractableToy Toy { get; }
}
