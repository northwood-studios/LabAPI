using CentralAuth;
using CommandSystem;
using CustomPlayerEffects;
using Footprinting;
using Generators;
using Hints;
using InventorySystem;
using InventorySystem.Disarming;
using InventorySystem.Items;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Pickups;
using MapGeneration;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
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

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ReferenceHub">reference hubs</see>, the in-game players.
/// </summary>
public class Player
{
    /// <summary>
    /// Contains all the cached players in the game, accessible through their <see cref="ReferenceHub"/>.
    /// </summary>
    public static Dictionary<ReferenceHub, Player> Dictionary { get; } = [];

    /// <summary>
    /// A cache of players by their User ID. Does not necessarily contain all players.
    /// </summary>
    private static readonly Dictionary<string, Player> UserIdCache = new(CustomNetworkManager.slots, StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// A reference to all <see cref="Player"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Player> List => Dictionary.Values;

    /// <summary>
    /// The <see cref="Player"/> representing the host or server.
    /// </summary>
    public static Player? Host { get; internal set; } // TODO: Implement this when we generate the map. Add it with Doors Cache, Rooms Cache, etc.

    /// <summary>
    /// Gets the amount of online players.
    /// </summary>
    public static int Count => ReferenceHub.AllHubs.Count(x => !x.isLocalPlayer && x.Mode == ClientInstanceMode.ReadyClient && !string.IsNullOrEmpty(x.authManager.UserId));

    /// <summary>
    /// Gets the amount of non-verified players
    /// </summary>
    public static int NonVerifiedCount => ConnectionsCount - Count;

    /// <summary>
    /// Gets the amount of connected players. Regardless of their authentication status.
    /// </summary>
    public static int ConnectionsCount => LiteNetLib4MirrorCore.Host.ConnectedPeersCount;

    /// <summary>
    /// Initializes the <see cref="Player"/> class to subscribe to <see cref="ReferenceHub"/> events and handle the player cache.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();
        UserIdCache.Clear();

        ReferenceHub.OnPlayerAdded += (hub) => _ = new Player(hub);
        ReferenceHub.OnPlayerRemoved += RemovePlayer;
    }

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    private Player(ReferenceHub referenceHub)
    {
        Dictionary.Add(referenceHub, this);
        ReferenceHub = referenceHub;
    }

    /// <summary>
    /// The <see cref="ReferenceHub">Reference Hub</see> of the player.
    /// </summary>
    public ReferenceHub ReferenceHub { get; }

    /// <summary>
    /// Gets the player's <see cref="GameObject"/>.
    /// </summary>
    public GameObject GameObject => ReferenceHub.gameObject;

    /// <summary>
    /// Gets whether the player is the host or server.
    /// </summary>
    public bool IsHost => ReferenceHub.IsHost;

    /// <summary>
    /// Gets whether the player is the dedicated server.
    /// </summary>
    public bool IsServer => ReferenceHub.isLocalPlayer;

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
    public NetworkConnection Connection => IsServer ? ReferenceHub.networkIdentity.connectionToServer : ReferenceHub.networkIdentity.connectionToClient;

    /// <summary>
    /// Gets the player's <see cref="RecyclablePlayerId"/> value.
    /// </summary>
    public int PlayerId => ReferenceHub.PlayerId;

    /// <summary>
    /// Gets if the player is currently offline.
    /// </summary>
    public bool IsOffline => GameObject == null;

    /// <summary>
    /// Gets if the player is currently online.
    /// </summary>
    public bool IsOnline => !IsOffline;

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
    public string LogName => IsServer ? "SERVER CONSOLE" : $"{Nickname} ({UserId})";

    /// <summary>
    /// Gets or sets the player's custom info.
    /// </summary>
    public string CustomInfo
    {
        get => ReferenceHub.nicknameSync.CustomPlayerInfo;
        set => ReferenceHub.nicknameSync.CustomPlayerInfo = value;
    }

    /// <summary>
    /// Gets or sets the player's current health;
    /// </summary>
    public float Health
    {
        get => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue = value;
    }

    /// <summary>
    /// Gets the player's current maximum health;
    /// </summary>
    // TODO: Add a setter for this property.
    public float MaxHealth => ReferenceHub.playerStats.GetModule<HealthStat>().MaxValue;

    /// <summary>
    /// Gets or sets the player's current artificial health;
    /// </summary>
    public float ArtificialHealth
    {
        get => IsSCP ? ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue : ReferenceHub.playerStats.GetModule<AhpStat>().CurValue;
        set
        {
            if (IsSCP)
            {
                ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue = value;
                return;
            }

            ReferenceHub.playerStats.GetModule<AhpStat>().CurValue = value;
        }
    }

    /// <summary>
    /// Gets the player's current maximum artificial health.
    /// </summary>
    // TODO: Possibly add a setter for this property?
    public float MaxArtificialHealth => IsSCP ? ReferenceHub.playerStats.GetModule<HumeShieldStat>().MaxValue : ReferenceHub.playerStats.GetModule<AhpStat>().MaxValue;

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
    /// Gets or sets the player's current <see cref="ItemBase">item</see>.
    /// </summary>
    public ItemBase CurrentItem
    {
        get => Inventory.CurInstance;
        set
        {
            if (value == null || value.ItemTypeId == ItemType.None)
                Inventory.ServerSelectItem(0);
            else
                Inventory.ServerSelectItem(value.ItemSerial);
        }
    }

    /// <summary>
    /// Gets the player's currently active <see cref="StatusEffectBase">status effects</see>.
    /// </summary>
    public IEnumerable<StatusEffectBase> ActiveEffects => ReferenceHub.playerEffectsController.AllEffects.Where(x => x.Intensity > 0);

    /// <summary>
    /// Gets the <see cref="RoomIdentifier"/> at the player's current position.
    /// </summary>
    public Room? Room => Room.GetRoomAtPosition(Position);

    /// <summary>
    /// Gets the <see cref="FacilityZone"/> for the player's current room. Returns <see cref="FacilityZone.None"/> if the room is null.
    /// </summary>
    public FacilityZone Zone => Room?.Zone ?? FacilityZone.None;

    /// <summary>
    /// Gets the <see cref="ItemBase">items</see> in the player's inventory.
    /// </summary>
    public IReadOnlyCollection<ItemBase> Items => Inventory.UserInventory.Items.Values;

    /// <summary>
    /// Gets the player's Reserve Ammo.
    /// </summary>
    public Dictionary<ItemType, ushort> Ammo => Inventory.UserInventory.ReserveAmmo;

    /// <summary>
    /// Gets or sets the player's role color.
    /// </summary>
    public string RoleColor
    {
        get => ReferenceHub.serverRoles.Network_myColor;
        set => ReferenceHub.serverRoles.SetColor(value);
    }

    /// <summary>
    /// Gets or sets the player's role name.
    /// </summary>
    public string RoleName
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
    /// Gets the player's group name. Or null if the player is not in a group.
    /// </summary>
    public string? GroupName => ServerStatic.GetPermissionsHandler()._members.GetValueOrDefault(UserId);

    /// <summary>
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
    public bool IsMuted => VoiceChatMutes.QueryLocalMute(UserId);

    /// <summary>
    /// Gets a value indicating whether the player is muted from the intercom.
    /// </summary>
    public bool IsIntercomMuted => VoiceChatMutes.QueryLocalMute(UserId, true);

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
            if (!IsDisarmed) return null;

            DisarmedPlayers.DisarmedEntry entry = DisarmedPlayers.Entries.Find(x => x.DisarmedPlayer == NetworkId);

            return Get(entry.Disarmer);
        }
        set
        {
            Inventory.SetDisarmedStatus(null);

            if (value == null)
                return;

            DisarmedPlayers.Entries.Add(new DisarmedPlayers.DisarmedEntry(NetworkId, value.NetworkId));
            new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
        }
    }

    /// <summary>
    /// Gets the player's current <see cref="Team"/>.
    /// </summary>
    public Team Team => RoleBase.Team;

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
    /// Gets or sets the player's position.
    /// </summary>
    public Vector3 Position
    {
        get => GameObject.transform.position;
        set => ReferenceHub.TryOverridePosition(value, Vector3.zero);
    }

    /// <summary>
    /// Gets or sets the player's rotation.
    /// </summary>
    public Quaternion Rotation
    {
        get => GameObject.transform.rotation;
        set => ReferenceHub.TryOverridePosition(Position, value.eulerAngles);
    }

    /// <summary>
    /// Gets or sets player's remaining stamina (min = 0, max = 1).
    /// </summary>
    public float StaminaRemaining
    {
        get => ReferenceHub.playerStats.GetModule<StaminaStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<StaminaStat>().CurValue = value;
    }

    #region Player Getters

    /// <summary>
    /// Gets the player wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    /// <returns>The requested player or null if the reference hub is null.</returns>
    [return: NotNullIfNotNull("referenceHub")]
    public static Player? Get(ReferenceHub? referenceHub)
    {
        if (referenceHub == null)
            return null;

        return Dictionary.TryGetValue(referenceHub, out Player player) ? player : new Player(referenceHub);
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
    public static List<Player> GetNonAlloc(IEnumerable<ReferenceHub> referenceHubs, List<Player> list)
    {
        // We clear the list to avoid any previous data.
        list.Clear();
        // And then we add all the players to the list.
        list.AddRange(referenceHubs.Select(Get));
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
            return false;

        if (!ReferenceHub.TryGetHub(gameObject, out ReferenceHub? hub))
            return false;

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
            return false;

        if (!TryGet(identity.netId, out player))
            return false;

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
            return false;

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
            return false;

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
            return false;

        if (UserIdCache.TryGetValue(userId!, out player) && player.IsOnline)
            return true;

        player = List.FirstOrDefault(x => x.UserId == userId);
        if (player == null)
            return false;

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
    /// Tries to get players by name by seeing if their name starts with the input.
    /// </summary>
    /// <param name="input">The input to search for.</param>
    /// <param name="players">The output players if found.</param>
    /// <returns>True if the players are found, false otherwise.</returns>
    public static bool TryGetPlayersByName(string input, out List<Player> players)
    {
        players = GetNonAlloc(ReferenceHub.AllHubs.Where(x => x.nicknameSync.Network_myNickSync.StartsWith(input, StringComparison.OrdinalIgnoreCase)),
            ListPool<Player>.Shared.Rent());

        return players.Count > 0;
    }

    #endregion

    #endregion

    /// <summary>
    /// Clears displayed broadcast(s).
    /// </summary>
    // TODO: Maybe use Server Wrapper
    public void ClearBroadcasts() => Broadcast.Singleton.TargetClearElements(ReferenceHub.characterClassManager.connectionToClient);

    /// <summary>
    /// Sends a broadcast to the player.
    /// </summary>
    /// <param name="message">The message to be broadcast.</param>
    /// <param name="duration">The broadcast duration.</param>
    /// <param name="type">The broadcast type.</param>
    /// <param name="shouldClearPrevious">Whether it should clear previous broadcasts.</param>
    // TODO: Maybe use Server Wrapper
    public void SendBroadcast(string message, ushort duration, Broadcast.BroadcastFlags type = Broadcast.BroadcastFlags.Normal, bool shouldClearPrevious = false)
    {
        if (shouldClearPrevious)
            ClearBroadcasts();

        Broadcast.Singleton.TargetAddElement(ReferenceHub.characterClassManager.connectionToClient, message, duration, type);
    }

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
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) | VcMuteFlags.LocalRegular);
        else
            VoiceChatMutes.IssueLocalMute(UserId);
    }

    /// <summary>
    /// Revokes a mute from a player, allowing them to speak again.
    /// </summary>
    /// <param name="revokeMute">If set to true, this player's <see cref="UserId"/> will be removed from the mute file.</param>
    public void Unmute(bool revokeMute)
    {
        if (revokeMute)
            VoiceChatMutes.RevokeLocalMute(UserId);
        else
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) & ~VcMuteFlags.LocalRegular);
    }

    /// <summary>
    /// Issue a mute to a player, preventing them from speaking through the intercom.
    /// </summary>
    /// <param name="isTemporary">Whether the mute is temporary, or should be added to the mute file.</param>
    public void IntercomMute(bool isTemporary = false)
    {
        if (isTemporary)
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) | VcMuteFlags.LocalIntercom);
        else
            VoiceChatMutes.IssueLocalMute(UserId, true);
    }

    /// <summary>
    /// Revokes a mute from a player, allowing them to speak through the intercom again.
    /// </summary>
    /// <param name="revokeMute">If set to true, this player's <see cref="UserId"/> will be removed from the mute file.</param>
    public void IntercomUnmute(bool revokeMute)
    {
        if (revokeMute)
            VoiceChatMutes.RevokeLocalMute(UserId, true);
        else
            VoiceChatMutes.SetFlags(ReferenceHub, VoiceChatMutes.GetFlags(ReferenceHub) & ~VcMuteFlags.LocalIntercom);
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
    public void AddItem(ItemType item) => Inventory.ServerAddItem(item, ItemAddReason.AdminCommand);

    /// <summary>
    /// Removes a specific <see cref="Item"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public void RemoveItem(Item item) => RemoveItem(item.Base);

    /// <summary>
    /// Removes a specific <see cref="ItemBase"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public void RemoveItem(ItemBase item) => Inventory.ServerRemoveItem(item.ItemSerial, item.PickupDropModel);

    /// <summary>
    /// Removes a specific <see cref="Pickup"/> from the player's inventory.
    /// </summary>
    /// <param name="pickup">The pickup to remove.</param>
    public void RemoveItem(Pickup pickup) => RemoveItem(pickup.ItemPickupBase);

    /// <summary>
    /// Removes a specific <see cref="ItemPickupBase"/> from the player's inventory.
    /// </summary>
    /// <param name="pickup">The pickup to remove.</param>
    public void RemoveItem(ItemPickupBase pickup) => Inventory.ServerRemoveItem(pickup.Info.Serial, pickup);

    /// <summary>
    /// Removes all items of the specified type from the player's inventory.
    /// </summary>
    /// <param name="item">The type of item.</param>
    /// <param name="maxAmount">The maximum amount of items to remove.</param>
    public void RemoveItem(ItemType item, int maxAmount = 1)
    {
        int count = 0;
        for (int i = 0; i < Items.Count; i++)
        {
            ItemBase? itemBase = Items.ElementAt(i);
            if (itemBase.ItemTypeId != item)
                continue;

            RemoveItem(itemBase);
            if (++count >= maxAmount)
                break;
        }
    }

    /// <summary>
    /// Drops the specified <see cref="Item"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>The dropped <see cref="ItemPickupBase">item pickup</see>.</returns>
    public ItemPickupBase DropItem(Item item) => DropItem(item.Base);

    /// <summary>
    /// Drops the specified <see cref="ItemBase"/> from the player's inventory.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>The dropped <see cref="ItemPickupBase">item pickup</see>.</returns>
    public ItemPickupBase DropItem(ItemBase item) => DropItem(item.ItemSerial);

    /// <summary>
    /// Drops the item with the specified serial from the player's inventory.
    /// </summary>
    /// <param name="serial">The serial of the item.</param>
    /// <returns>The dropped <see cref="ItemPickupBase">item pickup</see>.</returns>
    public ItemPickupBase DropItem(ushort serial) => Inventory.ServerDropItem(serial);

    /// <summary>
    /// Drops all items from the player's inventory.
    /// </summary>
    /// <returns>The list of dropped items.</returns>
    public List<ItemPickupBase> DropAllItems()
    {
        List<ItemPickupBase> items = ListPool<ItemPickupBase>.Shared.Rent();
        foreach (ItemBase item in Items)
            items.Add(DropItem(item));

        return items;
    }

    /// <summary>
    /// Sets the ammo amount of a specific ammo type.
    /// </summary>
    /// <param name="item">The type of ammo</param>
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
    /// <returns>The list of dropped ammo.</returns>
    public List<AmmoPickup> DropAmmo(ItemType item, ushort amount, bool checkMinimals = true) => Inventory.ServerDropAmmo(item, amount, checkMinimals);

    /// <summary>
    /// Drops all ammo from the player's inventory.
    /// </summary>
    /// <returns>The list of dropped ammo.</returns>
    public List<AmmoPickup> DropAllAmmo()
    {
        List<AmmoPickup> ammo = ListPool<AmmoPickup>.Shared.Rent();
        foreach (KeyValuePair<ItemType, ushort> pair in Ammo)
            ammo.AddRange(DropAmmo(pair.Key, pair.Value));

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
        foreach (ItemBase item in Items)
            RemoveItem(item);
    }

    /// <summary>
    /// Clear Ammo from the player's inventory.
    /// </summary>
    public void ClearAmmo()
    {
        if (Ammo.Any(x => x.Value > 0))
            Inventory.SendAmmoNextFrame = true;

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
            ClearAmmo();

        if (clearItems)
            ClearItems();
    }

    /// <summary>
    /// Heals the player by the specified amount.
    /// </summary>
    /// <param name="amount">The amount to heal.</param>
    public void Heal(float amount) => ReferenceHub.playerStats.GetModule<HealthStat>().ServerHeal(amount);

    /// <summary>
    /// Sets the player's role.
    /// </summary>
    /// <param name="newRole">The <see cref="RoleTypeId"/> which will be set.</param>
    /// <param name="reason">The <see cref="RoleChangeReason"/> of role change.</param>
    /// <param name="flags">The <see cref="RoleSpawnFlags"/> of role change.</param>
    public void SetRole(RoleTypeId newRole, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags flags = RoleSpawnFlags.All) => ReferenceHub.roleManager.ServerSetRole(newRole, reason, flags);

    /// <summary>
    /// Disconnects the player from the server.
    /// </summary>
    /// <param name="reason">The reason for the disconnection.</param>
    public void Disconnect(string? reason = null) => ServerConsole.Disconnect(GameObject, reason ?? string.Empty);

    /// <summary>
    /// Sends the player a hint text.
    /// </summary>
    /// <param name="text">The text which will be displayed.</param>
    /// <param name="duration">The duration of which the text will be visible.</param>
    public void ReceiveHint(string text, float duration = 3f) => ReferenceHub.hints.Show(new TextHint(text, new HintParameter[] { new StringHintParameter(text) }, null, duration));

    /// <summary>
    /// Sends the player a hint text with effects.
    /// </summary>
    /// <param name="text">The text which will be displayed.</param>
    /// <param name="effects">The effects of text.</param>
    /// <param name="duration">The duration of which the text will be visible.</param>
    public void ReceiveHint(string text, HintEffect[] effects, float duration = 3f) =>
        ReferenceHub.hints.Show(new TextHint(text, new HintParameter[] { new StringHintParameter(text) }, effects, duration));

    /// <summary>
    /// Sends the player a hit marker.
    /// </summary>
    /// <param name="size">The size of hit marker.</param>
    public void ReceiveHitMarker(float size = 1f) => Hitmarker.SendHitmarkerDirectly(Connection, size);

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
    /// <param name="duration">The duration of the status effect.</param>
    /// <param name="intensity">The intensity of the status effect.</param>
    /// <param name="addDuration">Whether to add the duration to the current duration, if the effect is already active.</param>
    /// <remarks>A duration of 0 means that it will not expire.</remarks>
    public void EnableEffect<T>(byte intensity = 1, float duration = 0f, bool addDuration = false)
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.ChangeState<T>(intensity, duration, addDuration);

    /// <summary>
    /// Enables a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <param name="effect">The status effect to enable.</param>
    /// <param name="duration">The duration of the status effect.</param>
    /// <param name="intensity">The intensity of the status effect.</param>
    /// <param name="addDuration">Whether to add the duration to the current duration, if the effect is already active.</param>
    /// <remarks>A duration of 0 means that it will not expire.</remarks>
    public void EnableEffect(StatusEffectBase? effect, byte intensity = 1, float duration = 0f, bool addDuration = false) => effect?.ServerSetState(intensity, duration, addDuration);

    /// <summary>
    /// Tries to get a specific <see cref="StatusEffectBase">status effect</see>.
    /// </summary>
    /// <typeparam name="T">The specified effect that will be looked for.</typeparam>
    /// <param name="effect">The found player effect.</param>
    /// <returns>Whether the <see cref="StatusEffectBase">status effect</see> was successfully retrieved. (And was cast successfully)</returns>
    public bool TryGetEffect<T>([NotNullWhen(true)] out T? effect)
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.TryGetEffect(out effect) && effect != null;

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
    /// <param name="reason">The reason for the kill</param>
    /// <param name="cassieAnnouncement">The cassie announcement to make upon death.</param>
    /// <returns>Whether the player was successfully killed.</returns>
    public bool Kill(string reason, string cassieAnnouncement = "") => Damage(new CustomReasonDamageHandler(reason, StandardDamageHandler.KillValue, cassieAnnouncement));

    /// <summary>
    /// Damages player with a custom reason.
    /// </summary>
    /// <param name="amount">The amount of damage.</param>
    /// <param name="reason">The reason of damage.</param>
    /// <param name="cassieAnnouncement">The cassie announcement send after death.</param>
    /// <returns>Whether the player was successfully damaged.</returns>
    public bool Damage(float amount, string reason, string cassieAnnouncement = "") => Damage(new CustomReasonDamageHandler(reason, amount, cassieAnnouncement));

    /// <summary>
    /// Damages player with explosion force.
    /// </summary>
    /// <param name="amount">The amount of damage.</param>
    /// <param name="attacker">The player which attacked</param>
    /// <param name="force">The force of explosion.</param>
    /// <param name="armorPenetration">The amount of armor penetration.</param>
    /// <returns>Whether the player was successfully damaged.</returns>
    public bool Damage(float amount, Player attacker, Vector3 force = default, int armorPenetration = 0) =>
        Damage(new ExplosionDamageHandler(new Footprint(attacker.ReferenceHub), force, amount, armorPenetration));

    /// <summary>
    /// Damages player.
    /// </summary>
    /// <param name="damageHandlerBase">The damage handler base.</param>
    /// <returns>Whether the player was successfully damaged.</returns>
    public bool Damage(DamageHandlerBase damageHandlerBase) => ReferenceHub.playerStats.DealDamage(damageHandlerBase);

    /// <summary>
    /// Bans the player from the server.
    /// </summary>
    /// <param name="issuer">The player which issued ban.</param>
    /// <param name="reason">The reason of ban.</param>
    /// <param name="duration">The duration of ban in seconds.</param>
    /// <returns>Whether the player was successfully banned.</returns>
    // TODO: Use the Server Wrapper
    public bool Ban(Player issuer, string reason, long duration) => BanPlayer.BanUser(ReferenceHub, issuer.ReferenceHub, reason, duration);

    /// <summary>
    /// Bans the player from the server.
    /// </summary>
    /// <param name="reason">The reason of ban.</param>
    /// <param name="duration">The duration of ban in seconds.</param>
    /// <returns>Whether the player was successfully banned.</returns>
    // TODO: Use the Server Wrapper
    public bool Ban(string reason, long duration) => BanPlayer.BanUser(ReferenceHub, reason, duration);

    /// <summary>
    /// Kicks the player from the server.
    /// </summary>
    /// <param name="issuer">The player which issued kick.</param>
    /// <param name="reason">The reason of kick.</param>
    /// <returns>Whether the player was successfully kicked.</returns>
    // TODO: Use the Server Wrapper
    public bool Kick(Player issuer, string reason) => BanPlayer.KickUser(ReferenceHub, issuer.ReferenceHub, reason);

    /// <summary>
    /// Kicks the player from the server.
    /// </summary>
    /// <param name="reason">The reason of kick.</param>
    /// <returns>Whether the player was successfully kicked.</returns>
    // TODO: Use the Server Wrapper
    public bool Kick(string reason) => BanPlayer.KickUser(ReferenceHub, reason);

    // TODO: EffectsManager, DamageManager, DataStorage?
    // DamageManager seems to have been unused previously. Also relies on DataStorage/SharedStorage

    /// <summary>
    /// Handles the removal of a player from the server.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    private static void RemovePlayer(ReferenceHub referenceHub)
    {
        if (referenceHub.authManager.UserId != null)
            UserIdCache.Remove(referenceHub.authManager.UserId);

        Dictionary.Remove(referenceHub);
    }
}