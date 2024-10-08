using System;
using System.Collections.Generic;
using CommandSystem;
using LabApi.Features.Wrappers;

namespace LabApi.Features.Permissions;

/// <summary>
/// Represents extension methods for the permissions system.
/// </summary>
public static class PermissionsExtensions
{
    /// <inheritdoc cref="PermissionsManager.GetPermissionsByProvider"/>
    public static Dictionary<Type, string[]> GetPermissionsByProvider(this ICommandSender sender) =>
        Player.Get(sender)?.GetPermissionsByProvider() ?? new Dictionary<Type, string[]>();

    /// <inheritdoc cref="IPermissionsProvider.GetPermissions"/>
    public static string[] GetPermissions(this ICommandSender sender) =>
        Player.Get(sender)?.GetPermissions() ?? Array.Empty<string>();

    /// <inheritdoc cref="IPermissionsProvider.HasPermissions"/>
    public static bool HasPermissions(this ICommandSender sender, params string[] permissions) =>
        Player.Get(sender)?.HasPermissions(permissions) ?? false;

    /// <inheritdoc cref="IPermissionsProvider.HasAnyPermission"/>
    public static bool HasAnyPermission(this ICommandSender sender, params string[] permissions) =>
        Player.Get(sender)?.HasAnyPermission(permissions) ?? false;

    /// <inheritdoc cref="IPermissionsProvider.AddPermissions"/>
    public static void AddPermissions(this ICommandSender sender, params string[] permissions) =>
        Player.Get(sender)?.AddPermissions(permissions);

    /// <inheritdoc cref="IPermissionsProvider.RemovePermissions"/>
    public static void RemovePermissions(this ICommandSender sender, params string[] permissions) =>
        Player.Get(sender)?.RemovePermissions(permissions);
}