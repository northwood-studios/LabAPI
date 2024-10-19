using GameCore;
using NorthwoodLib.Pools;
using System.Collections.Generic;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A static wrapper representing the <see cref="ReservedSlots">reserved slots</see> of the server.
/// </summary>
public static class ReservedSlots
{
    /// <summary>
    /// Absolute path to UserIDReservedSlots file.
    /// </summary>
    public static string FilePath => ConfigSharing.Paths[3] + "UserIDReservedSlots.txt";

    /// <summary>
    /// All currently players with reserved slots.
    /// </summary>
    public static IEnumerable<string> WhitelistedPlayers => ReservedSlot.Users;

    /// <summary>
    /// Amount of players with reserved slots.
    /// </summary>
    public static int Count => ReservedSlot.Users.Count;

    /// <summary>
    /// Checks if player has a reserved slot on the server.
    /// </summary>
    /// <param name="userId">The user id of the player.</param>
    /// <returns>Whether player has a reserved slot.</returns>
    public static bool HasReservedSlot(string userId) => ReservedSlot.HasReservedSlot(userId);

    /// <summary>
    /// Reloads reserved slots from the file.
    /// </summary>
    public static void Reload() => ReservedSlot.Reload();

    /// <summary>
    /// Gives the player a reserved slot and saves it to file.
    /// </summary>
    /// <param name="userId">The userid.</param>
    public static void Add(string userId)
    {
        if (HasReservedSlot(userId))
            return;

        List<string> lines = FileManager.ReadAllLinesList(FilePath);
        lines.Add(userId);
        FileManager.WriteToFile(lines, FilePath, true);
        ReservedSlot.Users.Add(userId);
    }

    /// <summary>
    /// Gives the player a reserved slot and saves it to file.
    /// </summary>
    /// <param name="player">The target player.</param>
    public static void Add(Player player) => Add(player.UserId);

    /// <summary>
    /// Removes the player's reserved slot and saves it to file.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>Whether the user id was found and removed from reserved slots.</returns>
    public static bool Remove(string userId)
    {
        if (!HasReservedSlot(userId))
            return false;

        List<string> lines = FileManager.ReadAllLinesList(FilePath);

        int result = lines.RemoveAll(n => string.IsNullOrEmpty(n) || n == userId);
        ReservedSlot.Users.Remove(userId);
        FileManager.WriteToFile(lines, FilePath, true);

        ListPool<string>.Shared.Return(lines);
        return result > 0;
    }

    /// <summary>
    ///  Removes player from whitelist and saves it to file.
    /// </summary>
    /// <param name="player">The target player.</param>
    /// <returns>Whether the player was found and removed from the reserved slots.</returns>
    public static bool Remove(Player player) => Remove(player.UserId);
}

