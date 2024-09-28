using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.GetGroup"/> event.
/// </summary>
public class PlayerGetGroupEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerGetGroupEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose group changed.</param>
    /// <param name="group">The new group.</param>
    public PlayerGetGroupEventArgs(ReferenceHub player, UserGroup group)
    {
        Player = Player.Get(player);
        Group = group;
    }

    /// <summary>
    /// Gets the player whose group changed.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the new group.
    /// </summary>
    public UserGroup Group { get; }
}