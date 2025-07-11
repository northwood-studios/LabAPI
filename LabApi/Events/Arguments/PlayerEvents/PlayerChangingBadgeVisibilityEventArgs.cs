using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangingBadgeVisibility"/> event.
/// </summary>
public class PlayerChangingBadgeVisibilityEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangingBadgeVisibilityEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> component of the player that is changing their badge visibility.</param>
    /// <param name="isGlobal">Whether the badge is a global badge.</param>
    /// <param name="newVisibility">The new visibility state.</param>
    public PlayerChangingBadgeVisibilityEventArgs(ReferenceHub hub, bool isGlobal, bool newVisibility)
    {
        Player = Player.Get(hub);
        IsGlobalBadge = isGlobal;
        NewVisibility = newVisibility;
        IsAllowed = true;
    }

    /// <summary>
    /// The player that is changing their badge visibility.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// <see langword="true"/> if the badge is a global badge, otherwise it is a local badge.
    /// </summary>
    public bool IsGlobalBadge { get; }

    /// <summary>
    /// Whether the badge will be visible.
    /// </summary>
    public bool NewVisibility { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
