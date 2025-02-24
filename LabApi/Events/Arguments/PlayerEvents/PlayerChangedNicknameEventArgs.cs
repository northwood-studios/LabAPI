using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangedNickname"/> event.
/// </summary>
public class PlayerChangedNicknameEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangedNicknameEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose nickname has changed.</param>
    /// <param name="oldNickname">The old nickname of the player.</param>
    /// <param name="newNickname">The new nickname of the player.</param>
    public PlayerChangedNicknameEventArgs(ReferenceHub player, string? oldNickname, string? newNickname)
    {
        Player = Player.Get(player);
        OldNickname = oldNickname;
        NewNickname = newNickname;
    }

    /// <summary>
    /// Gets the player whose nickname has changed.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old nickname of the player.
    /// </summary>
    /// <remarks>Null means they did not have a custom display-name before.</remarks>
    public string? OldNickname { get; }

    /// <summary>
    /// Gets the new nickname of the player.
    /// </summary>
    /// <remarks>Null means their regular name will be used.</remarks>
    public string? NewNickname { get; }
}