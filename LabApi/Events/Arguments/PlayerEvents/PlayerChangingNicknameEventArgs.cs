using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangingNickname"/> event.
/// </summary>
public class PlayerChangingNicknameEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangingNicknameEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player whose nickname is changing.</param>
    /// <param name="oldNickname">The old nickname of the player.</param>
    /// <param name="newNickname">The new nickname of the player.</param>
    public PlayerChangingNicknameEventArgs(ReferenceHub hub, string? oldNickname, string? newNickname)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        OldNickname = oldNickname;
        NewNickname = newNickname;
    }

    /// <summary>
    /// Gets the player whose role is changing.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old nickname of the player.
    /// </summary>
    /// <remarks>Null means they did not have a custom display-name before.</remarks>
    public string? OldNickname { get; }

    /// <summary>
    /// Gets or sets the new nickname of the player.
    /// </summary>
    /// <remarks>Null means their regular name will be used.</remarks>
    public string? NewNickname { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}