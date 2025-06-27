using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ValidatedVisibility"/> event.
/// </summary>
public class PlayerValidatedVisibilityEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerValidatedVisibilityEventArgs"/> class.
    /// </summary>
    /// <param name="observer">The observing player.</param>
    /// <param name="target">The target player.</param>
    /// <param name="isVisible">The observers visibility to the target.</param>
    public PlayerValidatedVisibilityEventArgs(ReferenceHub observer, ReferenceHub target, bool isVisible)
    {
        Player = Player.Get(observer);
        Target = Player.Get(target);
        IsVisible = isVisible;
    }

    /// <summary>
    /// The observing player that checked if they could see the target.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target player that was checked for visibility from the observer.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets or sets whether the observer <see cref="Player"/> could see the target <see cref="Target"/>.
    /// </summary>
    public bool IsVisible { get; set; }
}