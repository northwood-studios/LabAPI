using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace LabApi.Features.Permissions.Providers;

/// <summary>
/// Represents a group of permissions. They are linked to RA user groups.
/// </summary>
public class PermissionGroup
{
    /// <summary>
    /// Constructor for deserialization.
    /// </summary>
    public PermissionGroup() : this([], []) {}

    /// <summary>
    /// Represents a group of permissions. They are linked to RA user groups.
    /// </summary>
    public PermissionGroup(string[] inheritedGroups, string[] permissions)
    {
        InheritedGroups = inheritedGroups;
        Permissions = permissions;
    }

    /// <summary>
    /// Gets the default permission group.
    /// </summary>
    public static PermissionGroup Default => new([], []);

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

            return groups;
        }
    }

    /// <summary>
    /// The inherited groups of the group.
    /// </summary>
    public string[] InheritedGroups { get; set; }

    /// <summary>
    /// The permissions of the group.
    /// </summary>
    public string[] Permissions { get; set; }

    /// <summary>
    /// Whether the user has all access to all permissions (*).
    /// </summary>
    [YamlIgnore]
    public bool IsRoot { get; set; } = false;

    /// <summary>
    /// An internal dictionary that saves special permissions. (x.*).
    /// </summary>
    [YamlIgnore]
    internal HashSet<string> SpecialPermissionsSuperset { get; } = [];
}