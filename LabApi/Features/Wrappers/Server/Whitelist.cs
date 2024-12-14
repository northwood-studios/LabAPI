using LabApi.Loader.Features.Paths;
using NorthwoodLib.Pools;
using System.Collections.Generic;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A static wrapper representing the <see cref="WhiteList">whitelist</see> of the server.
/// </summary>
public static class Whitelist
{
    /// <summary>
    /// Absolute path to UserIDWhitelist file.
    /// </summary>
    public static string FilePath => PathManager.WhitelistConfigPath;

    /// <summary>
    /// All currently whitelisted players.
    /// </summary>
    public static IEnumerable<string> WhitelistedPlayers => WhiteList.Users;

    /// <summary>
    /// Amount of whitelisted players.
    /// </summary>
    public static int Count => WhiteList.Users.Count;

    /// <summary>
    /// Gets or sets whether the whitelist is currently enabled. This value is reset to the config one after server restart.
    /// </summary>
    public static bool WhitelistEnabled
    {
        get => WhiteList.WhitelistEnabled;
        set => WhiteList.WhitelistEnabled = value;
    }

    /// <summary>
    /// Checks if player is on whitelist.
    /// </summary>
    /// <param name="userId">The user id of the player.</param>
    /// <returns>Whether player is on whitelist.</returns>
    public static bool IsOnWhitelist(string userId) => WhiteList.IsOnWhitelist(userId);

    /// <summary>
    /// Chhecks if the player is allowed on the server if the whitelist is enabled.
    /// </summary>
    /// <param name="userId">The user id of the player.</param>
    /// <returns>Whether the player is whitelisted. Will always return true if whitelist is disabled.</returns>
    public static bool IsWhitelisted(string userId) => WhiteList.IsWhitelisted(userId);

    /// <summary>
    /// Reloads whitelist from the whitelist file.
    /// </summary>
    public static void Reload() => WhiteList.Reload();

    /// <summary>
    /// Adds player to whitelist and saves it to file.
    /// </summary>
    /// <param name="userId">The userid.</param>
    public static void Add(string userId)
    {
        if (IsOnWhitelist(userId))
            return;

        List<string> lines = FileManager.ReadAllLinesList(FilePath);
        lines.Add(userId);
        FileManager.WriteToFile(lines, FilePath, true);
        WhiteList.Users.Add(userId);
    }

    /// <summary>
    /// Adds player to whitelist and saves it to file.
    /// </summary>
    /// <param name="player">The player to be added onto whitelist.</param>
    public static void Add(Player player) => Add(player.UserId);

    /// <summary>
    /// Removes player from whitelist and saves it to file.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>Whether the user id was found and removed from the whitelist.</returns>
    public static bool Remove(string userId)
    {
        if (!IsOnWhitelist(userId))
            return false;

        List<string> lines = FileManager.ReadAllLinesList(FilePath);

        int result = lines.RemoveAll(n => string.IsNullOrEmpty(n) || n == userId);
        WhiteList.Users.Remove(userId);
        FileManager.WriteToFile(lines, FilePath, true);

        ListPool<string>.Shared.Return(lines);
        return result > 0;
    }

    /// <summary>
    ///  Removes player from whitelist and saves it to file.
    /// </summary>
    /// <param name="player">The player to be removed from the whitelist.</param>
    /// <returns>Whether the player was found and removed from the whitelist.</returns>
    public static bool Remove(Player player) => Remove(player.UserId);
}
