using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using NorthwoodLib.Pools;
using Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace LabApi.Features.Permissions.Providers;

/// <summary>
/// Grants a default permission provider.
/// Server host friendly and easily configurable from the file system.
/// </summary>
public class DefaultPermissionsProvider : IPermissionsProvider
{
    private const string LoggerPrefix = "[PERMISSIONS_PROVIDER]";
    private const string PermissionsFileName = "permissions.yml";

    private readonly FileInfo _permissions;
    private Dictionary<string, PermissionGroup> _permissionsDictionary = [];

    /// <summary>
    /// Creates a new instance of the <see cref="DefaultPermissionsProvider"/> class.
    /// </summary>
    public DefaultPermissionsProvider()
    {
        _permissions = new FileInfo(Path.Combine(PathManager.Configs.FullName, PermissionsFileName));

        // We can create the default permissions file if it doesn't exist.
        if (!_permissions.Exists)
        {
            Logger.Warn($"{LoggerPrefix} Permissions file not found. Creating a new one.");

            // We load the default permissions in the dictionary.
            LoadDefaultPermissions();

            // And save them to the file.
            SavePermissions();
            return;
        }

        try
        {
            // We deserialize the permissions from the file.
            _permissionsDictionary = YamlParser.Deserializer.Deserialize<Dictionary<string, PermissionGroup>>(File.ReadAllText(_permissions.FullName));

            // We then reload the permissions to fill the special permissions.
            ReloadPermissions();

            // And finally, we save the permissions to the file to ensure the permissions are up to date.
            SavePermissions();
        }
        catch (Exception e)
        {
            Logger.Error($"{LoggerPrefix} Failed to load permissions from file {_permissions.FullName}.");
            Logger.Error(e);
            throw;
        }
    }

    /// <inheritdoc cref="IPermissionsProvider.GetPermissions"/>
    public string[] GetPermissions(Player player)
    {
        PermissionGroup group = GetPlayerGroup(player);
        return GetPermissions(group);
    }

    /// <inheritdoc cref="IPermissionsProvider.HasPermissions"/>
    public bool HasPermissions(Player player, params string[] permissions)
    {
        PermissionGroup group = GetPlayerGroup(player);
        return permissions.All(permission => HasPermission(group, permission));
    }

    /// <inheritdoc cref="IPermissionsProvider.HasAnyPermission"/>
    public bool HasAnyPermission(Player player, params string[] permissions)
    {
        PermissionGroup group = GetPlayerGroup(player);
        return permissions.Any(permission => HasPermission(group, permission));
    }

    /// <inheritdoc cref="IPermissionsProvider.HasPermission" />
    public bool HasPermission(Player player, string specificPermission)
    {
        PermissionGroup group = GetPlayerGroup(player);
        return HasPermission(group, specificPermission);
    }

    /// <inheritdoc cref="IPermissionsProvider.AddPermissions"/>
    public void AddPermissions(Player player, params string[] permissions)
    {
        PermissionGroup group = GetPlayerGroup(player);
        group.Permissions = [.. group.Permissions, .. permissions];
        ReloadPermissions();
        SavePermissions();
    }

    /// <inheritdoc cref="IPermissionsProvider.RemovePermissions"/>
    public void RemovePermissions(Player player, params string[] permissions)
    {
        PermissionGroup group = GetPlayerGroup(player);
        group.Permissions = group.Permissions.Except(permissions).ToArray();
        ReloadPermissions();
        SavePermissions();
    }

    /// <inheritdoc />
    void IPermissionsProvider.ReloadPermissions() => ReloadPermissions();

    /// <summary>
    /// Gets the <see cref="PermissionGroup"/> the player is a part of.
    /// </summary>
    /// <param name="player">The player whose <see cref="PermissionGroup"/> to find.</param>
    /// <returns>The <see cref="PermissionGroup"/> the player is a part of, otherwise <see cref="PermissionGroup.Default"/>.</returns>
    public PermissionGroup GetPlayerGroup(Player player) => GetPermissionGroup(player.PermissionsGroupName ?? "default");

    /// <summary>
    /// Gets the <see cref="PermissionGroup"/> from a <see cref="UserGroup.Name"/>.
    /// </summary>
    /// <param name="groupName">A <see cref="UserGroup.Name"/> to find the <see cref="PermissionGroup"/> of.</param>
    /// <returns>A <see cref="PermissionGroup"/> if one is defined, <see cref="PermissionGroup.Default"/> otherwise.</returns>
    public PermissionGroup GetPermissionGroup(string groupName) => _permissionsDictionary.GetValueOrDefault(groupName) ?? PermissionGroup.Default;

    /// <summary>
    /// Tries to get the <see cref="PermissionGroup"/> from a <see cref="UserGroup.Name"/>.
    /// </summary>
    /// <param name="groupName">A <see cref="UserGroup.Name"/> to find the <see cref="PermissionGroup"/> of.</param>
    /// <param name="permissionGroup">The found <see cref="PermissionGroup"/> when true, null otherwise.</param>
    /// <returns>Whether a <see cref="PermissionGroup"/> with the registry name of <paramref name="groupName"/> was found.</returns>
    public bool TryGetPermissionGroup(string groupName, [NotNullWhen(true)] out PermissionGroup? permissionGroup) => _permissionsDictionary.TryGetValue(groupName, out permissionGroup);

    /// <summary>
    /// Gets the <see cref="string"/> array of permissions a <see cref="PermissionGroup"/> grants.
    /// </summary>
    /// <param name="group">The <see cref="PermissionGroup"/>, permissions of which will be returned.</param>
    /// <returns>A <see cref="string"/> array of permissions this group grants.</returns>
    public string[] GetPermissions(PermissionGroup group)
    {
        List<string> permissions = ListPool<string>.Shared.Rent();

        permissions.AddRange(group.Permissions);
        permissions.AddRange(group.SpecialPermissionsSuperset);

        foreach (string inheritedGroup in group.InheritedGroups)
        {
            if (!_permissionsDictionary.TryGetValue(inheritedGroup, out PermissionGroup inherited))
            {
                continue;
            }

            permissions.AddRange(GetPermissions(inherited));
        }

        return [.. permissions];
    }

    /// <summary>
    /// Adds a new permission group.
    /// </summary>
    /// <param name="groupName">A <see cref="UserGroup.Name"/> the <paramref name="group"/> is linked to.</param>
    /// <param name="group">The group to add.</param>
    /// <returns>Whether the group was successfully added. False if group with this name is already registered or if the <paramref name="group"/>'s <see cref="PermissionGroup.IsRuntime"/> is set to false.</returns>
    public bool AddPermissionGroup(string groupName, PermissionGroup group)
        => group.IsRuntime && _permissionsDictionary.TryAdd(groupName, group);

    /// <summary>
    /// Removes a permission group if it exists.
    /// </summary>
    /// <param name="groupName">A <see cref="UserGroup.Name"/> which links to a <see cref="PermissionGroup"/> that is to be removed.</param>
    /// <param name="group">The group which was removed or null.</param>
    /// <returns>Whether the group was found and removed successfully. False if group with this name could not be found or if the <paramref name="group"/>'s <see cref="PermissionGroup.IsRuntime"/> is set to false.</returns>
    public bool RemovePermissionGroup(string groupName, [NotNullWhen(true)] out PermissionGroup? group)
    {
        if (!_permissionsDictionary.TryGetValue(groupName, out group))
        {
            return false;
        }

        if (!group.IsRuntime)
        {
            return false;
        }

        return _permissionsDictionary.Remove(groupName, out group);
    }

    private bool HasPermission(PermissionGroup group, string permission)
    {
        if (group.IsRoot)
        {
            return true;
        }

        // We do first check if the group has the permission.
        if (group.Permissions.Contains(permission))
        {
            return true;
        }

        if (permission.Contains("."))
        {
            int index = permission.LastIndexOf(".", StringComparison.Ordinal);
            string perm = permission[..index];

            if (group.SpecialPermissionsSuperset.Contains(perm + ".*"))
            {
                return true;
            }
        }

        // Then we check if the group has the permission from the inherited groups.
        foreach (string inheritedGroup in group.InheritedGroups)
        {
            if (!_permissionsDictionary.TryGetValue(inheritedGroup, out PermissionGroup inherited))
            {
                continue;
            }

            if (HasPermission(inherited, permission))
            {
                return true;
            }
        }

        return false;
    }

    private void LoadDefaultPermissions()
    {
        _permissionsDictionary = PermissionGroup.DefaultPermissionGroups;
        ReloadPermissions();
    }

    private void ReloadPermissions()
    {
        // We clear the special permissions and fill them again.
        foreach (PermissionGroup permissionsGroup in _permissionsDictionary.Values)
        {
            permissionsGroup.SpecialPermissionsSuperset.Clear();
            foreach (string permission in permissionsGroup.Permissions)
            {
                if (permission == ".*")
                {
                    permissionsGroup.IsRoot = true;

                    // We don't have to continue.
                    break;
                }

                if (!permission.Contains(".*"))
                {
                    continue;
                }

                int index = permission.LastIndexOf(".", StringComparison.Ordinal);
                string perm = permission[..index];

                permissionsGroup.SpecialPermissionsSuperset.Add(perm + ".*");
            }
        }
    }

    private void SavePermissions() => File.WriteAllText(_permissions.FullName, YamlParser.Serializer.Serialize(_permissionsDictionary.Where(kvp => !kvp.Value.IsRuntime).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
}
