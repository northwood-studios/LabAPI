using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.GroupChanged"/> event.
/// </summary>
public class PlayerGroupChangedEventArgs : EventArgs, IPlayerEvent, IGroupEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerGroupChangedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player whose group changed.</param>
    /// <param name="group">The new group.</param>
    public PlayerGroupChangedEventArgs(ReferenceHub hub, UserGroup group)
    {
        Player = Player.Get(hub);
        Group = group;
    }

    /// <summary>
    /// Gets the player whose group changed.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc cref="IGroupEvent.Group"/>
    public UserGroup Group { get; }
}