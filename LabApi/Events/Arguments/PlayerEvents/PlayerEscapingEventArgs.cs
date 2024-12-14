using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using Respawning;
using System;
using static Escape;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Escaping"/> event.
/// </summary>
public class PlayerEscapingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEscapingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is escaping.</param>
    /// <param name="newRole">The new role that is set after escape.</param>
    /// <param name="ticketTeam">The team that will be granted tickets.</param>
    /// <param name="tokens">The amount of tokens granted to team after escape.</param>
    public PlayerEscapingEventArgs(ReferenceHub player, RoleTypeId newRole, EscapeScenarioType escapeScenario)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        NewRole = newRole;
        EscapeScenario = escapeScenario;
    }

    /// <summary>
    /// Gets the player who is escaping.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the new role that is set after escape.
    /// </summary>
    public RoleTypeId NewRole { get; set; }

    /// <summary>
    /// Gets or sets the escape scenario 
    /// </summary>
    public EscapeScenarioType EscapeScenario { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}