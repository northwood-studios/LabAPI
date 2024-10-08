using LabApi.Features.Wrappers;

namespace LabApi.Features.Permissions;

/// <summary>
/// Represents a provider of user permissions.
/// </summary>
public interface IPermissionsProvider
{
    /// <summary>
    /// Retrieves all the permissions of the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to retrieve the permissions for.</param>
    /// <returns>An array of all the permissions of the given <paramref name="player"/>.</returns>
    public string[] GetPermissions(Player player);

    /// <summary>
    /// Whether the given <paramref name="player"/> has all the given <paramref name="permissions"/>.
    /// </summary>
    /// <param name="player">The player to check the permissions for.</param>
    /// <param name="permissions">The permissions to check.</param>
    /// <returns>True if the <paramref name="player"/> has all the <paramref name="permissions"/>; otherwise, false.</returns>
    public bool HasPermissions(Player player, params string[] permissions);

    /// <summary>
    /// Whether the given <paramref name="player"/> has any of the given <paramref name="permissions"/>.
    /// </summary>
    /// <param name="player">The player to check the permissions for.</param>
    /// <param name="permissions">The permissions to check.</param>
    /// <returns>True if the <paramref name="player"/> has any of the <paramref name="permissions"/>; otherwise, false.</returns>
    public bool HasAnyPermission(Player player, params string[] permissions);

    /// <summary>
    /// Adds all the given <paramref name="permissions"/> to the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to add the permissions to.</param>
    /// <param name="permissions">The permissions to add.</param>
    public void AddPermissions(Player player, params string[] permissions);

    /// <summary>
    /// Removes all the given <paramref name="permissions"/> from the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to remove the permissions from.</param>
    /// <param name="permissions">The permissions to remove.</param>
    public void RemovePermissions(Player player, params string[] permissions);
}