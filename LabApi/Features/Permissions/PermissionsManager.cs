using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabApi.Features.Permissions;

/// <summary>
/// Handles the registration and management of permissions and permission providers.
/// </summary>
public static class PermissionsManager
{
    private const string LoggerPrefix = "[PERMISSIONS]";

    /// <summary>
    /// Internal dictionary to store the registered permission providers.
    /// </summary>
    private static readonly Dictionary<Type, IPermissionsProvider> PermissionProviders = [];

    /// <summary>
    /// Registers the given <see cref="IPermissionsProvider"/>.
    /// </summary>
    /// <typeparam name="T">The type of the permission provider to register.</typeparam>
    public static void RegisterProvider<T>()
        where T : IPermissionsProvider, new()
    {
        if (PermissionProviders.ContainsKey(typeof(T)))
        {
            Logger.Warn($"{LoggerPrefix} The permission provider of type {typeof(T).FullName} is already registered.");
            return;
        }

        if (Activator.CreateInstance<T>() is not IPermissionsProvider provider)
        {
            Logger.Error($"{LoggerPrefix} Failed to create an instance of the permission provider of type {typeof(T).FullName}.");
            return;
        }

        PermissionProviders.Add(typeof(T), provider);
    }

    /// <summary>
    /// Unregisters the given <see cref="IPermissionsProvider"/>.
    /// </summary>
    /// <typeparam name="T">The type of the permission provider to unregister.</typeparam>
    public static void UnregisterProvider<T>()
        where T : IPermissionsProvider, new()
    {
        if (PermissionProviders.Remove(typeof(T)))
        {
            return;
        }

        Logger.Warn($"{LoggerPrefix} Failed to unregister the permission provider of type {typeof(T).FullName}. It is not registered.");
    }

    /// <summary>
    /// Retrieves the registered <see cref="IPermissionsProvider"/> of the given type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the permission provider to retrieve.</typeparam>
    /// <returns>The registered <see cref="IPermissionsProvider"/> of the given type <typeparamref name="T"/>; otherwise, null.</returns>
    public static IPermissionsProvider? GetProvider<T>()
        where T : IPermissionsProvider, new()
    {
        if (PermissionProviders.TryGetValue(typeof(T), out IPermissionsProvider provider))
        {
            return provider;
        }

        Logger.Warn($"{LoggerPrefix} The permission provider of type {typeof(T).FullName} is not registered.");
        return null;
    }

    /// <summary>
    /// Gets all the permissions of the given <paramref name="player"/> by each registered <see cref="IPermissionsProvider"/>.
    /// </summary>
    /// <param name="player">The player to retrieve the permissions for.</param>
    /// <returns>A dictionary of all the permissions of the given <paramref name="player"/> by each registered <see cref="IPermissionsProvider"/>.</returns>
    public static Dictionary<Type, string[]> GetPermissionsByProvider(this Player player)
        => PermissionProviders.ToDictionary(x => x.Key, x => x.Value.GetPermissions(player));

    /// <inheritdoc cref="IPermissionsProvider.GetPermissions"/>
    public static string[] GetPermissions(this Player player)
        => PermissionProviders.Values.SelectMany(x => x.GetPermissions(player)).ToArray();

    /// <inheritdoc cref="IPermissionsProvider.HasPermissions"/>
    public static bool HasPermissions(this Player player, params string[] permissions)
        => PermissionProviders.Values.Any(x => x.HasPermissions(player, permissions));

    /// <inheritdoc cref="IPermissionsProvider.HasAnyPermission"/>
    public static bool HasAnyPermission(this Player player, params string[] permissions)
        => PermissionProviders.Values.Any(x => x.HasAnyPermission(player, permissions));

    /// <inheritdoc cref="IPermissionsProvider.AddPermissions"/>
    public static void AddPermissions(this Player player, params string[] permissions)
    {
        foreach (IPermissionsProvider provider in PermissionProviders.Values)
        {
            provider.AddPermissions(player, permissions);
        }
    }

    /// <inheritdoc cref="IPermissionsProvider.RemovePermissions"/>
    public static void RemovePermissions(this Player player, params string[] permissions)
    {
        foreach (IPermissionsProvider provider in PermissionProviders.Values)
        {
            provider.RemovePermissions(player, permissions);
        }
    }

    /// <summary>
    /// Reloads all the registered <see cref="IPermissionsProvider"/>s.
    /// </summary>
    public static void ReloadAllPermissionsProviders()
    {
        foreach (IPermissionsProvider provider in PermissionProviders.Values)
        {
            provider.ReloadPermissions();
        }
    }
}