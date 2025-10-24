using Achievements;
using CommandSystem;
using CustomPlayerEffects;
using InventorySystem.Configs;
using LabApi.Features.Permissions;
using LabApi.Features.Permissions.Providers;
using Mirror;
using NorthwoodLib.Pools;
using RemoteAdmin;
using RoundRestarting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static BanHandler;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the server.
/// </summary>
public static class Server
{
    /// <summary>
    /// The <see cref="Server"/> Instance.
    /// </summary>
    public static Player? Host { get; internal set; }

    /// <summary>
    /// Gets the IP address of the server.
    /// </summary>
    public static string IpAddress => ServerConsole.Ip;

    /// <summary>
    /// Gets the port of the server.
    /// </summary>
    public static ushort Port => ServerStatic.ServerPort;

    /// <summary>
    /// Gets the amount of online players.
    /// </summary>
    public static int PlayerCount => Player.Count;

    /// <summary>
    /// Gets or sets the maximum amount of players allowed online at the same time.
    /// </summary>
    public static int MaxPlayers
    {
        get => CustomNetworkManager.slots;
        set => CustomNetworkManager.slots = value;
    }

    /// <summary>
    /// Gets or sets the amount of reserved slots.
    /// </summary>
    public static int ReservedSlots
    {
        get => CustomNetworkManager.reservedSlots;
        set => CustomNetworkManager.reservedSlots = value;
    }

    /// <summary>
    /// Gets the <see cref="DefaultPermissionsProvider"/>.
    /// </summary>
    public static DefaultPermissionsProvider? DefaultPermissionProvider => (DefaultPermissionsProvider?)PermissionsManager.GetProvider<DefaultPermissionsProvider>();

    /// <summary>
    /// Gets the Ticks Per Second of the server.
    /// </summary>
    public static double Tps => Math.Round(1f / Time.smoothDeltaTime);

    /// <summary>
    /// Gets the max Ticks Per Second of the server.
    /// </summary>
    public static short MaxTps
    {
        get => ServerStatic.ServerTickrate;
        set => ServerStatic.ServerTickrate = value;
    }

    /// <summary>
    /// Gets or sets the spawn protection duration for players.
    /// </summary>
    public static float SpawnProtectDuration
    {
        get => SpawnProtected.SpawnDuration;
        set => SpawnProtected.SpawnDuration = value;
    }

    /// <summary>
    /// Gets whether the server is in Idle Mode.
    /// </summary>
    public static bool IdleModeActive => IdleMode.IdleModeActive;

    /// <summary>
    /// Gets or sets whether the server temporarily can't enter Idle Mode.
    /// </summary>
    public static bool PauseIdleMode
    {
        get => IdleMode.PauseIdleMode;
        set => IdleMode.PauseIdleMode = value;
    }

    /// <summary>
    /// Gets or sets whether Idle Mode is available on the server.
    /// </summary>
    public static bool IdleModeAvailable
    {
        get => IdleMode.IdleModeEnabled;
        set => IdleMode.IdleModeEnabled = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether friendly fire is enabled.
    /// </summary>
    public static bool FriendlyFire
    {
        get => ServerConsole.FriendlyFire;
        set
        {
            if (FriendlyFire == value)
            {
                return;
            }

            ServerConsole.FriendlyFire = value;
            ServerConfigSynchronizer.Singleton.RefreshMainBools();
            ServerConfigSynchronizer.OnRefreshed?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether achievement granting is enabled.
    /// </summary>
    public static bool AchievementsEnabled
    {
        get => !AchievementManager.AchievementsDisabled;
        set
        {
            if (AchievementManager.AchievementsDisabled != value)
            {
                return;
            }

            AchievementManager.AchievementsDisabled = !value;
            ServerConfigSynchronizer.Singleton.RefreshMainBools();
            ServerConfigSynchronizer.OnRefreshed?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the server name as seen on the server list.
    /// </summary>
    public static string ServerListName
    {
        get => ServerConsole.ServerName;
        set => ServerConsole.ServerName = value;
    }

    /// <summary>
    /// Gets or sets the server name as seen on the player list.
    /// </summary>
    // TODO: maybe move to a player list wrapper?
    public static string PlayerListName
    {
        get => PlayerList.Title.Value;
        set => PlayerList.Title.Value = value;
    }

    /// <summary>
    /// Gets or sets the refresh rate for the player list name.
    /// </summary>
    // TODO: maybe move to a player list wrapper?
    public static float PlayerListNameRefreshRate
    {
        get => PlayerList.RefreshRate.Value;
        set => PlayerList.RefreshRate.Value = value;
    }

    /// <summary>
    /// Gets or sets whether the server has been marked as transparently modded.<br/>
    /// For this status to be applied automatically, all installed plugins must have their
    /// <see cref="LabApi.Loader.Features.Plugins.Plugin.IsTransparent"/> property set to <see langword="true"/>.<br/>
    /// For more information, see article 5.2 in the official documentation: https://scpslgame.com/csg.
    /// </summary>
    public static bool IsTransparentlyModded { get; internal set; }

    /// <summary>
    /// Gets the <see cref="ItemCategory">Category</see> <see cref="ILimit{ItemCategory, SByte}">limits</see>.
    /// </summary>
    public static ILimit<ItemCategory, sbyte> CategoryLimits { get; } = new CategoryLimitsSynchronizer();

    /// <summary>
    /// Gets the <see cref="ItemType">Ammo</see> <see cref="ILimit{ItemType, UInt16}">limits</see>.
    /// </summary>
    public static ILimit<ItemType, ushort> AmmoLimits { get; } = new AmmoLimitsSynchronizer();

    /// <summary>
    /// Gets the <see cref="CommandSystem.RemoteAdminCommandHandler"/> instance.
    /// </summary>
    public static RemoteAdminCommandHandler RemoteAdminCommandHandler => CommandProcessor.RemoteAdminCommandHandler;

    /// <summary>
    /// Gets the <see cref="CommandSystem.ClientCommandHandler"/> instance.
    /// </summary>
    public static ClientCommandHandler ClientCommandHandler => QueryProcessor.DotCommandHandler;

    /// <summary>
    /// Gets the <see cref="CommandSystem.GameConsoleCommandHandler"/> instance.
    /// </summary>
    public static GameConsoleCommandHandler GameConsoleCommandHandler => GameCore.Console.ConsoleCommandHandler;

    /// <summary>
    /// Gets the <see cref="ServerShutdown.ServerShutdownState"/> of the server.
    /// </summary>
    public static ServerShutdown.ServerShutdownState ShutdownState => ServerShutdown.ShutdownState;

    #region Ban System

    /// <summary>
    /// Bans the specified <see cref="Player"/> from the server.
    /// </summary>
    /// <param name="player">The player to ban.</param>
    /// <param name="reason">The reason of the ban.</param>
    /// <param name="duration">The duration of the ban in seconds.</param>
    /// <returns>Whether the player was successfully banned.</returns>
    public static bool BanPlayer(Player player, string reason, long duration) =>
        global::BanPlayer.BanUser(player.ReferenceHub, reason, duration);

    /// <summary>
    /// Bans the specified <see cref="Player"/> from the server.
    /// </summary>
    /// <param name="player">The player to ban.</param>
    /// <param name="issuer">The player that issued the ban.</param>
    /// <param name="reason">The reason of the ban.</param>
    /// <param name="duration">The duration of ban in seconds.</param>
    /// <returns>Whether the player was successfully banned.</returns>
    public static bool BanPlayer(Player player, Player issuer, string reason, long duration) =>
        global::BanPlayer.BanUser(player.ReferenceHub, issuer.ReferenceHub, reason, duration);

    /// <summary>
    /// Kicks the specified <see cref="Player"/> from the server.
    /// </summary>
    /// <param name="player">The player to kick.</param>
    /// <param name="reason">The reason of the kick.</param>
    /// <returns>Whether the player was successfully kicked.</returns>
    public static bool KickPlayer(Player player, string reason) =>
        global::BanPlayer.KickUser(player.ReferenceHub, reason);

    /// <summary>
    /// Kicks the specified <see cref="Player"/> from the server.
    /// </summary>
    /// <param name="player">The player to kick.</param>
    /// <param name="issuer">The player that issued the kick.</param>
    /// <param name="reason">The reason of the kick.</param>
    /// <returns>Whether the player was successfully kicked.</returns>
    public static bool KickPlayer(Player player, Player issuer, string reason) =>
        global::BanPlayer.KickUser(player.ReferenceHub, issuer.ReferenceHub, reason);

    /// <summary>
    /// Bans a player from the server.
    /// </summary>
    /// <param name="userId">The User ID of player that will be banned.</param>
    /// <param name="reason">The ban reason.</param>
    /// <param name="duration">The duration of the ban.</param>
    /// <param name="bannedPlayerNickname">The nickname of the banned player.</param>
    /// <returns>Whether or not the ban was successful.</returns>
    public static bool BanUserId(string userId, string reason, long duration, string bannedPlayerNickname = "UnknownName") =>
        BanUserId(userId, Host, reason, duration, bannedPlayerNickname);

    /// <summary>
    /// Bans a player from the server.
    /// </summary>
    /// <param name="userId">The User ID of the player that will be banned.</param>
    /// <param name="issuer">The issuer of the ban.</param>
    /// <param name="reason">The ban reason.</param>
    /// <param name="duration">The duration of the ban.</param>
    /// <param name="bannedPlayerNickname">The nickname of the banned player.</param>
    /// <returns>Whether the ban was successful.</returns>
    public static bool BanUserId(string userId, Player? issuer, string reason, long duration, string bannedPlayerNickname = "UnknownName")
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(reason) || issuer == null)
        {
            return false;
        }

        return IssueBan(
            new BanDetails()
            {
                Id = userId,
                IssuanceTime = TimeBehaviour.CurrentTimestamp(),
                Expires = TimeBehaviour.GetBanExpirationTime((uint)duration),
                Issuer = issuer.LogName,
                OriginalName = bannedPlayerNickname,
                Reason = reason,
            },
            BanType.UserId);
    }

    /// <summary>
    /// Bans a player from the server.
    /// </summary>
    /// <param name="ipAddress">The IP address of the player which will be banned.</param>
    /// <param name="reason">The ban reason.</param>
    /// <param name="duration">The duration of the ban.</param>
    /// <param name="bannedPlayerNickname">The nickname of the banned player.</param>
    /// <returns>Whether the ban was successful.</returns>
    public static bool BanIpAddress(string ipAddress, string reason, long duration, string bannedPlayerNickname = "UnknownName") =>
        BanIpAddress(ipAddress, Host, reason, duration, bannedPlayerNickname);

    /// <summary>
    /// Bans a player from the server.
    /// </summary>
    /// <param name="ipAddress">The IP address of the player that will be banned.</param>
    /// <param name="issuer">The issuer of the ban.</param>
    /// <param name="reason">The ban reason.</param>
    /// <param name="duration">The duration of the ban.</param>
    /// <param name="bannedPlayerNickname">The nickname of the banned player.</param>
    /// <returns>Whether the ban was successful.</returns>
    public static bool BanIpAddress(string ipAddress, Player? issuer, string reason, long duration, string bannedPlayerNickname = "UnknownName")
    {
        if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrEmpty(reason) || issuer == null)
        {
            return false;
        }

        return IssueBan(
            new BanDetails()
            {
                Id = ipAddress,
                IssuanceTime = TimeBehaviour.CurrentTimestamp(),
                Expires = TimeBehaviour.GetBanExpirationTime((uint)duration),
                Issuer = issuer.LogName,
                OriginalName = bannedPlayerNickname,
                Reason = reason,
            },
            BanType.IP);
    }

    /// <summary>
    /// Unbans a player from the server.
    /// </summary>
    /// <param name="userId">The User ID of the player to unban.</param>
    /// <returns>Whether the unban was successful.</returns>
    public static bool UnbanUserId(string userId)
    {
        if (string.IsNullOrEmpty(userId) || !IsPlayerBanned(userId))
        {
            return false;
        }

        RemoveBan(userId, BanType.UserId);
        return true;
    }

    /// <summary>
    /// Unbans a player from the server.
    /// </summary>
    /// <param name="ipAddress">The IP address of the player.</param>
    /// <returns>Whether the unban was successful.</returns>
    public static bool UnbanIpAddress(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress) || !IsPlayerBanned(ipAddress))
        {
            return false;
        }

        RemoveBan(ipAddress, BanType.IP);
        return true;
    }

    /// <summary>
    /// Checks whether a player is banned.
    /// </summary>
    /// <param name="value">The User ID or IP address of the player.</param>
    /// <returns>Whether the player is banned.</returns>
    public static bool IsPlayerBanned(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        return (value.Contains("@") ? GetBan(value, BanType.UserId) : GetBan(value, BanType.IP)) != null;
    }

    /// <summary>
    /// Gets all banned players.
    /// </summary>
    /// <returns>A pooled list of all banned players.</returns>
    public static List<BanDetails> GetAllBannedPlayers()
    {
        List<BanDetails> bans = ListPool<BanDetails>.Shared.Rent();
        bans.AddRange(GetBans(BanType.UserId));
        bans.AddRange(GetBans(BanType.IP));
        return bans;
    }

    /// <summary>
    /// Gets all banned players by ban type.
    /// </summary>
    /// <param name="banType">The type of ban.</param>
    /// <returns>A pooled list of specified ban types.</returns>
    public static List<BanDetails> GetAllBannedPlayers(BanType banType) => GetBans(banType);

    #endregion

    /// <summary>
    /// Restarts the server and reconnects all players.
    /// </summary>
    public static void Restart() => Round.Restart(false, true, ServerStatic.NextRoundAction.Restart);

    /// <summary>
    /// Restarts the server and reconnects all players to target server port.
    /// </summary>
    /// <param name="redirectPort">The port number of the server to send all the players too.</param>
    public static void Restart(ushort redirectPort)
    {
        NetworkServer.SendToAll(new RoundRestartMessage(RoundRestartType.RedirectRestart, 0.1f, redirectPort, true, false));
        Round.Restart(false, true, ServerStatic.NextRoundAction.Restart);
    }

    /// <summary>
    /// Shutdowns the server and disconnects all players.
    /// </summary>
    public static void Shutdown() => global::Shutdown.Quit();

    /// <summary>
    /// Shutdowns the server and reconnects all players to target server port.
    /// </summary>
    /// <param name="redirectPort">The port number of the server to send all the players too.</param>
    public static void Shutdown(ushort redirectPort)
    {
        NetworkServer.SendToAll(new RoundRestartMessage(RoundRestartType.RedirectRestart, 0.1f, redirectPort, true, false));
        Shutdown();
    }

    /// <summary>
    /// Run a command in the server console.
    /// </summary>
    /// <param name="command">The command name.</param>
    /// <param name="sender">The <see cref="CommandSender"/> running the command.</param>
    /// <returns>The commands response.</returns>
    public static string RunCommand(string command, CommandSender? sender = null) =>
        ServerConsole.EnterCommand(command, sender);

    /// <summary>
    /// Sends a broadcast to all players.
    /// </summary>
    /// <param name="message">The message to be broadcast.</param>
    /// <param name="duration">The broadcast duration.</param>
    /// <param name="type">The broadcast type.</param>
    /// <param name="shouldClearPrevious">Clears previous displayed broadcast.</param>
    public static void SendBroadcast(string message, ushort duration, Broadcast.BroadcastFlags type = Broadcast.BroadcastFlags.Normal, bool shouldClearPrevious = false)
    {
        if (shouldClearPrevious)
        {
            ClearBroadcasts();
        }

        Broadcast.Singleton.RpcAddElement(message, duration, type);
    }

    /// <summary>
    /// Sends a broadcast to the specified <see cref="Player"/>.
    /// </summary>
    /// <param name="player">The player to send a broadcast.</param>
    /// <param name="message">The message to be broadcast.</param>
    /// <param name="duration">The broadcast duration.</param>
    /// <param name="type">The broadcast type.</param>
    /// <param name="shouldClearPrevious">Clears previous displayed broadcast.</param>
    public static void SendBroadcast(Player player, string message, ushort duration, Broadcast.BroadcastFlags type = Broadcast.BroadcastFlags.Normal, bool shouldClearPrevious = false)
    {
        if (shouldClearPrevious)
        {
            ClearBroadcasts(player);
        }

        Broadcast.Singleton.TargetAddElement(player.Connection, message, duration, type);
    }

    /// <summary>
    /// Sends the admin chat messages to all players with <see cref="PlayerPermissions.AdminChat"/> permissions.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="isSilent">Whether the message should not appear in broadcast.</param>
    public static void SendAdminChatMessage(string message, bool isSilent = false) => SendAdminChatMessage(Player.ReadyList.Where(static n => n.UserGroup != null && PermissionsHandler.IsPermitted(n.UserGroup.Permissions, PlayerPermissions.AdminChat)), message, isSilent);

    /// <summary>
    /// Sends admin chat message to all specified players.
    /// </summary>
    /// <param name="targetPlayers">The target players.</param>
    /// <param name="message">The message to send.</param>
    /// <param name="isSilent">Whether the message should not appear in broadcast.</param>
    public static void SendAdminChatMessage(IEnumerable<Player> targetPlayers, string message, bool isSilent = false)
    {
        StringBuilder sb = StringBuilderPool.Shared.Rent();

        sb.Append(Host!.NetworkId);
        sb.Append('!');

        if (isSilent)
        {
            sb.Append("@@");
        }

        sb.Append(message);

        string toSend = StringBuilderPool.Shared.ToStringReturn(sb);
        foreach (Player player in targetPlayers)
        {
            if (!player.IsPlayer || !player.IsReady)
            {
                continue;
            }

            player.ReferenceHub.encryptedChannelManager.TrySendMessageToClient(toSend, EncryptedChannelManager.EncryptedChannel.AdminChat);
        }
    }

    /// <summary>
    /// Clears broadcast's for all players.
    /// </summary>
    public static void ClearBroadcasts() => Broadcast.Singleton.RpcClearElements();

    /// <summary>
    /// Clears broadcast's for the specified <see cref="Player"/>.
    /// </summary>
    /// <param name="player">The player to clear the broadcast's.</param>
    public static void ClearBroadcasts(Player player) => Broadcast.Singleton.TargetClearElements(player.Connection);

    /// <summary>
    /// Interface for getting and setting key value limits.
    /// </summary>
    /// <typeparam name="TKey">The Key type.</typeparam>
    /// <typeparam name="TValue">The Value type.</typeparam>
    // TODO: we might want to move this outside the class into its own file or just remove it entirely and expose the implementing classes instead
    public interface ILimit<in TKey, TValue>
    {
        /// <summary>
        /// Key value getter and setter.
        /// </summary>
        /// <param name="index">The key used to identify the limit.</param>
        /// <returns>The limit value for the corresponding key.</returns>
        TValue this[TKey index]
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Private implementation class for synchronizing ItemCategory limits.
    /// </summary>
    private class CategoryLimitsSynchronizer : ILimit<ItemCategory, sbyte>
    {
        /// <inheritdoc/>
        public sbyte this[ItemCategory category]
        {
            get => InventoryLimits.StandardCategoryLimits[category];
            set
            {
                if (!InventoryLimits.StandardCategoryLimits.ContainsKey(category))
                {
                    throw new IndexOutOfRangeException($"Index {category} was not a valid ItemCategory type");
                }

                InventoryLimits.StandardCategoryLimits[category] = value;
                ServerConfigSynchronizer.Singleton.RefreshCategoryLimits();
            }
        }
    }

    /// <summary>
    /// Private implementation class for synchronizing Ammo limits.
    /// </summary>
    private class AmmoLimitsSynchronizer : ILimit<ItemType, ushort>
    {
        /// <inheritdoc/>
        public ushort this[ItemType ammo]
        {
            get => InventoryLimits.StandardAmmoLimits[ammo];
            set
            {
                if (!InventoryLimits.StandardAmmoLimits.ContainsKey(ammo))
                {
                    throw new IndexOutOfRangeException($"Index {ammo} was not a valid Ammo type");
                }

                InventoryLimits.StandardAmmoLimits[ammo] = value;
                ServerConfigSynchronizer.Singleton.RefreshAmmoLimits();
            }
        }
    }
}
