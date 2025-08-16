using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RequestingRaPlayerInfo"/> event.
/// </summary>
public class PlayerRequestingRaPlayerInfoEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRequestingRaPlayerInfoEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">THe <see cref="CommandSender"/> instance of the player making the request.</param>
    /// <param name="targetHub">The <see cref="ReferenceHub"/> component of the target player.</param>
    /// <param name="isSensitiveInfo">Whether the info being requested is sensitive.</param>
    /// <param name="hasSensitiveInfoPerms">Whether the player has perms to view sensitive info.</param>
    /// <param name="hasUserIdPerms">Whether the player has perms to view the targets user id.</param>
    /// <param name="infoBuilder">The <see cref="StringBuilder"/> used to construct the response message.</param>
    public PlayerRequestingRaPlayerInfoEventArgs(
        CommandSender commandSender,
        ReferenceHub targetHub,
        bool isSensitiveInfo,
        bool hasSensitiveInfoPerms,
        bool hasUserIdPerms,
        StringBuilder infoBuilder)
    {
        Player = Player.Get(commandSender)!;
        Target = Player.Get(targetHub);
        IsSensitiveInfo = isSensitiveInfo;
        HasSensitiveInfoPerms = hasSensitiveInfoPerms;
        HasUserIdPerms = hasUserIdPerms;
        InfoBuilder = infoBuilder;
        IsAllowed = true;
    }

    /// <summary>
    /// The player than made the request.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target player selected.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets or sets whether the player is requesting sensitive info.
    /// </summary>
    public bool IsSensitiveInfo { get; set; }

    /// <summary>
    /// Gets or sets whether the player has permissions to view sensitive info.
    /// </summary>
    /// <remarks>
    /// If <see cref="IsSensitiveInfo"/> is <see langword="true"/> and this is <see langword="false"/> no response is sent.
    /// </remarks>
    public bool HasSensitiveInfoPerms { get; set; }

    /// <summary>
    /// Gets or sets whether the player has permission to view the <see cref="Target"/> <see cref="Player.UserId"/>.
    /// </summary>
    public bool HasUserIdPerms { get; set; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the response message.
    /// </summary>
    public StringBuilder InfoBuilder { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
