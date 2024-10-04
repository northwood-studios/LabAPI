using LabApi.Loader.Features.Misc;
using Mirror;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.Ragdolls;
using PlayerStatsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BasicRagdoll">basic ragdolls</see>.
/// </summary>
public class Ragdoll
{
    /// <summary>
    /// Contains all the cached ragdolls in the game, accessible through their <see cref="BasicRagdoll"/>.
    /// </summary>
    public static Dictionary<BasicRagdoll, Ragdoll> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Ragdoll"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Ragdoll> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="ragdoll">The ragdoll component.</param>
    private Ragdoll(BasicRagdoll ragdoll)
    {
        Dictionary.Add(ragdoll, this);
        Base = ragdoll;
    }

    /// <summary>
    /// Gets the <see cref="BasicRagdoll"/> of the ragdoll.
    /// </summary>
    public BasicRagdoll Base { get; }

    /// <summary>
    /// Gets or sets the role info of the ragdoll
    /// <para>This does NOT change the ragdoll visually</para>
    /// </summary>
    public RoleTypeId Role
    {
        get => Base.NetworkInfo.RoleType;
        set => Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, Base.NetworkInfo.Handler, value, Base.NetworkInfo.StartPosition, Base.NetworkInfo.StartRotation, Base.NetworkInfo.Nickname, Base.NetworkInfo.CreationTime);
    }

    /// <summary>
    /// Gets or sets the ragdoll nickname.
    /// </summary>
    public string Nickname
    {
        get => Base.NetworkInfo.Nickname;
        set => Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, Base.NetworkInfo.Handler, Base.NetworkInfo.RoleType, Base.NetworkInfo.StartPosition, Base.NetworkInfo.StartRotation, value, Base.NetworkInfo.CreationTime);
    }

    /// <summary>
    /// Gets or sets the ragdoll damage handler, providing death cause.
    /// </summary>
    //TODO: Damage handler wrapper
    public DamageHandlerBase DamageHandler
    {
        get => Base.NetworkInfo.Handler;
        set => Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, value, Base.NetworkInfo.RoleType, Base.NetworkInfo.StartPosition, Base.NetworkInfo.StartRotation, Base.NetworkInfo.Nickname, Base.NetworkInfo.CreationTime);
    }

    /// <summary>
    /// Gets or sets position of the ragdoll.
    /// </summary>
    public Vector3 Position
    {
        get => Base.transform.position;
        set
        {
            Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, Base.NetworkInfo.Handler, Base.NetworkInfo.RoleType, value, Base.NetworkInfo.StartRotation, Base.NetworkInfo.Nickname, Base.NetworkInfo.CreationTime);
            Base.transform.position = value;
        }
    }

    /// <summary>
    /// Gets or sets rotation of the ragdoll.
    /// </summary>
    public Quaternion Rotation
    {
        get => Base.transform.rotation;
        set
        {
            Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, Base.NetworkInfo.Handler, Base.NetworkInfo.RoleType, Base.NetworkInfo.StartPosition, value, Base.NetworkInfo.Nickname, Base.NetworkInfo.CreationTime);
            Base.transform.rotation = value;
        }
    }

    /// <summary>
    /// Destroys this ragdoll.
    /// </summary>
    public void Destroy()
    {
        NetworkServer.Destroy(Base.gameObject);
    }

    /// <summary>
    /// Spawns a new ragdoll based on a specified player and damage handler.
    /// </summary>
    /// <param name="player">Player for ragdoll template</param>
    /// <param name="handler">Handler that is shown as death cause</param>
    /// <returns>New ragdoll</returns>
    public static Ragdoll SpawnRagdoll(Player player, DamageHandlerBase handler)
    {
        return Get(RagdollManager.ServerSpawnRagdoll(player.ReferenceHub, handler));
    }


    /// <summary>
    /// Spawns a new ragdoll for the specified role at position. May return null if specified role doesn't have ragdoll.
    /// </summary>
    /// <param name="role">Target role of the ragdoll</param>
    /// <param name="position">Position at which </param>
    /// <param name="nickname">Nickname of the ragdoll</param>
    /// <param name="handler">Handler that is displayed as reason of death</param>
    /// <returns>New ragdoll or null</returns>
    public static Ragdoll SpawnRagdoll(RoleTypeId role, Vector3 position, string nickname = "", DamageHandlerBase handler = null)
    {
        //We use host hub to change his role to the target & use it as a template
        ReferenceHub.HostHub.roleManager.ServerSetRole(role, RoleChangeReason.RemoteAdmin, RoleSpawnFlags.None);

        //Check for roles that may not have ragdoll templates
        if (ReferenceHub.HostHub.roleManager.CurrentRole is not FpcStandardRoleBase humanRole) return null;

        //Set the position
        humanRole.FpcModule.ServerOverridePosition(position, Vector3.zero);

        //Spawn the ragdoll
        BasicRagdoll ragdoll = RagdollManager.ServerSpawnRagdoll(ReferenceHub.HostHub, handler);

        //Set role of the hosthub back to none
        ReferenceHub.HostHub.roleManager.ServerSetRole(RoleTypeId.None, RoleChangeReason.RemoteAdmin, RoleSpawnFlags.All);

        //set the ragdoll info
        ragdoll.Info = new RagdollData(ReferenceHub.HostHub, ragdoll.Info.Handler, role, position, ragdoll.Info.StartRotation, nickname, NetworkTime.time);
        return Get(ragdoll);
    }

    /// <summary>
    /// Initializes the <see cref="Ragdoll"/> class to subscribe to <see cref="RagdollManager"/> events and handle the ragdoll caching.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();

        RagdollManager.OnRagdollSpawned += (ragdoll) => new Ragdoll(ragdoll);
        RagdollManager.OnRagdollRemoved += (ragdoll) => Dictionary.Remove(ragdoll);
    }

    /// <summary>
    /// Gets the ragdoll wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="ragdoll">The ragdoll.</param>
    /// <returns>The requested ragdoll.</returns>
    public static Ragdoll Get(BasicRagdoll ragdoll) => Dictionary.TryGetValue(ragdoll, out Ragdoll rag) ? rag : new Ragdoll(ragdoll);
}