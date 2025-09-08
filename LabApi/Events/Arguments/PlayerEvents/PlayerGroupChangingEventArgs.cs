using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.GroupChanging"/> event.
/// </summary>
public class PlayerGroupChangingEventArgs : EventArgs, IPlayerEvent, IGroupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerGroupChangingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player whose group is changing.</param>
    /// <param name="group">The new group.</param>
    public PlayerGroupChangingEventArgs(ReferenceHub hub, UserGroup? group)
    {
        Player = Player.Get(hub);
        Group = group;
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player whose group is changing.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the new group.
    /// </summary>
    public UserGroup? Group { get; set; }

    /// <inheritdoc cref="ICancellableEvent.IsAllowed"/>
    public bool IsAllowed { get; set; }
}