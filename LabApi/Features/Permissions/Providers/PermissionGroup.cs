using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace LabApi.Features.Permissions.Providers;

/// <summary>
/// Represents a group of permissions. They are linked to RA user groups.
/// </summary>
public class PermissionGroup(string[] inheritedGroups, string[] permissions)
{
    /// <summary>
    /// Gets the default permission group.
    /// </summary>
    public static PermissionGroup Default => new(Array.Empty<string>(), Array.Empty<string>());

    /// <summary>
    /// Generates the default permission groups based on the available groups in the RA settings.
    /// </summary>
    public static Dictionary<string, PermissionGroup> DefaultPermissionGroups
    {
        get
        {
            Dictionary<string, PermissionGroup> groups = new()
            {
                ["default"] = Default
            };

            // We fill the permissions with the available groups, because it is cool, and we can ;).
            foreach (string raGroup in ServerStatic.GetPermissionsHandler().GetAllGroupsNames())
            {
                groups[raGroup] = new PermissionGroup(["default"], Array.Empty<string>());
            }

            return groups;
        }
    }

    /// <summary>
    /// The inherited groups of the group.
    /// </summary>
    public string[] InheritedGroups { get; set; } = inheritedGroups;

    /// <summary>
    /// The permissions of the group.
    /// </summary>
    public string[] Permissions { get; set; } = permissions;

    /// <summary>
    /// An internal dictionary that saves special permissions. (x.*).
    /// </summary>
    [YamlIgnore]
    internal HashSet<string> SpecialPermissionsSuperset { get; } = [];
}