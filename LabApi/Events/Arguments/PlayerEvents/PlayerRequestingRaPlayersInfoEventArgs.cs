using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RequestingRaPlayersInfo"/> event.
/// </summary>
public class PlayerRequestingRaPlayersInfoEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    private readonly IEnumerable<ReferenceHub> _targets;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRequestingRaPlayersInfoEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">The <see cref="CommandSender"/> instance of the player making the request.</param>
    /// <param name="targets">The reference hub components of the targets selected by the player.</param>
    /// <param name="isSensitiveInfo">Whether the player requested sensitive info.</param>
    /// <param name="hasSensitiveInfoPerms">Whether the player has perms to view sensitive info.</param>
    /// <param name="hasUserIdPerms">Whether the player has perms to view the user ids of the targets.</param>
    /// <param name="infoBuilder">The <see cref="StringBuilder"/> used to construct the response message.</param>
    public PlayerRequestingRaPlayersInfoEventArgs(
        CommandSender commandSender,
        IEnumerable<ReferenceHub> targets,
        bool isSensitiveInfo,
        bool hasSensitiveInfoPerms,
        bool hasUserIdPerms,
        StringBuilder infoBuilder)
    {
        Player = Player.Get(commandSender)!;
        IsSensitiveInfo = isSensitiveInfo;
        HasSensitiveInfoPerms = hasSensitiveInfoPerms;
        HasUserIdPerms = hasUserIdPerms;
        InfoBuilder = infoBuilder;
        IsAllowed = true;
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
    /// Gets or sets whether the <see cref="Player"/> requested sensitive info.
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
    /// Gets or sets whether the <see cref="Player"/> has permission to view the <see cref="Player.UserId"/> of the <see cref="Targets"/>.
    /// </summary>
    public bool HasUserIdPerms { get; set; }

    /// <summary>
    /// Gets the <see cref="StringBuilder"/> used to construct the response message.
    /// </summary>
    public StringBuilder InfoBuilder { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
