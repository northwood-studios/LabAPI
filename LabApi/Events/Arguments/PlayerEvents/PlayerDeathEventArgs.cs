using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerStatsSystem;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Death"/> event.
/// </summary>
public class PlayerDeathEventArgs : EventArgs, IPlayerEvent, IDamageEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDeathEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who died.</param>
    /// <param name="attacker">The player who caused the death.</param>
    /// <param name="damageHandler">The damage that caused the death.</param>
    /// <param name="oldRole">The previous role of the player before death.</param>
    /// <param name="oldPosition">The previous world position of the player before death.</param>
    /// <param name="oldVelocity">The previous velocity of the player before death.</param>
    /// <param name="oldCameraRotation">The previous world rotation of the players camera before death.</param>
    public PlayerDeathEventArgs(ReferenceHub player, ReferenceHub? attacker, DamageHandlerBase damageHandler,
        RoleTypeId oldRole, Vector3 oldPosition, Vector3 oldVelocity, Quaternion oldCameraRotation)
    {
        Player = Player.Get(player);
        Attacker = Player.Get(attacker);
        DamageHandler = damageHandler;
        OldRole = oldRole;
        OldPosition = oldPosition;
        OldVelocity = oldVelocity;
        OldCameraRotation = oldCameraRotation;
    }

    /// <summary>
    /// Gets the player who died.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who caused the death.
    /// </summary>
    public Player? Attacker { get; }

    /// <summary>
    /// Gets the damage that caused the death.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }

    /// <summary>
    /// Gets the role of the <see cref="Player"/> before they had died.
    /// </summary>
    public RoleTypeId OldRole { get; }

    /// <summary>
    /// Gets the player's position before they died.
    /// </summary>
    public Vector3 OldPosition { get; }

    /// <summary>
    /// Gets the player's velocity before they died.
    /// </summary>
    public Vector3 OldVelocity { get; }

    /// <summary>
    /// Gets the player's camera rotation before they died.
    /// </summary>
    public Quaternion OldCameraRotation { get; }
}