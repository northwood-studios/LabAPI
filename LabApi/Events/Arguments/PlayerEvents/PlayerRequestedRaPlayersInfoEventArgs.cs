using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RequestedRaPlayersInfo"/> event.
/// </summary>
public class PlayerRequestedRaPlayersInfoEventArgs : EventArgs, IPlayerEvent
{
    private readonly IEnumerable<ReferenceHub> _targets;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRequestedRaPlayersInfoEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">The <see cref="CommandSender"/> instance of the player making the request.</param>
    /// <param name="targets">The reference hub components of the targets selected in the request.</param>
    /// <param name="isSensitiveInfo">Whether the info being requested is sensitive.</param>
    /// <param name="hasUserIdPerms">Whether the player has perms to view the user ids of the targets.</param>
    /// <param name="infoBuilder">The <see cref="StringBuilder"/> used to build the response.</param>
    /// <param name="idBuilder">The <see cref="StringBuilder"/> used to build the clipboard text for the targets ids.</param>
    /// <param name="ipBuilder">The <see cref="StringBuilder"/> used to build the clipboard text for the targets IPs.</param>
    /// <param name="userIdBuilder">The <see cref="StringBuilder"/> used to build the clipboard text for the targets user ids.</param>
    public PlayerRequestedRaPlayersInfoEventArgs(
        CommandSender commandSender,
        IEnumerable<ReferenceHub> targets,
        bool isSensitiveInfo,
        bool hasUserIdPerms,
        StringBuilder infoBuilder,
        StringBuilder idBuilder,
        StringBuilder ipBuilder,
        StringBuilder userIdBuilder)
    {
        Player = Player.Get(commandSender)!;
        IsSensitiveInfo = isSensitiveInfo;
        HasUserIdPerms = hasUserIdPerms;
        InfoBuilder = infoBuilder;
        PlayerIdBuilder = idBuilder;
        IpBuilder = ipBuilder;
        UserIdBuilder = userIdBuilder;
        _targets = targets;
    }

    /// <summary>
    /// The player that made the request.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target players selected.
    /// </summary>
    public IEnumerable<Player> Targets => _targets.Select(Player.Get)!;

    /// <summary>
    /// Gets whether the <see cref="Player"/> requested sensitive info.
    /// </summary>
    public bool IsSensitiveInfo { get; }

    /// <summary>
    /// Gets whether the <see cref="Player"/> has perms to view the <see cref="Player.UserId"/> of the <see cref="Targets"/>.
    /// </summary>
    public bool HasUserIdPerms { get; }

    /// <summary>
    /// Gets the <see cref="StringBuilder"/> used to construct the response message.
    /// </summary>
    public StringBuilder InfoBuilder { get; }

    /// <summary>
    /// Gets the <see cref="StringBuilder"/> used to construct the clipboard text for the <see cref="Targets"/> <see cref="Player.PlayerId"/>.
    /// </summary>
    public StringBuilder PlayerIdBuilder { get; }

    /// <summary>
    /// Gets the <see cref="StringBuilder"/> used to construct the clipboard text for the <see cref="Targets"/> <see cref="Player.IpAddress"/>.
    /// </summary>
    public StringBuilder IpBuilder { get; }

    /// <summary>
    /// Gets the <see cref="StringBuilder"/> used to construct the clipboard text for the <see cref="Targets"/> <see cref="Player.UserId"/>.
    /// </summary>
    public StringBuilder UserIdBuilder { get; }
}
