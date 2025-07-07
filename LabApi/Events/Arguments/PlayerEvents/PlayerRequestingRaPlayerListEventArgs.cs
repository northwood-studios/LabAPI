using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Text;
using static RemoteAdmin.Communication.RaPlayerList;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RequestingRaPlayerList"/> event.
/// </summary>
public class PlayerRequestingRaPlayerListEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRequestingRaPlayerListEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">The <see cref="CommandSender"/> instance of the player that made the request for the RA player list.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> used to build the RA player list.</param>
    /// <param name="isDescending">Whether to sort players by descending order.</param>
    /// <param name="sorting">The <see cref="PlayerSorting"/> mode to use.</param>
    /// <param name="viewHiddenLocalBadges">Whether the requester can view hidden local RA badges or not.</param>
    /// <param name="viewHiddenGlobalBadges">Whether the requester can view hidden global RA badges or not.</param>
    public PlayerRequestingRaPlayerListEventArgs(CommandSender commandSender, StringBuilder builder, bool isDescending,
        PlayerSorting sorting, bool viewHiddenLocalBadges, bool viewHiddenGlobalBadges)
    {
        Player = Player.Get(commandSender)!;
        ListBuilder = builder;
        IsDescending = isDescending;
        Sorting = sorting;
        ViewHiddenLocalBadges = viewHiddenLocalBadges;
        ViewHiddenGlobalBadges = viewHiddenGlobalBadges;
        IsAllowed = true;
    }

    /// <summary>
    /// The player that requested the RA player list.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the RA player list.
    /// </summary>
    /// <remarks>
    /// String builder is empty in this event.
    /// </remarks>
    public StringBuilder ListBuilder { get; }

    /// <summary>
    /// Gets or set whether to sort players by descending order.
    /// </summary>
    public bool IsDescending { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="PlayerSorting"/> used.
    /// </summary>
    public PlayerSorting Sorting { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="Player"/> can view hidden local RA badges.
    /// </summary>
    public bool ViewHiddenLocalBadges { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="Player"/> can view hidden global RA badges.
    /// </summary>
    public bool ViewHiddenGlobalBadges { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
