using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using UnityEngine;
using static Escape;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Escaping"/> event.
/// </summary>
public class PlayerEscapingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEscapingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is escaping.</param>
    /// <param name="newRole">The new role that is set after escape.</param>
    /// <param name="escapeScenario">The scenario of the escape.</param>
    /// <param name="oldRole">The old role of the player.</param>
    /// <param name="escapeZone">The bounds of the escape zone that was triggered.</param>
    public PlayerEscapingEventArgs(ReferenceHub hub, RoleTypeId oldRole, RoleTypeId newRole, EscapeScenarioType escapeScenario, Bounds escapeZone)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        OldRole = oldRole;
        NewRole = newRole;
        EscapeScenario = escapeScenario;
        EscapeZone = escapeZone;
    }

    /// <summary>
    /// Gets the player who is escaping.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old role of the player.
    /// </summary>
    public RoleTypeId OldRole { get; }

    /// <summary>
    /// Gets or sets the new role that is set after escape.
    /// </summary>
    public RoleTypeId NewRole { get; set; }

    /// <summary>
    /// Gets or sets the escape scenario.
    /// </summary>
    public EscapeScenarioType EscapeScenario { get; set; }

    /// <summary>
    /// The bounds of the escape zone that was triggered.
    /// </summary>
    public Bounds EscapeZone { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}