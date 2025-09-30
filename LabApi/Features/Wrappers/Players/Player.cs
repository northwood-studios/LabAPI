using CentralAuth;
using CommandSystem;
using CustomPlayerEffects;
using Footprinting;
using Generators;
using Hints;
using InventorySystem;
using InventorySystem.Disarming;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.Usables.Scp330;
using LabApi.Features.Enums;
using LabApi.Features.Stores;
using MapGeneration;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.NetworkMessages;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.Spectating;
using PlayerRoles.Voice;
using PlayerStatsSystem;
using RoundRestarting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using Utils.Networking;
using Utils.NonAllocLINQ;
using VoiceChat;
using VoiceChat.Playbacks;
using static PlayerStatsSystem.AhpStat;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ReferenceHub">reference hubs</see>, the in-game players.
/// </summary>
public class Player
{
    /// <summary>
    /// A cache of players by their User ID. Does not necessarily contain all players.
    /// </summary>
    private static readonly Dictionary<string, Player> UserIdCache = new(CustomNetworkManager.slots, StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Contains all the cached players in the game, accessible through their <see cref="ReferenceHub"/>.
    /// </summary>
    public static Dictionary<ReferenceHub, Player> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Player"/> instances currently in the game.
    /// </summary>
    /// <remarks>
    /// This list includes the host player, NPCs and unauthenticated players.
    /// <para>
    /// Unauthenticated players you must be especially careful with as interacting with them incorrectly will cause them to softlock while joining the game.
    /// Use <see cref="ReadyList"/> to get connected players that you can send network messages to.
    /// </para>
    /// </remarks>
    public static IReadOnlyCollection<Player> List => Dictionary.Values;

    /// <summary>
    /// A reference to all <see cref="Player"/> instances that are authenticated or dummy players.
    /// </summary>
    public static IEnumerable<Player> ReadyList => List.Where(x => x.IsDummy || (x.IsPlayer && x.IsReady));

    /// <summary>
    /// A reference to all <see cref="Player"/> instances that are NPCs.
    /// </summary>
    /// <remarks>
    /// The host player is not counted as an NPC.
    /// </remarks>
    public static IEnumerable<Player> NpcList => List.Where(x => x.IsNpc);

    /// <summary>
    /// A reference to all <see cref="Player"/> instance that are real players but are not authenticated yet.
    /// </summary>
    public static IEnumerable<Player> UnauthenticatedList => List.Where(x => x.IsPlayer && !x.IsReady);

    /// <summary>
    /// A reference to all <see cref="Player"/> instances that are dummy NPCs.
    /// </summary>
    public static IEnumerable<Player> DummyList => List.Where(x => x.IsDummy);

    /// <summary>
    /// A reference to all <see cref="Player"/> instance that are NPCs but are not dummies.
    /// </summary>
    public static IEnumerable<Player> RegularNpcList => List.Where(x => x.IsNpc && !x.IsDummy);

    /// <summary>
    /// The <see cref="Player"/> representing the host or server.
    /// </summary>
    public static Player? Host => Server.Host;

    /// <summary>
    /// Gets the amount of ready players or dummies.
    /// </summary>
    public static int Count => ReadyList.Count();

    /// <summary>
    /// Gets the amount of non-verified players.
    /// </summary>
    public static int NonVerifiedCount => UnauthenticatedList.Count();

    /// <summary>
    /// Gets the amount of connected players. Regardless of their authentication status.
    /// </summary>
    public static int ConnectionsCount => LiteNetLib4MirrorCore.Host.ConnectedPeersCount;

    /// <summary>
    /// Validates the custom info text and returns result whether it is valid or invalid.<br/>
    /// Current validation requirements are the following:
    /// <br/>
    /// <list type="bullet">
    /// <item>Match the <see cref="Misc.PlayerCustomInfoRegex"/> regex.</item>
    /// <item>Use only color,i,b and size rich text tags.</item>
    /// <item>Colors used have to be from <see cref="Misc.AcceptedColours"/></item>
    /// </list>
    /// <br/>
    /// </summary>
    /// <param name="text">The text to check on.</param>
    /// <param name="rejectionReason">Out parameter containing rejection reason.</param>
    /// <returns>Whether is the info parameter valid.</returns>
    public static bool ValidateCustomInfo(string text, out string rejectionReason) => NicknameSync.ValidateCustomInfo(text, out rejectionReason);

    /// <summary>
    /// Gets a all players matching the criteria specified by the <see cref="PlayerSearchFlags"/>.
    /// </summary>
    /// <param name="flags">The <see cref="PlayerSearchFlags"/> of the players to include.</param>
    /// <returns>The set of players that match the criteria.</returns>
    /// <remarks>
    /// By default this returns the same set of players as <see cref="ReadyList"/>.
    /// </remarks>
    public static IEnumerable<Player> GetAll(PlayerSearchFlags flags = PlayerSearchFlags.AuthenticatedAndDummy)
    {
        bool authenticated = (flags & PlayerSearchFlags.AuthenticatedPlayers) > 0;
        bool unauthenticated = (flags & PlayerSearchFlags.UnauthenticatedPlayers) > 0;
        bool dummyNpcs = (flags & PlayerSearchFlags.DummyNpcs) > 0;
        bool regularNpcs = (flags & PlayerSearchFlags.RegularNpcs) > 0;
        bool host = (flags & PlayerSearchFlags.Host) > 0;

        bool includePlayers = authenticated || unauthenticated;
        bool allPlayers = authenticated && unauthenticated;
        bool includeNpcs = dummyNpcs || regularNpcs;
        bool allNpcs = dummyNpcs && regularNpcs;

        foreach (Player player in List)
        {
            if ((includePlayers && player.IsPlayer && (allPlayers || player.IsReady == authenticated)) ||
                ((includeNpcs && player.IsNpc) && (allNpcs || player.IsDummy == dummyNpcs)) ||
                (host && player.IsHost))
            {
                yield return player;
            }
        }
    }

    #region Player Getters

    /// <summary>
    /// Gets the player wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    /// <returns>The requested player or null if the reference hub is null.</returns>
    [return: NotNullIfNotNull(nameof(referenceHub))]
    public static Player? Get(ReferenceHub? referenceHub)
    {
        if (referenceHub == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(referenceHub, out Player player) ? player : CreatePlayerWrapper(referenceHub);
    }

    /// <summary>
    /// Gets a list of players from a list of reference hubs.
    /// </summary>
    /// <param name="referenceHubs">The reference hubs of the players.</param>
    /// <returns>A list of players.</returns>
    public static List<Player> Get(IEnumerable<ReferenceHub> referenceHubs)
    {
        // We rent a list from the pool to avoid unnecessary allocations.
        // We don't care if the developer forgets to return the list to the pool
        // as at least it will be more efficient than always allocating a new list.
        List<Player> list = ListPool<Player>.Shared.Rent();
        return GetNonAlloc(referenceHubs, list);
    }

    /// <summary>
    /// Gets a list of players from a list of reference hubs without allocating a new list.
    /// </summary>
    /// <param name="referenceHubs">The reference hubs of the players.</param>
    /// <param name="list">A reference to the list to add the players to.</param>
    /// <returns>The <paramref name="list"/> passed in.</returns>
    public static List<Player> GetNonAlloc(IEnumerable<ReferenceHub> referenceHubs, List<Player> list)
    {
        // We clear the list to avoid any previous data.
        list.Clear();

        // And then we add all the players to the list.
        list.AddRange(referenceHubs.Select(Get)!);

        // We finally return the list.
        return list;
    }

    #region Get Player from a GameObject

    /// <summary>
    /// Gets the <see cref="Player"/> associated with the <see cref="GameObject"/>.
    /// </summary>
    /// <param name="gameObject">The <see cref="UnityEngine.GameObject"/> to get the player from.</param>
    /// <returns>The <see cref="Player"/> associated with the <see cref="GameObject"/> or null if it doesn't exist.</returns>
    public static Player? Get(GameObject? gameObject) => TryGet(gameObject, out Player? player) ? player : null;

    /// <summary>
    /// Tries to get the <see cref="Player"/> associated with the <see cref="GameObject"/>.
    /// </summary>
    /// <param name="gameObject">The <see cref="UnityEngine.GameObject"/> to get the player from.</param>
    /// <param name="player">The <see cref="Player"/> associated with the <see cref="UnityEngine.GameObject"/> or null if it doesn't exist.</param>
    /// <returns>Whether the player was successfully retrieved.</returns>
    public static bool TryGet(GameObject? gameObject, [NotNullWhen(true)] out Player? player)
    {
        player = null;
        if (gameObject == null)
        {
            return false;
        }

        if (!ReferenceHub.TryGetHub(gameObject, out ReferenceHub? hub))
        {
            return false;
        }

        player = Get(hub);
        return true;
    }

    #endregion

    #region Get Player from a NetworkIdentity

    /// <summary>
    /// Gets the <see cref="Player"/> associated with the <see cref="NetworkIdentity"/>.
    /// </summary>
    /// <param name="identity">The <see cref="NetworkIdentity"/> to get the player from.</param>
    /// <returns>The <see cref="Player"/> associated with the <see cref="NetworkIdentity"/> or null if it doesn't exist.</returns>
    public static Player? Get(NetworkIdentity? identity) => TryGet(identity, out Player? player) ? player : null;

    /// <summary>
    /// Tries to get the <see cref="Player"/> associated with the <see cref="NetworkIdentity"/>.
    /// </summary>
    /// <param name="identity">The <see cref="NetworkIdentity"/> to get the player from.</param>
    /// <param name="player">The <see cref="Player"/> associated with the <see cref="NetworkIdentity"/> or null if it doesn't exist.</param>
    /// <returns>Whether the player was successfully retrieved.</returns>
    public static bool TryGet(NetworkIdentity? identity, [NotNullWhen(true)] out Player? player)
    {
        player = null;
        if (identity == null)
        {
            return false;
        }

        if (!TryGet(identity.netId, out player))
        {
            return false;
        }

        return true;
    }

    #endregion

    #region Get Player from a NetworkIdentity.netId (uint)

    /// <summary>
    /// Gets the <see cref="Player"/> associated with the <see cref="NetworkIdentity.netId"/>.
    /// </summary>
    /// <param name="netId">The <see cref="NetworkIdentity.netId"/> to get the player from.</param>
    /// <returns>The <see cref="Player"/> associated with the <see cref="NetworkIdentity.netId"/> or null if it doesn't exist.</returns>
    public static Player? Get(uint netId) => TryGet(netId, out Player? player) ? player : null;

    /// <summary>
    /// Tries to get the <see cref="Player"/> associated with the <see cref="NetworkIdentity.netId"/>.
    /// </summary>
    /// <param name="netId">The <see cref="NetworkIdentity.netId"/> to get the player from.</param>
    /// <param name="player">The <see cref="Player"/> associated with the <see cref="NetworkIdentity.netId"/> or null if it doesn't exist.</param>
    /// <returns>Whether the player was successfully retrieved.</returns>
    public static bool TryGet(uint netId, [NotNullWhen(true)] out Player? player)
    {
        player = null;
        if (!ReferenceHub.TryGetHubNetID(netId, out ReferenceHub hub))
        {
            return false;
        }

        player = Get(hub);
        return true;
    }

    #endregion

    #region Get Player from a ICommandSender

    /// <summary>
    /// Gets the <see cref="Player"/> associated with the <see cref="ICommandSender"/>.
    /// </summary>
    /// <param name="sender">The <see cref="ICommandSender"/> to get the player from.</param>
    /// <returns>The <see cref="Player"/> associated with the <see cref="ICommandSender"/> or null if it doesn't exist.</returns>
    public static Player? Get(ICommandSender? sender) => TryGet(sender, out Player? player) ? player : null;

    /// <summary>
    /// Tries to get the <see cref="Player"/> associated with the <see cref="ICommandSender"/>.
    /// </summary>
    /// <param name="sender">The <see cref="ICommandSender"/> to get the player from.</param>
    /// <param name="player">The <see cref="Player"/> associated with the <see cref="ICommandSender"/> or null if it doesn't exist.</param>
    /// <returns>Whether the player was successfully retrieved.</returns>
    public static bool TryGet(ICommandSender? sender, [NotNullWhen(true)] out Player? player)
    {
        player = null;

        if (sender is not CommandSender commandSender)
        {
            return false;
        }

        return TryGet(commandSender.SenderId, out player);
    }

    #endregion

    #region Get Player from a UserId

    /// <summary>
    /// Gets the <see cref="Player"/> associated with the <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The User ID of the player.</param>
    /// <returns>The <see cref="Player"/> associated with the <paramref name="userId"/> or null if it doesn't exist.</returns>
    public static Player? Get(string? userId) => TryGet(userId, out Player? player) ? player : null;

    /// <summary>
    /// Tries to get the <see cref="Player"/> associated with the <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The user ID of the player.</param>
    /// <param name="player">The <see cref="Player"/> associated with the <paramref name="userId"/> or null if it doesn't exist.</param>
    /// <returns>Whether the player was successfully retrieved.</returns>
    public static bool TryGet(string? userId, [NotNullWhen(true)] out Player? player)
    {
        player = null;
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }

        if (UserIdCache.TryGetValue(userId!, out player) && player.IsOnline)
        {
            return true;
        }

        player = List.FirstOrDefault(x => x.UserId == userId);
        if (player == null)
        {
            return false;
        }

        UserIdCache[userId!] = player;
        return true;
    }

    #endregion

    #region Get Player from a Player Id

    /// <summary>
    /// Gets the <see cref="Player" /> associated with the <paramref name="playerId" />.
    /// </summary>
    /// <param name="playerId">The player ID of the player.</param>
    /// <returns>The <see cref="Player" /> associated with the <paramref name="playerId" /> or null if it doesn't exist.</returns>
    public static Player? Get(int playerId) => TryGet(playerId, out Player? player) ? player : null;

    /// <summary>
    /// Tries to get the <see cref="Player" /> associated with the <paramref name="playerId" />.
    /// </summary>
    /// <param name="playerId">The player ID of the player.</param>
    /// <param name="player">The <see cref="Player" /> associated with the <paramref name="playerId" /> or null if it doesn't exist.</param>
    /// <returns>Whether the player was successfully retrieved.</returns>
    public static bool TryGet(int playerId, [NotNullWhen(true)] out Player? player)
    {
        player = List.FirstOrDefault(n => n.PlayerId == playerId);
        return player != null;
    }

    #endregion

    #region Get Player from a Name

    /// <summary>
    /// Get closest player by lexicographical order.
    /// Players are compared by their <see cref="DisplayName"/>.
    /// </summary>
    /// <param name="input">The input to search the player by.</param>
    /// <param name="requireFullMatch">Whether the full match is required.</param>
    /// <returns>Player or <see langword="null"/> if no close player found.</returns>
    public static Player? GetByDisplayName(string input, bool requireFullMatch = false) => GetByName(input, requireFullMatch, static p => p.DisplayName);

    /// <summary>
    /// Gets the closest player by lexicographical order.
    /// Players are compared by their <see cref="Nickname"/>.
    /// </summary>
    /// <param name="input">The input to search the player by.</param>
    /// <param name="requireFullMatch">Whether the full match is required.</param>
    /// <returns>Player or <see langword="null"/> if no close player found.</returns>
    public static Player? GetByNickname(string input, bool requireFullMatch = false) => GetByName(input, requireFullMatch, static p => p.Nickname);

    /// <summary>
    /// Gets the closest player by lexicographical order.
    /// Base function to allow to select by <see langword="string"/> player property.
    /// </summary>
    /// <param name="input">The input to search the player by.</param>
    /// <param name="requireFullMatch">Whether the full match is required.</param>
    /// <param name="propertySelector">Function to select player property.</param>
    /// <returns>Player or <see langword="null"/> if no close player found.</returns>
    public static Player? GetByName(string input, bool requireFullMatch, Func<Player, string> propertySelector)
    {
        IOrderedEnumerable<Player> sortedPlayers = List.OrderBy(propertySelector);

        foreach (Player player in sortedPlayers)
        {
            string toCheck = propertySelector(player);
            if (requireFullMatch)
            {
                if (toCheck.Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    return player;
                }
            }
            else if (toCheck.StartsWith(input, StringComparison.OrdinalIgnoreCase))
            {
                return player;
            }
        }

        return null;
    }

    /// <summary>
    /// Tries to get players by name by seeing if their name starts with the input.
    /// </summary>
    /// <param name="input">The input to search for.</param>
    /// <param name="players">The output players if found.</param>
    /// <returns>True if the players are found, false otherwise.</returns>
    public static bool TryGetPlayersByName(string input, out List<Player> players)
    {
        players = GetNonAlloc(
            ReferenceHub.AllHubs.Where(x => x.nicknameSync.Network_myNickSync.StartsWith(input, StringComparison.OrdinalIgnoreCase)),
            ListPool<Player>.Shared.Rent());

        return players.Count > 0;
    }

    #endregion

    #endregion

    /// <summary>
    /// Initializes the <see cref="Player"/> class to subscribe to <see cref="ReferenceHub"/> events and handle the player cache.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();
        UserIdCache.Clear();

        ReferenceHub.OnPlayerAdded += AddPlayer;
        ReferenceHub.OnPlayerRemoved += RemovePlayer;
    }

    /// <summary>
    /// Creates a new wrapper for the player using the player's <see cref="global::ReferenceHub"/>.
    /// </summary>
    /// <param name="referenceHub">The <see cref="global::ReferenceHub"/> of the player.</param>
    /// <returns>The created player wrapper.</returns>
    private static Player CreatePlayerWrapper(ReferenceHub referenceHub)
    {
        Player player = new(referenceHub);

        if (referenceHub.isLocalPlayer)
        {
            Server.Host = player;
        }

        return player;
    }

    /// <summary>
    /// Handles the creation of a player in the server.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    private static void AddPlayer(ReferenceHub referenceHub)
    {
        try
        {
            if (Dictionary.ContainsKey(referenceHub))
            {
                return;
            }

            CreatePlayerWrapper(referenceHub);
        }
        catch (Exception ex)
        {
            Console.Logger.InternalError($"Failed to handle player addition with exception: {ex}");
        }
    }

    /// <summary>
    /// Handles the removal of a player from the server.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    private static void RemovePlayer(ReferenceHub referenceHub)
    {
        try
        {
            if (referenceHub.authManager.UserId != null)
            {
                UserIdCache.Remove(referenceHub.authManager.UserId);
            }

            if (TryGet(referenceHub.gameObject, out Player? player))
            {
                CustomDataStoreManager.RemovePlayer(player);
            }

            if (referenceHub.isLocalPlayer)
            {
                Server.Host = null;
            }

            Dictionary.Remove(referenceHub);
        }
        catch (Exception ex)
        {
            Console.Logger.InternalError($"Failed to handle player removal with exception: {ex}");
        }
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    internal Player(ReferenceHub referenceHub)
    {
        Dictionary.Add(referenceHub, this);

        ReferenceHub = referenceHub;
        CustomDataStoreManager.AddPlayer(this);
    }

    /// <summary>
    /// The <see cref="ReferenceHub">Reference Hub</see> of the player.
    /// </summary>
    public ReferenceHub ReferenceHub { get; }

    /// <summary>
    /// Gets the player's <see cref="GameObject"/>.
    /// </summary>
    public GameObject? GameObject => ReferenceHub ? ReferenceHub.gameObject : null;

    /// <summary>
    /// Gets whether the player is the host or server.
    /// </summary>
    public bool IsHost => ReferenceHub.isLocalPlayer;

    /// <summary>
    /// Gets whether the player is the dedicated server.
    /// </summary>
    [Obsolete($"Use {nameof(IsHost)} instead")]
    public bool IsServer => ReferenceHub.isLocalPlayer;

    /// <summary>
    /// Gets whether this <see cref="Player"/> instance is not controlled by a real human being.
    /// </summary>
    /// <remarks>
    /// This list includes dummy players.
    /// </remarks>
    public bool IsNpc => !IsHost && ReferenceHub.connectionToClient.GetType() != typeof(NetworkConnectionToClient);

    /// <summary>
    /// Gets whether the player is a real player and not the host or an Npc.
    /// </summary>
    public bool IsPlayer => Connection.GetType() == typeof(NetworkConnectionToClient);

    /// <summary>
    /// Gets whether the player is a dummy instance.
    /// </summary>
    public bool IsDummy => ReferenceHub.authManager.InstanceMode == ClientInstanceMode.Dummy;

    /// <summary>
    /// Gets the Player's User ID.
    /// </summary>
    public string UserId => ReferenceHub.authManager.UserId;

    /// <summary>
    /// Gets the player's Network ID.
    /// </summary>
    public uint NetworkId => ReferenceHub.characterClassManager.netId;

    /// <summary>
    /// Gets the player's <see cref="NetworkConnection"/>.
    /// </summary>
    public NetworkConnection Connection => IsHost ? ReferenceHub.networkIdentity.connectionToServer : ReferenceHub.networkIdentity.connectionToClient;

    /// <summary>
    /// Gets the player's <see cref="NetworkConnectionToClient"/>.
    /// </summary>
    public NetworkConnectionToClient ConnectionToClient => ReferenceHub.networkIdentity.connectionToClient;

    /// <summary>
    /// Gets the player's <see cref="RecyclablePlayerId"/> value.
    /// </summary>
    public int PlayerId => ReferenceHub.PlayerId;

    /// <summary>
    /// Gets if the player is currently offline.
    /// </summary>
    [Obsolete("Use IsDestroyed instead.")]
    public bool IsOffline => GameObject == null;

    /// <summary>
    /// Gets if the player is currently online.
    /// </summary>
    [Obsolete("Use !IsDestroyed instead.")]
    public bool IsOnline => !IsOffline;

    /// <summary>
    /// Gets whether the player was destroyed.
    /// </summary>
    public bool IsDestroyed => !ReferenceHub;

    /// <summary>
    /// Gets if the player is properly connected and authenticated.
    /// </summary>
    public bool IsReady => ReferenceHub.authManager.InstanceMode != ClientInstanceMode.Unverified && ReferenceHub.nicknameSync.NickSet;

    /// <summary>
    /// Gets the player's IP address.
    /// </summary>
    public string IpAddress => ReferenceHub.characterClassManager.connectionToClient.address;

    /// <summary>
    /// Gets or sets the player's current role.
    /// </summary>
    public RoleTypeId Role
    {
        get => ReferenceHub.GetRoleId();
        set => ReferenceHub.roleManager.ServerSetRole(value, RoleChangeReason.RemoteAdmin);
    }

    /// <summary>
    /// Gets the player's current <see cref="PlayerRoleBase"/>.
    /// </summary>
    public PlayerRoleBase RoleBase => ReferenceHub.roleManager.CurrentRole;

    /// <summary>
    /// Get's the player's current role unique identifier.
    /// </summary>
    public int LifeId => RoleBase.UniqueLifeIdentifier;

    /// <summary>
    /// Gets the Player's Nickname.
    /// </summary>
    public string Nickname => ReferenceHub.nicknameSync.MyNick;

    /// <summary>
    /// Gets or sets the Player's Display Name.
    /// </summary>
    public string DisplayName
    {
        get => ReferenceHub.nicknameSync.DisplayName;
        set => ReferenceHub.nicknameSync.DisplayName = value;
    }

    /// <summary>
    /// Gets the log name needed for command senders.
    /// </summary>
    public string LogName => IsHost ? "SERVER CONSOLE" : $"{Nickname} ({UserId})";

    /// <summary>
    /// Gets or sets the player's custom info.<br/>
    /// Do note that custom info is restricted by several things listed in <see cref="ValidateCustomInfo"/>.
    /// Please use this method to validate your string as it is validated on the client by the same method.
    /// </summary>
    public string CustomInfo
    {
        get => ReferenceHub.nicknameSync.CustomPlayerInfo;
        set => ReferenceHub.nicknameSync.CustomPlayerInfo = value;
    }

    /// <summary>
    /// Gets or sets the player's info area flags.
    /// Flags determine what info is displayed to other players when they hover their cross-hair over.
    /// </summary>
    public PlayerInfoArea InfoArea
    {
        get => ReferenceHub.nicknameSync.Network_playerInfoToShow;
        set => ReferenceHub.nicknameSync.Network_playerInfoToShow = value;
    }

    /// <summary>
    /// Gets or sets the player's current health.
    /// </summary>
    public float Health
    {
        get => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue = value;
    }

    /// <summary>
    /// Gets or sets the player's current maximum health.
    /// </summary>
    public float MaxHealth
    {
        get => ReferenceHub.playerStats.GetModule<HealthStat>().MaxValue;
        set => ReferenceHub.playerStats.GetModule<HealthStat>().MaxValue = value;
    }

    /// <summary>
    /// Gets or sets the player's current artificial health.<br/>
    /// Setting the value will clear all the current "processes" (each process is responsible for decaying AHP value separately. E.g 2 processes blue candy AHP, which doesn't decay and adrenaline process, where AHP does decay).<br/>
    /// Note: This value cannot be greater than <see cref="MaxArtificialHealth"/>. Set it to your desired value first if its over <see cref="AhpStat.DefaultMax"/> and then set this one.
    /// </summary>
    public float ArtificialHealth
    {
        get => ReferenceHub.playerStats.GetModule<AhpStat>().CurValue;
        set
        {
            AhpStat ahp = ReferenceHub.playerStats.GetModule<AhpStat>();
            ahp.ServerKillAllProcesses();

            if (value > 0)
            {
                ReferenceHub.playerStats.GetModule<AhpStat>().ServerAddProcess(value, MaxArtificialHealth, 0f, 1f, 0f, false);
            }
        }
    }

    /// <summary>
    /// Gets or sets the player's current maximum artificial health or hume shield.<br/>
    /// Note: The value resets to <see cref="AhpStat.DefaultMax"/> when the player's AHP reaches 0.
    /// </summary>
    public float MaxArtificialHealth
    {
        get => ReferenceHub.playerStats.GetModule<AhpStat>().MaxValue;
        set => ReferenceHub.playerStats.GetModule<AhpStat>().MaxValue = value;
    }

    /// <summary>
    /// Gets or sets the player's hume shield current value.
    /// </summary>
    public float HumeShield
    {
        get => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue = value;
    }

    /// <summary>
    /// Gets or sets the player's maximum hume shield value.
    /// Note: This value may change if the player passes a new hume shield threshold for SCPs.
    /// </summary>
    public float MaxHumeShield
    {
        get => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MaxValue;
        set => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MaxValue = value;
    }

    /// <summary>
    /// Gets or sets the current regeneration rate of the hume shield per second.
    /// Returns -1 if the player's role doesn't have hume shield controller.
    /// </summary>
    public float HumeShieldRegenRate
    {
        get
        {
            if (ReferenceHub.roleManager.CurrentRole is not IHumeShieldedRole role)
            {
                return -1;
            }

            return role.HumeShieldModule.HsRegeneration;
        }

        set
        {
            if (ReferenceHub.roleManager.CurrentRole is not IHumeShieldedRole role)
            {
                return;
            }

            ((DynamicHumeShieldController)role.HumeShieldModule).RegenerationRate = value;
        }
    }

    /// <summary>
    /// Gets or sets the time that must pass after taking damage for hume shield to regenerate again.
    /// Returns -1 if the player's role doesn't have hume shield controller.
    /// </summary>
    public float HumeShieldRegenCooldown
    {
        get
        {
            if (ReferenceHub.roleManager.CurrentRole is not IHumeShieldedRole role)
            {
                return -1;
            }

            return ((DynamicHumeShieldController)role.HumeShieldModule).RegenerationCooldown;
        }

        set
        {
            if (ReferenceHub.roleManager.CurrentRole is not IHumeShieldedRole role)
            {
                return;
            }

            ((DynamicHumeShieldController)role.HumeShieldModule).RegenerationCooldown = value;
        }
    }

    /// <summary>
    /// Gets or sets the player's current gravity. Default value is <see cref="FpcGravityController.DefaultGravity"/>.<br/>
    /// If the player's current role is not first person controlled (inherit from <see cref="IFpcRole"/> then <see cref="Vector3.zero"/> is returned.<br/>
    /// Y-axis is up and down. Negative values makes the player go down. Positive upwards. Player must not be grounded in order for gravity to take effect.
    /// </summary>
    public Vector3 Gravity
    {
        get
        {
            if (ReferenceHub.roleManager.CurrentRole is IFpcRole role)
            {
                return role.FpcModule.Motor.GravityController.Gravity;
            }

            return Vector3.zero;
        }

        set
        {
            if (ReferenceHub.roleManager.CurrentRole is IFpcRole role)
            {
                role.FpcModule.Motor.GravityController.Gravity = value;
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the player has remote admin access.
    /// </summary>
    public bool RemoteAdminAccess => ReferenceHub.serverRoles.RemoteAdmin;

    /// <summary>
    /// Gets a value indicating whether the player has Do-Not-Track enabled.
    /// </summary>
    public bool DoNotTrack => ReferenceHub.authManager.DoNotTrack;

    /// <summary>
    /// Gets or sets a value indicating whether the player is in overwatch mode.
    /// </summary>
    public bool IsOverwatchEnabled
    {
        get => ReferenceHub.serverRoles.IsInOverwatch;
        set => ReferenceHub.serverRoles.IsInOverwatch = value;
    }

    /// <summary>
    /// Gets the player this player is currently spectating.<br/>
    /// Returns null if current player is not spectator or the spectated player is not valid.
    /// </summary>
    public Player? CurrentlySpectating
    {
        get
        {
            if (RoleBase is not SpectatorRole sr)
            {
                return null;
            }

            if (!ReferenceHub.TryGetHubNetID(sr.SyncedSpectatedNetId, out ReferenceHub hub))
            {
                return null;
            }

            return Get(hub);
        }
    }

    /// <summary>
    /// Gets a pooled list of players who are currently spectating this player.
    /// </summary>
    public List<Player> CurrentSpectators
    {
        get
        {
            List<Player> list = ListPool<Player>.Shared.Rent();
            foreach (Player player in List)
            {
                if (ReferenceHub.IsSpectatedBy(player.ReferenceHub))
                {
                    list.Add(player);
                }
            }

            return list;
        }
    }

    /// <summary>
    /// Gets or sets whether this player can be spectated by other players.
    /// </summary>
    /// <remarks>
    /// This property is reset when player leaves.
    /// </remarks>
    public bool IsSpectatable
    {
        get => !SpectatableVisibilityManager.IsHidden(ReferenceHub);
        set => SpectatableVisibilityManager.SetHidden(ReferenceHub, !value);
    }

    /// <summary>
    /// Gets or sets the player's current <see cref="Item">item</see>.
    /// </summary>
    public Item? CurrentItem
    {
        get => Item.Get(Inventory.CurInstance);
        set
        {
            if (value == null || value.Type == ItemType.None)
            {
                Inventory.ServerSelectItem(0);
            }
            else
            {
                Inventory.ServerSelectItem(value.Serial);
            }
        }
    }

    /// <summary>
    /// Gets the player's currently active <see cref="StatusEffectBase">status effects</see>.
    /// </summary>
    public IEnumerable<StatusEffectBase> ActiveEffects => ReferenceHub.playerEffectsController.AllEffects.Where(static x => x.Intensity > 0);

    /// <summary>
    /// Gets the <see cref="LabApi.Features.Wrappers.Room"/> at the player's current position.
    /// May be <see langword="null"/> if the player is in the void.
    /// <para>
    /// Player inside of the elevator is consider to be in the said room until the elevator teleports to the next door.
    /// </para>
    /// </summary>
    public Room? Room => Room.TryGetRoomAtPosition(Position, out Room? room) ? room : null;

    /// <summary>
    /// Gets the cached room of the player. Cached room is revalidated once every frame or when player teleports.<br/>
    /// It is not guarantee that the <see cref="Position"/> will match the exact same room it should be in due to the caching.<br/>
    /// May be <see langword="null"/> if the player is in the void.
    /// </summary>
    public Room? CachedRoom => ReferenceHub.TryGetCurrentRoom(out RoomIdentifier rid) ? Room.Get(rid) : null;

    /// <summary>
    /// Gets the <see cref="FacilityZone"/> for the player's current room. Returns <see cref="FacilityZone.None"/> if the room is null.
    /// </summary>
    public FacilityZone Zone => Room?.Zone ?? FacilityZone.None;

    /// <summary>
    /// Gets the <see cref="Item">items</see> in the player's inventory.
    /// </summary>
    public IEnumerable<Item> Items => Inventory.UserInventory.Items.Values.Select(Item.Get)!;

    /// <summary>
    /// Gets the player's Reserve Ammo.
    /// </summary>
    public Dictionary<ItemType, ushort> Ammo => Inventory.UserInventory.ReserveAmmo;

    /// <summary>
    /// Gets or sets the player's group color.
    /// </summary>
    public string GroupColor
    {
        get => ReferenceHub.serverRoles.Network_myColor;
        set => ReferenceHub.serverRoles.SetColor(value);
    }

    /// <summary>
    /// Gets or sets what is displayed for the player's group.
    /// </summary>
    public string GroupName
    {
        get => ReferenceHub.serverRoles.Network_myText;
        set => ReferenceHub.serverRoles.SetText(value);
    }

    /// <summary>
    /// Gets or sets the player's <see cref="UserGroup"/>.
    /// </summary>
    public UserGroup? UserGroup
    {
        get => ReferenceHub.serverRoles.Group;
        set => ReferenceHub.serverRoles.SetGroup(value);
    }

    /// <summary>
    /// Gets the player's default permission group name. Or null if the player is not in a group.
    /// </summary>
    public string? PermissionsGroupName => ServerStatic.PermissionsHandler.Members.GetValueOrDefault(UserId);

    /// <summary>s
    /// Gets the player's unit ID, or -1 if the role is not a <see cref="HumanRole"/>.
    /// </summary>
    public int UnitId => RoleBase is HumanRole humanRole ? humanRole.UnitNameId : -1;

    /// <summary>
    /// Gets a value indicating whether the player has a reserved slot.
    /// </summary>
    public bool HasReservedSlot => ReservedSlot.HasReservedSlot(UserId);

    /// <summary>
    /// Gets the player's velocity.
    /// </summary>
    public Vector3 Velocity => ReferenceHub.GetVelocity();

    /// <summary>
    /// Gets the player's <see cref="Inventory"/>.
    /// </summary>
    public Inventory Inventory => ReferenceHub.inventory;

    /// <summary>
    /// Gets the <see cref="VoiceModuleBase"/> for the player, or null if the player does not have a voice module.
    /// </summary>
    public VoiceModuleBase? VoiceModule => RoleBase is IVoiceRole voiceRole ? voiceRole.VoiceModule : null;

    /// <summary>
    /// Gets the current <see cref="VoiceChatChannel"/> for the player, or <see cref="VoiceChatChannel.None"/> if the player is not using a voice module.
    /// </summary>
    public VoiceChatChannel VoiceChannel => VoiceModule?.CurrentChannel ?? VoiceChatChannel.None;

    /// <summary>
    /// Gets a value indicating whether the player has no items in their inventory.
    /// </summary>
    public bool IsWithoutItems => Inventory.UserInventory.Items.Count == 0;

    /// <summary>
    /// Gets a value indicating whether the player's inventory is full.
    /// </summary>
    public bool IsInventoryFull => Inventory.UserInventory.Items.Count >= Inventory.MaxSlots;

    /// <summary>
    /// Gets a value indicating whether the player is out of ammunition.
    /// </summary>
    public bool IsOutOfAmmo => Inventory.UserInventory.ReserveAmmo.All(ammo => ammo.Value == 0);

    /// <summary>
    /// Gets or sets a value indicating whether the player is disarmed.
    /// </summary>
    public bool IsDisarmed
    {
        get => Inventory.IsDisarmed();
        set
        {
            if (value)
            {
                Inventory.SetDisarmedStatus(null);
                DisarmedPlayers.Entries.Add(new DisarmedPlayers.DisarmedEntry(ReferenceHub.networkIdentity.netId, 0U));
                new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
                return;
            }

            Inventory.SetDisarmedStatus(null);
            new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
        }
    }

    /// <summary>
    /// Gets a value indicating whether the player is muted.
    /// </summary>
    public bool IsMuted => VoiceChatMutes.IsMuted(ReferenceHub);

    /// <summary>
    /// Gets a value indicating whether the player is muted from the intercom.
    /// </summary>
    public bool IsIntercomMuted => VoiceChatMutes.IsMuted(ReferenceHub, true);

    /// <summary>
    /// Gets a value indicating whether the player is talking through a radio.
    /// </summary>
    public bool IsUsingRadio => PersonalRadioPlayback.IsTransmitting(ReferenceHub);

    /// <summary>
    /// Gets a value indicating whether the player is speaking.
    /// </summary>
    public bool IsSpeaking => VoiceModule != null && VoiceModule.IsSpeaking;

    /// <summary>
    /// Gets a value indicating whether the player is a Global Moderator.
    /// </summary>
    public bool IsGlobalModerator => ReferenceHub.authManager.RemoteAdminGlobalAccess;

    /// <summary>
    /// Gets a value indicating whether the player is a Northwood Staff member.
    /// </summary>
    public bool IsNorthwoodStaff => ReferenceHub.authManager.NorthwoodStaff;

    /// <summary>
    /// Gets or sets a value indicating whether bypass mode is enabled for the player, allowing them to open doors/gates without keycards.
    /// </summary>
    public bool IsBypassEnabled
    {
        get => ReferenceHub.serverRoles.BypassMode;
        set => ReferenceHub.serverRoles.BypassMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether god mode is enabled for the player.
    /// </summary>
    public bool IsGodModeEnabled
    {
        get => ReferenceHub.characterClassManager.GodMode;
        set => ReferenceHub.characterClassManager.GodMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether noclip mode is enabled for the player.
    /// </summary>
    public bool IsNoclipEnabled
    {
        get => ReferenceHub.playerStats.GetModule<AdminFlagsStat>().HasFlag(AdminFlags.Noclip);
        set => ReferenceHub.playerStats.GetModule<AdminFlagsStat>().SetFlag(AdminFlags.Noclip, value);
    }

    /// <summary>
    /// Gets or sets the player who disarmed this player.
    /// </summary>
    public Player? DisarmedBy
    {
        get
        {
            if (!IsDisarmed)
            {
                return null;
            }

            DisarmedPlayers.DisarmedEntry entry = DisarmedPlayers.Entries.Find(x => x.DisarmedPlayer == NetworkId);

            return Get(entry.Disarmer);
        }

        set
        {
            Inventory.SetDisarmedStatus(null);

            if (value == null)
            {
                return;
            }

            DisarmedPlayers.Entries.Add(new DisarmedPlayers.DisarmedEntry(NetworkId, value.NetworkId));
            new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
        }
    }

    /// <summary>
    /// Gets the player's current <see cref="Team"/>.
    /// </summary>
    public Team Team => RoleBase.Team;

    /// <summary>
    /// Gets the player's current <see cref="Faction"/>.
    /// </summary>
    public Faction Faction => Team.GetFaction();

    /// <summary>
    /// Gets whether the player is currently Alive.
    /// </summary>
    public bool IsAlive => Team != Team.Dead;

    /// <summary>
    /// Gets if the player is an SCP.
    /// </summary>
    public bool IsSCP => Team == Team.SCPs;

    /// <summary>
    /// Gets if the player is a human.
    /// </summary>
    public bool IsHuman => IsAlive && !IsSCP;

    /// <summary>
    /// Gets if the player is part of the NTF.
    /// </summary>
    public bool IsNTF => Team == Team.FoundationForces;

    /// <summary>
    /// Gets if the player is part of the Chaos Insurgency.
    /// </summary>
    public bool IsChaos => Team == Team.ChaosInsurgency;

    /// <summary>
    /// Gets if the player is a tutorial.
    /// </summary>
    public bool IsTutorial => Role == RoleTypeId.Tutorial;

    /// <summary>
    /// Gets the player's Camera <see cref="Transform"/>.
    /// </summary>
    public Transform Camera => ReferenceHub.PlayerCameraReference;

    /// <summary>
    /// Gets or sets the player's position.<br/>
    /// Returns <see cref="Vector3.zero"/> if the player's role is not currently derived from <see cref="IFpcRole"/>.
    /// </summary>
    public Vector3 Position
    {
        get
        {
            if (RoleBase is not IFpcRole fpcRole)
            {
                return Vector3.zero;
            }

            return fpcRole.FpcModule.Position;
        }
        set => ReferenceHub.TryOverridePosition(value);
    }

    /// <summary>
    /// Gets or sets the player's rotation.
    /// </summary>
    public Quaternion Rotation
    {
        get => GameObject.transform.rotation;
        set => ReferenceHub.TryOverrideRotation(value.eulerAngles);
    }

    /// <summary>
    /// Gets or sets the player's look rotation. X is vertical axis while Y is horizontal. Vertical axis is clamped by the base game logic.<br/>
    /// Returns <see cref="Vector2.zero"/> if the player's role is not currently derived from <see cref="IFpcRole"/>.
    /// </summary>
    public Vector2 LookRotation
    {
        get
        {
            if (ReferenceHub.roleManager.CurrentRole is not IFpcRole fpcRole)
            {
                return Vector2.zero;
            }

            FpcMouseLook mouseLook = fpcRole.FpcModule.MouseLook;
            return new Vector2(mouseLook.CurrentVertical, mouseLook.CurrentHorizontal);
        }
        set => ReferenceHub.TryOverrideRotation(value);
    }

    /// <summary>
    /// Gets or sets player's scale. Player's role must be <see cref="IFpcRole"/> for it to take effect.<br/>
    /// Vertical scale is not linear as the model's origin and scaling is done from player's feet.
    /// </summary>
    public Vector3 Scale
    {
        get
        {
            if (ReferenceHub.roleManager.CurrentRole is not IFpcRole fpcRole)
            {
                return Vector3.zero;
            }

            return fpcRole.FpcModule.Motor.ScaleController.Scale;
        }

        set
        {
            if (ReferenceHub.roleManager.CurrentRole is not IFpcRole fpcRole)
            {
                return;
            }

            fpcRole.FpcModule.Motor.ScaleController.Scale = value;
        }
    }

    /// <summary>
    /// Gets or sets player's remaining stamina (min = 0, max = 1).
    /// </summary>
    public float StaminaRemaining
    {
        get => ReferenceHub.playerStats.GetModule<StaminaStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<StaminaStat>().CurValue = value;
    }

    /// <summary>
    /// Gets or sets the current player's emotion.
    /// </summary>
    public EmotionPresetType Emotion
    {
        get => EmotionSync.GetEmotionPreset(ReferenceHub);
        set => EmotionSync.ServerSetEmotionPreset(ReferenceHub, value);
    }

    /// <summary>
    /// Teleports the player by the delta location.
    /// </summary>
    /// <param name="delta">Position to add to the current one.</param>
    public void Move(Vector3 delta) => ReferenceHub.TryOverridePosition(Position + delta);

    /// <summary>
    /// Rotates the player by the parameter.
    /// </summary>
    /// <param name="delta">Rotation to add to the current one. X is vertical and Y is horizontal rotation.</param>
    public void Rotate(Vector2 delta) => ReferenceHub.TryOverrideRotation(LookRotation + delta);

    /// <summary>
    /// Forces <see cref="IFpcRole"/> to jump.
    /// <para>Jumping can be also adjusted via <see cref="HeavyFooted"/> and <see cref="Lightweight"/> status effects.</para>
    /// </summary>
    /// <param name="jumpStrength">Strength that the player will jump with.</param>
    public void Jump(float jumpStrength)
    {
        if (ReferenceHub.roleManager.CurrentRole is IFpcRole fpcRole)
        {
            fpcRole.FpcModule.Motor.JumpController.ForceJump(jumpStrength);
        }
    }

    /// <inheritdoc cref="Jump(float)"/>
    public void Jump()
    {
        if (ReferenceHub.roleManager.CurrentRole is IFpcRole fpcRole)
        {
            Jump(fpcRole.FpcModule.JumpSpeed);
        }
    }

    /// <summary>
    /// Clears displayed broadcast(s).
    /// </summary>
    public void ClearBroadcasts() => Server.ClearBroadcasts(this);

    /// <summary>
    /// Sends a broadcast to the player.
    /// </summary>
    /// <param name="message">The message to be broadcast.</param>
    /// <param name="duration">The broadcast duration.</param>
    /// <param name="type">The broadcast type.</param>
    /// <param name="shouldClearPrevious">Whether it should clear previous broadcasts.</param>
    public void SendBroadcast(string message, ushort duration, Broadcast.BroadcastFlags type = Broadcast.BroadcastFlags.Normal, bool shouldClearPrevious = false)
        => Server.SendBroadcast(this, message, duration, type, shouldClearPrevious);

    /// <summary>
    /// Sends a message to the player's console.
    /// </summary>
    /// <param name="message">The message to be sent.</param>
    /// <param name="color">The color of the message.</param>
    public void SendConsoleMessage(string message, string color = "green") => ReferenceHub.gameConsoleTransmission.SendToClient(message, color);

    /// <summary>
    /// Issue a mute to a player, preventing them from speaking.
    /// </summary>
    /// <param name="isTemporary">Whether the mute is temporary, or should be added to the mute file.</param>
    public void Mute(bool isTemporary = true)
    {
        if (isTemporary)
        {
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) | VcMuteFlags.LocalRegular);
        }
        else
        {
            VoiceChatMutes.IssueLocalMute(UserId);
        }
    }

    /// <summary>
    /// Revokes a mute from a player, allowing them to speak again.
    /// </summary>
    /// <param name="revokeMute">If set to true, this player's <see cref="UserId"/> will be removed from the mute file.</param>
    public void Unmute(bool revokeMute)
    {
        if (revokeMute)
        {
            VoiceChatMutes.RevokeLocalMute(UserId);
        }
        else
        {
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) & ~VcMuteFlags.LocalRegular);
        }
    }

    /// <summary>
    /// Issue a mute to a player, preventing them from speaking through the intercom.
    /// </summary>
    /// <param name="isTemporary">Whether the mute is temporary, or should be added to the mute file.</param>
    public void IntercomMute(bool isTemporary = false)
    {
        if (isTemporary)
        {
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) | VcMuteFlags.LocalIntercom);
        }
        else
        {
            VoiceChatMutes.IssueLocalMute(UserId, true);
        }
    }

    /// <summary>
    /// Revokes a mute from a player, allowing them to speak through the intercom again.
    /// </summary>
    /// <param name="revokeMute">If set to true, this player's <see cref="UserId"/> will be removed from the mute file.</param>
    public void IntercomUnmute(bool revokeMute)
    {
        if (revokeMute)
        {
            VoiceChatMutes.RevokeLocalMute(UserId, true);
        }
        else
        {
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) & ~VcMuteFlags.LocalIntercom);
        }
    }

    /// <summary>
    /// Adds ammo of the specified type to the player's inventory.
    /// </summary>
    /// <param name="item">The type of ammo.</param>
    /// <param name="amount">The amount of ammo.</param>
    public void AddAmmo(ItemType item, ushort amount) => Inventory.ServerAddAmmo(item, amount);

    /// <summary>
    /// Adds an item of the specified type to the player's inventory.
    /// </summary>
    /// <param name="item">The type of item.</param>
    /// <param name="reason">The reason why is this item being added.</param>
    /// <returns>The <see cref="Item"/> added or null if it could not be added.</returns>
    public Item? AddItem(ItemType item, ItemAddReason reason = ItemAddReason.AdminCommand) => Item.Get(Inventory.ServerAddItem(item, reason));

    /// <summary>
    /// Adds an item by picking it up.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> to pickup.</param>
    /// <returns>The <see cref="Item"/> added or null if it could not be added.</returns>
    public Item? AddItem(Pickup pickup)
        => Item.Get(Inventory.ServerAddItem(pickup.Type, ItemAddReason.PickedUp, pickup.Serial, pickup.Base));

    /// <summary>
    /// Removes a specific <see cref="Item"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public void RemoveItem(Item item) => RemoveItem(item.Base);

    /// <summary>
    /// Removes a specific <see cref="ItemBase"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public void RemoveItem(ItemBase item) => Inventory.ServerRemoveItem(item.ItemSerial, null);

    /// <summary>
    /// Removes a specific <see cref="Pickup"/> from the player's inventory.
    /// </summary>
    /// <param name="pickup">The pickup to remove.</param>
    public void RemoveItem(Pickup pickup) => RemoveItem(pickup.Base);

    /// <summary>
    /// Removes a specific <see cref="ItemPickupBase"/> from the player's inventory.
    /// </summary>
    /// <param name="pickup">The pickup to remove.</param>
    public void RemoveItem(ItemPickupBase pickup) => Inventory.ServerRemoveItem(pickup.Info.Serial, null);

    /// <summary>
    /// Removes all items of the specified type from the player's inventory.
    /// </summary>
    /// <param name="item">The type of item.</param>
    /// <param name="maxAmount">The maximum amount of items to remove.</param>
    public void RemoveItem(ItemType item, int maxAmount = 1)
    {
        int count = 0;
        for (int i = 0; i < Items.Count(); i++)
        {
            ItemBase? itemBase = Items.ElementAt(i).Base;
            if (itemBase.ItemTypeId != item)
            {
                continue;
            }

            RemoveItem(itemBase);
            if (++count >= maxAmount)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Drops the specified <see cref="Item"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>The dropped <see cref="Pickup">item pickup</see>.</returns>
    public Pickup DropItem(Item item) => DropItem(item.Base);

    /// <summary>
    /// Drops the specified <see cref="ItemBase"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>The dropped <see cref="Pickup">item pickup</see>.</returns>
    public Pickup DropItem(ItemBase item) => DropItem(item.ItemSerial);

    /// <summary>
    /// Drops the item with the specified serial from the player's inventory.
    /// </summary>
    /// <param name="serial">The serial of the item.</param>
    /// <returns>The dropped <see cref="Pickup">item pickup</see>.</returns>
    public Pickup DropItem(ushort serial) => Pickup.Get(Inventory.ServerDropItem(serial));

    /// <summary>
    /// Drops all items from the player's inventory.
    /// </summary>
    /// <returns>The pooled list of dropped items. Please return when your done with it.</returns>
    public List<Pickup> DropAllItems()
    {
        List<Pickup> items = ListPool<Pickup>.Shared.Rent();
        foreach (Item item in Items.ToArray())
        {
            items.Add(DropItem(item));
        }

        return items;
    }

    /// <summary>
    /// Sets the ammo amount of a specific ammo type.
    /// </summary>
    /// <param name="item">The type of ammo.</param>
    /// <param name="amount">The amount of ammo.</param>
    public void SetAmmo(ItemType item, ushort amount) => Inventory.ServerSetAmmo(item, amount);

    /// <summary>
    /// Gets ammo amount of specific ammo type.
    /// </summary>
    /// <param name="item">The type of ammo.</param>
    /// <returns>The amount of ammo which the player has.</returns>
    public ushort GetAmmo(ItemType item) => ReferenceHub.inventory.GetCurAmmo(item);

    /// <summary>
    /// Drops ammo of the specified type from the player's inventory.
    /// </summary>
    /// <param name="item">The type of ammo.</param>
    /// <param name="amount">The amount to drop.</param>
    /// <param name="checkMinimals">Will prevent dropping small amounts of ammo.</param>
    /// <returns>The dropped ammo.</returns>
    public IEnumerable<AmmoPickup> DropAmmo(ItemType item, ushort amount, bool checkMinimals = true) => Inventory.ServerDropAmmo(item, amount, checkMinimals).Select(x => AmmoPickup.Get(x));

    /// <summary>
    /// Drops all ammo from the player's inventory.
    /// </summary>
    /// <returns>The list of dropped ammo.</returns>
    public List<AmmoPickup> DropAllAmmo()
    {
        List<AmmoPickup> ammo = ListPool<AmmoPickup>.Shared.Rent();

        foreach (KeyValuePair<ItemType, ushort> pair in Ammo.ToDictionary(e => e.Key, e => e.Value))
        {
            ammo.AddRange(DropAmmo(pair.Key, pair.Value));
        }

        return ammo;
    }

    /// <summary>
    /// Drops all items and ammo from the player's inventory.
    /// </summary>
    public void DropEverything() => Inventory.ServerDropEverything();

    /// <summary>
    /// Clear Items from the player's inventory.
    /// </summary>
    public void ClearItems()
    {
        foreach (Item item in Items.ToArray())
        {
            RemoveItem(item);
        }
    }

    /// <summary>
    /// Clear Ammo from the player's inventory.
    /// </summary>
    public void ClearAmmo()
    {
        if (Ammo.Any(x => x.Value > 0))
        {
            Inventory.SendAmmoNextFrame = true;
        }

        Ammo.Clear();
    }

    /// <summary>
    /// Clears the player's inventory.
    /// </summary>
    /// <param name="clearAmmo">Whether to clear the player's ammo.</param>
    /// <param name="clearItems">Whether to clear the player's items.</param>
    public void ClearInventory(bool clearAmmo = true, bool clearItems = true)
    {
        if (clearAmmo)
        {
            ClearAmmo();
        }

        if (clearItems)
        {
            ClearItems();
        }
    }

    /// <summary>
    /// Gives a candy to the player.
    /// </summary>
    /// <param name="candy">The candy to give the player.</param>
    /// <param name="reason">The reason to grant the candy bag.</param>
    public void GiveCandy(CandyKindID candy, ItemAddReason reason)
        => ReferenceHub.GrantCandy(candy, reason);

    /// <summary>
    /// Gives a random candy to the player.
    /// </summary>
    /// <param name="reason">The reason to grant the candy bag.</param>
    /// <remarks>This will use <see cref="Scp330Candies.GetRandom"/>, meaning it will use <see cref="ICandy.SpawnChanceWeight"/> to choose the candy.</remarks>
    public void GiveRandomCandy(ItemAddReason reason = ItemAddReason.AdminCommand)
        => GiveCandy(Scp330Candies.GetRandom(), reason);

    /// <summary>
    /// Checks if a player has the specified <see cref="PlayerPermissions"/>.
    /// </summary>
    /// <param name="permission">The permission to check the player for.</param>
    /// <returns>Whether the permission check was successful.</returns>
    public bool HasPermission(PlayerPermissions permission)
    {
        PlayerPermissions currentPerms = (PlayerPermissions)ReferenceHub.serverRoles.Permissions;
        return currentPerms.HasFlag(permission);
    }

    /// <summary>
    /// Adds regeneration to the player.
    /// </summary>
    /// <param name="rate">The rate to heal per second.</param>
    /// <param name="duration">How long the regeneration should last.</param>
    public void AddRegeneration(float rate, float duration)
        => Scp330Bag.AddSimpleRegeneration(ReferenceHub, rate, duration);

    /// <summary>
    /// Heals the player by the specified amount.
    /// </summary>
    /// <param name="amount">The amount to heal.</param>
    public void Heal(float amount) => ReferenceHub.playerStats.GetModule<HealthStat>().ServerHeal(amount);

    /// <summary>
    /// Creates and run a new AHP process.
    /// </summary>
    /// <param name="amount">Amount of AHP to be added.</param>
    /// <param name="limit">Adds limit to the AHP.</param>
    /// <param name="decay">Rate of AHP decay (per second).</param>
    /// <param name="efficacy">Value between 0 and 1. Defines what % of damage will be absorbed.</param>
    /// <param name="sustain">Pauses decay for specified amount of seconds.</param>
    /// <param name="persistent">If true, it won't be automatically removed when reaches 0.</param>
    /// <returns>Process in case it needs to be removed. Use <see cref="ServerKillProcess(AhpProcess)"/> to kill it.</returns>
    public AhpProcess CreateAhpProcess(float amount, float limit, float decay, float efficacy, float sustain, bool persistent) =>
        ReferenceHub.playerStats.GetModule<AhpStat>().ServerAddProcess(amount, limit, decay, efficacy, sustain, persistent);

    /// <summary>
    /// Kills the AHP process.
    /// </summary>
    /// <param name="process">Process to be killed.</param>
    public void ServerKillProcess(AhpProcess process) => ReferenceHub.playerStats.GetModule<AhpStat>().ServerKillProcess(process.KillCode);

    /// <summary>
    /// Sets the player's role.
    /// </summary>
    /// <param name="newRole">The <see cref="RoleTypeId"/> which will be set.</param>
    /// <param name="reason">The <see cref="RoleChangeReason"/> of role change.</param>
    /// <param name="flags">The <see cref="RoleSpawnFlags"/> of role change.</param>
    public void SetRole(RoleTypeId newRole, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags flags = RoleSpawnFlags.All) => ReferenceHub.roleManager.ServerSetRole(newRole, reason, flags);

    /// <summary>
    /// Determines if <paramref name="otherPlayer"/> is seen as spectator or their role based on visibility, permissions, and distance of this player.
    /// </summary>
    /// <param name="otherPlayer">The other player to check.</param>
    /// <returns>The role this player sees for the other player.</returns>
    public RoleTypeId GetRoleVisibilityFor(Player otherPlayer) => FpcServerPositionDistributor.GetVisibleRole(otherPlayer.ReferenceHub, ReferenceHub);

    /// <summary>
    /// Disconnects the player from the server.
    /// </summary>
    /// <param name="reason">The reason for the disconnection.</param>
    public void Disconnect(string? reason = null) => ServerConsole.Disconnect(GameObject, reason ?? string.Empty);

    /// <summary>
    /// Sends the player a text hint.
    /// </summary>
    /// <param name="text">The text which will be displayed.</param>
    /// <param name="duration">The duration of which the text will be visible in seconds.</param>
    public void SendHint(string text, float duration = 3f) =>
        SendHint(text, [new StringHintParameter(string.Empty)], null, duration);

    /// <summary>
    /// Sends the player a text hint with effects.
    /// </summary>
    /// <param name="text">The text which will be displayed.</param>
    /// <param name="effects">The effects of text.</param>
    /// <param name="duration">The duration of which the text will be visible in seconds.</param>
    public void SendHint(string text, HintEffect[] effects, float duration = 3f) =>
        ReferenceHub.hints.Show(new TextHint(text, [new StringHintParameter(string.Empty)], effects, duration));

    /// <summary>
    /// Sends the player a text hint with parameters.
    /// </summary>
    /// <param name="text">The text which will be displayed.</param>
    /// <param name="parameters">The parameters to interpolate into the text.</param>
    /// <param name="effects">The effects used for hint animations. See <see cref="HintEffect"/>.</param>
    /// <param name="duration">The duration of which the text will be visible.</param>
    /// <remarks>
    /// Parameters are interpolated into the string on the client.
    /// E.g. <c>"Test param1: {0} param2: {1}"</c>.
    /// </remarks>
    public void SendHint(string text, HintParameter[] parameters, HintEffect[]? effects = null, float duration = 3f) =>
        ReferenceHub.hints.Show(new TextHint(text, parameters.IsEmpty() ? [new StringHintParameter(string.Empty)] : parameters, effects, duration));

    /// <summary>
    /// Sends the player a hit marker.
    /// </summary>
    /// <param name="size">The size of hit marker.</param>
    public void SendHitMarker(float size = 1f) => Hitmarker.SendHitmarkerDirectly(Connection, size);

    /// <summary>
    /// Gets the stats module.
    /// </summary>
    /// <typeparam name="T">The type of the stat module.</typeparam>
    /// <returns>The stat module.</returns>
    public T GetStatModule<T>()
        where T : StatBase => ReferenceHub.playerStats.GetModule<T>();

    /// <summary>
    /// Gets whether the player has a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <typeparam name="T">The type of the status effect to check.</typeparam>
    /// <returns>Whether the player has the status effect.</returns>
    public bool HasEffect<T>()
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.TryGetEffect(out T? effect) && effect != null && effect.IsEnabled;

    /// <summary>
    /// Disables all active <see cref="StatusEffectBase">status effects</see>.
    /// </summary>
    public void DisableAllEffects() => ReferenceHub.playerEffectsController.DisableAllEffects();

    /// <summary>
    /// Disables a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <typeparam name="T">The type of the status effect to disable.</typeparam>
    public void DisableEffect<T>()
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.DisableEffect<T>();

    /// <summary>
    /// Disables a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <param name="effect">The status effect to disable.</param>
    public void DisableEffect(StatusEffectBase? effect) => effect?.ServerDisable();

    /// <summary>
    /// Enables a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <typeparam name="T">The type of the status effect to enable.</typeparam>
    /// <param name="intensity">The intensity of the status effect.</param>
    /// <param name="duration">The duration of the status effect.</param>
    /// <param name="addDuration">Whether to add the duration to the current duration, if the effect is already active.</param>
    /// <remarks>A duration of 0 means that it will not expire.</remarks>
    public void EnableEffect<T>(byte intensity = 1, float duration = 0f, bool addDuration = false)
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.ChangeState<T>(intensity, duration, addDuration);

    /// <summary>
    /// Enables a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <param name="effect">The status effect to enable.</param>
    /// <param name="intensity">The intensity of the status effect.</param>
    /// <param name="duration">The duration of the status effect.</param>
    /// <param name="addDuration">Whether to add the duration to the current duration, if the effect is already active.</param>
    /// <remarks>A duration of 0 means that it will not expire.</remarks>
    public void EnableEffect(StatusEffectBase? effect, byte intensity = 1, float duration = 0f, bool addDuration = false) => effect?.ServerSetState(intensity, duration, addDuration);

    /// <summary>
    /// Tries to get a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <typeparam name="T">The specified effect that will be looked for.</typeparam>
    /// <param name="effect">The found player effect.</param>
    /// <returns>Whether the <see cref="StatusEffectBase">status effect</see> was successfully retrieved (And was cast successfully).</returns>
    public bool TryGetEffect<T>([NotNullWhen(true)] out T? effect)
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.TryGetEffect(out effect) && effect != null;

    /// <summary>
    /// Tries to get a specific <see cref="StatusEffectBase"/> based on its name.
    /// </summary>
    /// <param name="effectName">The name of the effect to get.</param>
    /// <param name="effect">The effect found.</param>
    /// <returns>Whether the <see cref="StatusEffectBase"/> was successfully found.</returns>
    public bool TryGetEffect(string effectName, [NotNullWhen(true)] out StatusEffectBase? effect)
        => ReferenceHub.playerEffectsController.TryGetEffect(effectName, out effect) && effect != null;

    /// <summary>
    /// Gets a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <typeparam name="T">The specified effect that will be looked for.</typeparam>
    /// <returns>The <see cref="StatusEffectBase"/> instance of <typeparamref name="T"/>, otherwise <see langword="null"/>.</returns>
    public T? GetEffect<T>()
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.GetEffect<T>();

    /// <summary>
    /// Redirects player connection to a target server port.
    /// </summary>
    /// <param name="port">The port of the target server.</param>
    public void RedirectToServer(ushort port) => Connection.Send(new RoundRestartMessage(RoundRestartType.RedirectRestart, 0.1f, port, true, false));

    /// <summary>
    /// Tells the player to reconnect to the server.
    /// </summary>
    /// <param name="delay">The delay before reconnecting.</param>
    /// <param name="isFastRestart">Whether fast restart is enabled.</param>
    public void Reconnect(float delay = 3f, bool isFastRestart = false) =>
        Connection.Send(new RoundRestartMessage(isFastRestart ? RoundRestartType.FastRestart : RoundRestartType.FullRestart, delay, 0, true, false));

    /// <summary>
    /// Kills the player.
    /// </summary>
    public void Kill() => Damage(new UniversalDamageHandler(StandardDamageHandler.KillValue, DeathTranslations.Unknown));

    /// <summary>
    /// Kills the player.
    /// </summary>
    /// <param name="reason">The reason for the kill.</param>
    /// <param name="cassieAnnouncement">The CASSIE announcement to make upon death.</param>
    /// <returns>Whether the player was successfully killed.</returns>
    public bool Kill(string reason, string cassieAnnouncement = "") => Damage(new CustomReasonDamageHandler(reason, StandardDamageHandler.KillValue, cassieAnnouncement));

    /// <summary>
    /// Damages player with a custom reason.
    /// </summary>
    /// <param name="amount">The amount of damage.</param>
    /// <param name="reason">The reason of damage.</param>
    /// <param name="cassieAnnouncement">The CASSIE announcement send after death.</param>
    /// <returns>Whether the player was successfully damaged.</returns>
    public bool Damage(float amount, string reason, string cassieAnnouncement = "") => Damage(new CustomReasonDamageHandler(reason, amount, cassieAnnouncement));

    /// <summary>
    /// Damages player with explosion force.
    /// </summary>
    /// <param name="amount">The amount of damage.</param>
    /// <param name="attacker">The player which attacked.</param>
    /// <param name="force">The force of explosion.</param>
    /// <param name="armorPenetration">The amount of armor penetration.</param>
    /// <returns>Whether the player was successfully damaged.</returns>
    public bool Damage(float amount, Player attacker, Vector3 force = default, int armorPenetration = 0) =>
        Damage(new ExplosionDamageHandler(new Footprint(attacker.ReferenceHub), force, amount, armorPenetration, ExplosionType.Grenade));

    /// <summary>
    /// Damages player.
    /// </summary>
    /// <param name="damageHandlerBase">The damage handler base.</param>
    /// <returns>Whether the player was successfully damaged.</returns>
    public bool Damage(DamageHandlerBase damageHandlerBase) => ReferenceHub.playerStats.DealDamage(damageHandlerBase);

    /// <summary>
    /// Bans the player from the server.
    /// </summary>
    /// <param name="issuer">The player that issued the ban.</param>
    /// <param name="reason">The reason of the ban.</param>
    /// <param name="duration">The duration of the ban in seconds.</param>
    /// <returns>Whether the player was successfully banned.</returns>
    public bool Ban(Player issuer, string reason, long duration) => Server.BanPlayer(this, issuer, reason, duration);

    /// <summary>
    /// Bans the player from the server.
    /// </summary>
    /// <param name="reason">The reason of the ban.</param>
    /// <param name="duration">The duration of the ban in seconds.</param>
    /// <returns>Whether the player was successfully banned.</returns>
    public bool Ban(string reason, long duration) => Server.BanPlayer(this, reason, duration);

    /// <summary>
    /// Kicks the player from the server.
    /// </summary>
    /// <param name="issuer">The player that issued the kick.</param>
    /// <param name="reason">The reason of the kick.</param>
    /// <returns>Whether the player was successfully kicked.</returns>
    public bool Kick(Player issuer, string reason) => Server.KickPlayer(this, issuer, reason);

    /// <summary>
    /// Kicks the player from the server.
    /// </summary>
    /// <param name="reason">The reason of the kick.</param>
    /// <returns>Whether the player was successfully kicked.</returns>
    public bool Kick(string reason) => Server.KickPlayer(this, reason);

    // TODO: EffectsManager, DamageManager, DataStorage?
    // DamageManager seems to have been unused previously. Also relies on DataStorage/SharedStorage

    /// <summary>
    /// Gets the <see cref="CustomDataStore"/> associated with the player, or creates a new one if it doesn't exist.
    /// </summary>
    /// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/>.</typeparam>
    /// <returns>The <see cref="CustomDataStore"/> associated with the player.</returns>
    public TStore GetDataStore<TStore>()
        where TStore : CustomDataStore
    {
        return CustomDataStore.GetOrAdd<TStore>(this);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[Player: DisplayName={DisplayName}, PlayerId={PlayerId}, NetworkId={NetworkId}, UserId={UserId}, IpAddress={IpAddress}, Role={Role}, IsHost={IsHost}, IsReady={IsReady}]";
    }
}
