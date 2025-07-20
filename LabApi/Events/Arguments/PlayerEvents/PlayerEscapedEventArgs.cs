using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using UnityEngine;
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
    /// <param name="escapeScenarioType">The scenario of the escape.</param>
    /// <param name="oldRole">The old role of the player.</param>
    /// <param name="escapeZone">The bounds of the escape zone that was triggered.</param>
    public PlayerEscapedEventArgs(ReferenceHub player, RoleTypeId oldRole, RoleTypeId newRole, EscapeScenarioType escapeScenarioType, Bounds escapeZone)
    {
        Player = Player.Get(player);
        OldRole = oldRole;
        NewRole = newRole;
        EscapeScenarioType = escapeScenarioType;
        EscapeZone = escapeZone;
    }

    /// <summary>
    /// Gets the player who escaped.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old role of the player.
    /// </summary>
    public RoleTypeId OldRole { get; }

    /// <summary>
    /// Gets the new role.
    /// </summary>
    public RoleTypeId NewRole { get; }

    /// <summary>
    /// Escape scenario of the player.
    /// </summary>
    public EscapeScenarioType EscapeScenarioType { get; }

    /// <summary>
    /// The bounds of the escape zone that was triggered.
    /// </summary>
    public Bounds EscapeZone { get; }
}