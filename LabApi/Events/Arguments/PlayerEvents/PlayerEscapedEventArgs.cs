using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using Respawning;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Escaped"/> event.
/// </summary>
public class PlayerEscapedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEscapedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who escaped.</param>
    /// <param name="newRole">The new role.</param>
    /// <param name="ticketTeam">The team that is granted tickets.</param>
    /// <param name="tokens">The amount of tokens granted to team after escape.</param>
    public PlayerEscapedEventArgs(ReferenceHub player, RoleTypeId newRole, SpawnableTeamType ticketTeam, float tokens)
    {
        Player = Player.Get(player);
        NewRole = newRole;
        TicketTeam = ticketTeam;
        Tokens = tokens;
    }

    /// <summary>
    /// Gets the player who escaped.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the new role.
    /// </summary>
    public RoleTypeId NewRole { get; }

    /// <summary>
    /// Gets the team that is granted tickets.
    /// </summary>
    public SpawnableTeamType TicketTeam { get; }

    /// <summary>
    /// Gets the amount of tokens granted to team after escape.
    /// </summary>
    public float Tokens { get; }
}