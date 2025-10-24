using System.Collections.Generic;
using PlayerRoles;
using System.Diagnostics.CodeAnalysis;
using InventorySystem;
using InventorySystem.Configs;
using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace LabApi.Features.Extensions;

/// <summary>
/// Adds extension methods to access information about <see cref="RoleTypeId"/>s.
/// </summary>
public static class RoleExtensions
{
    private static readonly InventoryRoleInfo EmptyInventoryInfo = new([], []); // prevent creating this multiple time

    /// <summary>
    /// Gets the <see cref="PlayerRoleBase"/> from a <see cref="RoleTypeId"/>.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>The <see cref="PlayerRoleBase"/>.</returns>
    public static PlayerRoleBase GetRoleBase(this RoleTypeId roleType) => PlayerRoleLoader.TryGetRoleTemplate(roleType, out PlayerRoleBase role) ? role : null!;

    /// <summary>
    /// Tries to get a role base from a <see cref="RoleTypeId"/>.
    /// </summary>
    /// <param name="roleTypeId">The <see cref="RoleTypeId"/> to get base of.</param>
    /// <param name="role">The <see cref="PlayerRoleBase"/> found.</param>
    /// <typeparam name="T">The <see cref="PlayerRoleBase"/>.</typeparam>
    /// <returns>The role base found, else null.</returns>
    public static bool TryGetRoleBase<T>(this RoleTypeId roleTypeId, [NotNullWhen(true)] out T? role) => PlayerRoleLoader.TryGetRoleTemplate(roleTypeId, out role);

    /// <summary>
    /// Gets the human-readable version of a <see cref="RoleTypeId"/>'s name.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>The name of the role.</returns>
    public static string GetFullName(this RoleTypeId roleType) => roleType.GetRoleBase().RoleName;

    /// <summary>
    /// Tries to get a random spawn point from a <see cref="RoleTypeId"/>.
    /// </summary>
    /// <param name="role">The role to get spawn from.</param>
    /// <param name="position">The position found.</param>
    /// <param name="horizontalRotation">The rotation found.</param>
    /// <returns>Whether a SpawnPoint was found.</returns>
    public static bool TryGetRandomSpawnPoint(this RoleTypeId role, out Vector3 position, out float horizontalRotation)
    {
        if (TryGetRoleBase(role, out IFpcRole? fpcRole))
        {
            return fpcRole.SpawnpointHandler.TryGetSpawnpoint(out position, out horizontalRotation);
        }

        position = Vector3.zero;
        horizontalRotation = 0f;
        return false;
    }

    /// <summary>
    /// Gets the inventory of the specified <see cref="RoleTypeId"/>.
    /// </summary>
    /// <param name="roleTypeId">The <see cref="RoleTypeId"/> to get inventory of.</param>
    /// <returns>The <see cref="InventoryRoleInfo"/> found.</returns>
    public static InventoryRoleInfo GetInventory(this RoleTypeId roleTypeId)
        => StartingInventories.DefinedInventories.GetValueOrDefault(roleTypeId, EmptyInventoryInfo);

    /// <summary>
    /// Checks if the role is an SCP role.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>A boolean which is true when the role is an SCP role.</returns>
    public static bool IsScp(this RoleTypeId roleType) => roleType.GetTeam() == Team.SCPs;

    /// <summary>
    /// Checks if the role is a dead role.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>A boolean which is true when the role is a dead role.</returns>
    public static bool IsDead(this RoleTypeId roleType) => roleType.GetTeam() == Team.Dead;

    /// <summary>
    /// Checks if the role is an NTF role.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>A boolean which is true when the role is an NTF role. Does not include Facility Guards.</returns>
    public static bool IsNtf(this RoleTypeId roleType) => roleType.GetTeam() == Team.FoundationForces && roleType != RoleTypeId.FacilityGuard;

    /// <summary>
    /// Checks if the role is a Chaos role.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>A boolean which is true when the role is a Chaos role.</returns>
    public static bool IsChaos(this RoleTypeId roleType) => roleType.GetTeam() == Team.ChaosInsurgency;

    /// <summary>
    /// Checks if the role is a military role (Chaos Insurgency or NTF).
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>A boolean which is true when the role is a military role.</returns>
    public static bool IsMilitary(this RoleTypeId roleType) => roleType.IsNtf() || roleType.IsChaos() || roleType == RoleTypeId.FacilityGuard;

    /// <summary>
    /// Checks if the role is a civilian role (Scientists and Class-D).
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>A boolean which is true when the role is a civilian role.</returns>
    public static bool IsCivilian(this RoleTypeId roleType) => roleType is RoleTypeId.ClassD or RoleTypeId.Scientist;
}