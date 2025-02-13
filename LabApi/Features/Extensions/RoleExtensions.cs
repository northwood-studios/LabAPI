using PlayerRoles;
using UnityEngine;

namespace LabApi.Features.Extensions;

public static class RoleExtensions
{
    /// <summary>
    /// Gets the <see cref="PlayerRoleBase"/> from a <see cref="RoleTypeId"/>.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>The <see cref="PlayerRoleBase"/>.</returns>
    public static PlayerRoleBase GetRoleBase(this RoleTypeId roleType) => PlayerRoleLoader.TryGetRoleTemplate(roleType, out PlayerRoleBase role) ? role : null!;
    
    /// <summary>
    /// Gets the <see cref="Team"/> that a <see cref="RoleTypeId"/> belongs to.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>The <see cref="Team"/>.</returns>
    public static Team GetTeam(this RoleTypeId roleType) => PlayerRoleLoader.AllRoles.TryGetValue(roleType, out PlayerRoleBase prb) ? prb.Team : Team.OtherAlive;
    
    /// <summary>
    /// Gets the human-readable version of a <see cref="RoleTypeId"/>'s name.
    /// </summary>
    /// <param name="roleType">The <see cref="RoleTypeId"/>.</param>
    /// <returns>The name of the role.</returns>
    public static string GetFullName(this RoleTypeId roleType) => roleType.GetRoleBase().RoleName;
    
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
    public static bool IsCivilian(this RoleTypeId roleType) => roleType == RoleTypeId.ClassD || roleType == RoleTypeId.Scientist;
}