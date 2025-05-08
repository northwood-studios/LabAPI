using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using NorthwoodLib.Pools;
using Serialization;

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
    private Dictionary<string, PermissionGroup> _permissionsDictionary = new();

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

    /// <inheritdoc cref="IPermissionsProvider.AddPermissions"/>
    public void AddPermissions(Player player, params string[] permissions)
    {
        PermissionGroup group = GetPlayerGroup(player);
        group.Permissions = group.Permissions.Concat(permissions).ToArray();
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

    private PermissionGroup GetPlayerGroup(Player player) => _permissionsDictionary.GetValueOrDefault(player.PermissionsGroupName ?? "default") ?? PermissionGroup.Default;

    private string[] GetPermissions(PermissionGroup group)
    {
        List<string> permissions = ListPool<string>.Shared.Rent();

        permissions.AddRange(group.Permissions);
        permissions.AddRange(group.SpecialPermissionsSuperset);

        foreach (string inheritedGroup in group.InheritedGroups)
        {
            if (!_permissionsDictionary.TryGetValue(inheritedGroup, out PermissionGroup inherited))
                continue;

            permissions.AddRange(GetPermissions(inherited));
        }

        return permissions.ToArray();
    }

    private bool HasPermission(PermissionGroup group, string permission)
    {
        if (group.IsRoot)
            return true;
    
        // We do first check if the group has the permission.
        if (group.Permissions.Contains(permission))
            return true;

        if (permission.Contains("."))
        {
            int index = permission.LastIndexOf(".", StringComparison.Ordinal);
            string perm = permission[..index];

            if (group.SpecialPermissionsSuperset.Contains(perm + ".*"))
                return true;
        }

        // Then we check if the group has the permission from the inherited groups.
        foreach (string inheritedGroup in group.InheritedGroups)
        {
            if (!_permissionsDictionary.TryGetValue(inheritedGroup, out PermissionGroup inherited))
                continue;

            if (HasPermission(inherited, permission))
                return true;
        }

        return false;
    }

    private void LoadDefaultPermissions()
    {
        _permissionsDictionary = PermissionGroup.DefaultPermissionGroups;
        ReloadPermissions();
    }

    void IPermissionsProvider.ReloadPermissions() => ReloadPermissions();

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
                    continue;

                int index = permission.LastIndexOf(".", StringComparison.Ordinal);
                string perm = permission[..index];

                permissionsGroup.SpecialPermissionsSuperset.Add(perm + ".*");
            }
        }
    }

    private void SavePermissions() => File.WriteAllText(_permissions.FullName, YamlParser.Serializer.Serialize(_permissionsDictionary));
}
