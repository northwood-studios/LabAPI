using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangedRole"/> event.
/// </summary>
public class PlayerChangedRoleEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangedRoleEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose role changed.</param>
    /// <param name="oldRole">The old role type.</param>
    /// <param name="newRole">The new role object of the player.</param>
    /// <param name="changeReason">The reason of role changed.</param>
    public PlayerChangedRoleEventArgs(ReferenceHub player, RoleTypeId oldRole, PlayerRoleBase newRole, RoleChangeReason changeReason)
    {
        Player = Player.Get(player);
        OldRole = oldRole;
        NewRole = newRole;
        ChangeReason = changeReason;
    }

    /// <summary>
    /// Gets the player whose role changed.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old role type.
    /// </summary>
    public RoleTypeId OldRole { get; }

    /// <summary>
    /// Gets the new role object of the player.
    /// </summary>
    public PlayerRoleBase NewRole { get; }

    /// <summary>
    /// Gets the reason of role changed.
    /// </summary>
    public RoleChangeReason ChangeReason { get; }
}