using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using static Escape;

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
    /// <param name="tokens">The amount of tokens granted to team after escape.</param>
    public PlayerEscapedEventArgs(ReferenceHub player, RoleTypeId newRole, EscapeScenarioType escapeScenarioType)
    {
        Player = Player.Get(player);
        NewRole = newRole;
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
    /// Escape scenario of the player.
    /// </summary>
    public EscapeScenarioType EscapeScenarioType { get; }
}