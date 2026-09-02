using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace LabApi.Features.Permissions.Providers;

/// <summary>
/// Represents a group of permissions. They are linked to RA user groups.
/// </summary>
public class PermissionGroup
{
    /// <summary>
    /// Gets the default permission group.
    /// </summary>
    public static PermissionGroup Default => new([], []) { IsRuntime = false };

    /// <summary>
    /// Generates the default permission groups based on the available groups in the RA settings.
    /// </summary>
    public static Dictionary<string, PermissionGroup> DefaultPermissionGroups
    {
        get
        {
            Dictionary<string, PermissionGroup> groups = new()
            {
                ["default"] = Default,
            };

            return groups;
        }
    }

    /// <summary>
    /// Constructor for deserialization.
    /// </summary>
    /// <remarks>
    /// All objects created by this constructor will have their <see cref="IsRuntime"/> property set to false.
    /// </remarks>
    public PermissionGroup()
        : this([], [], false)
    {
    }

    /// <summary>
    /// Represents a group of permissions. They are linked to RA user groups.
    /// </summary>
    /// <param name="inheritedGroups">Array of groups that should be inherited.</param>
    /// <param name="permissions">Array of permissions this group should have.</param>
    /// <param name="isRuntime">Bool indicating whether the <see cref="PermissionGroup"/> should skip being saved to the permission file config. See: <seealso cref="IsRuntime"/>.</param>
    public PermissionGroup(string[] inheritedGroups, string[] permissions, bool isRuntime = true)
    {
        InheritedGroups = inheritedGroups;
        Permissions = permissions;
        IsRuntime = isRuntime;
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
    /// A bool indicating whether the permission was created at runtime and should not be saved.
    /// Will not be saved if set to true.
    /// </summary>
    [YamlIgnore]
    public bool IsRuntime { get; internal set; }

    /// <summary>
    /// An internal dictionary that saves special permissions. (x.*).
    /// </summary>
    [YamlIgnore]
    internal HashSet<string> SpecialPermissionsSuperset { get; } = [];
}
