using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangingRole"/> event.
/// </summary>
public class PlayerChangingRoleEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangingRoleEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose role is changing.</param>
    /// <param name="oldRole">The old role object of the player.</param>
    /// <param name="newRole">The new role type.</param>
    /// <param name="changeReason">The reason of role changing.</param>
    public PlayerChangingRoleEventArgs(ReferenceHub player, PlayerRoleBase oldRole, RoleTypeId newRole, RoleChangeReason changeReason)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        OldRole = oldRole;
        NewRole = newRole;
        ChangeReason = changeReason;
    }

    /// <summary>
    /// Gets the player whose role is changing.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the old role object of the player.
    /// </summary>
    public PlayerRoleBase OldRole { get; }

    /// <summary>
    /// Gets the new role type.
    /// </summary>
    public RoleTypeId NewRole { get; set; }

    /// <summary>
    /// Gets the reason of role change.
    /// </summary>
    public RoleChangeReason ChangeReason { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}