using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RequestedRaPlayerInfo"/> event.
/// </summary>
public class PlayerRequestedRaPlayerInfoEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRequestedRaPlayerInfoEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">The <see cref="CommandSender"/> instance of the player making the request.</param>
    /// <param name="targetHub">The <see cref="ReferenceHub"/> component of the selected target.</param>
    /// <param name="isSensitiveInfo">Whether the info being requested is sensitive.</param>
    /// <param name="hasUserIdPerms">Whether the player has perms to view the user id of the target.</param>
    /// <param name="infoBuilder">The <see cref="StringBuilder"/> used to construct the response message.</param>
    /// <param name="idBuilder">The <see cref="StringBuilder"/> used to construct the clipboard text of the targets player id.</param>
    /// <param name="ipBuilder">The <see cref="StringBuilder"/> used to construct the clipboard text of the targets ip address.</param>
    /// <param name="userIdBuilder">The <see cref="StringBuilder"/> used to construct the clipboard text of the targets user id.</param>
    public PlayerRequestedRaPlayerInfoEventArgs(CommandSender commandSender, ReferenceHub targetHub, bool isSensitiveInfo,
        bool hasUserIdPerms, StringBuilder infoBuilder, StringBuilder idBuilder, StringBuilder ipBuilder, StringBuilder userIdBuilder)
    {
        Player = Player.Get(commandSender)!;
        Target = Player.Get(targetHub);
        IsSensitiveInfo = isSensitiveInfo;
        HasUserIdPerms = hasUserIdPerms;
        InfoBuilder = infoBuilder;
        PlayerIdBuilder = idBuilder;
        IpBuilder = ipBuilder;
        UserIdBuilder = userIdBuilder;
    }

    /// <summary>
    /// The player that made the request.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target player selected.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Whether the <see cref="Player"/> requested sensitive info.
    /// </summary>
    public bool IsSensitiveInfo { get; }

    /// <summary>
    /// Whether the <see cref="Player"/> has permission to view the user id of the <see cref="Target"/>.
    /// </summary>
    public bool HasUserIdPerms { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the response message.
    /// </summary>
    public StringBuilder InfoBuilder { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the clipboard text of the targets player id.
    /// </summary>
    public StringBuilder PlayerIdBuilder { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the clipboard text of the targets ip address.
    /// </summary>
    public StringBuilder IpBuilder { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the clipboard text of the targets user id.
    /// </summary>
    public StringBuilder UserIdBuilder { get; }
}
