using Generators;
using InventorySystem.Items.Pickups;
using Mirror;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Scp049.Zombies;
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
        Base = ragdoll;
        GameObject = ragdoll.gameObject;
        Transform = ragdoll.transform;
        Dictionary.TryAdd(ragdoll, this);
    }

    /// <summary>
    /// Gets the <see cref="BasicRagdoll"/> of the ragdoll.
    /// </summary>
    public BasicRagdoll Base { get; private set; }

    /// <summary>
    /// Gets the ragdoll's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject { get; }

    /// <summary>
    /// Gets the pickup's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform { get; }

    /// <summary>
    /// Gets whether the ragdoll was destroyed.
    /// </summary>
    public bool IsDestroyed => Base == null || GameObject == null;

    /// <summary>
    /// Gets or sets the role info of the ragdoll.
    /// <para>This does NOT change the ragdoll visually.</para>
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
    /// Gets or sets the position of the ragdoll.
    /// </summary>
    public Vector3 Position
    {
        get => Base.transform.position;
        set
        {
            Base.transform.position = value;
            Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, Base.NetworkInfo.Handler, Base.NetworkInfo.RoleType, value, Base.NetworkInfo.StartRotation, Base.NetworkInfo.Scale, Base.NetworkInfo.Nickname, Base.NetworkInfo.CreationTime);
        }
    }

    /// <summary>
    /// Gets or sets the rotation of the ragdoll.
    /// </summary>
    public Quaternion Rotation
    {
        get => Base.transform.rotation;
        set
        {
            Base.transform.rotation = value;
            Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, Base.NetworkInfo.Handler, Base.NetworkInfo.RoleType, Base.NetworkInfo.StartPosition, value, Base.NetworkInfo.Scale, Base.NetworkInfo.Nickname, Base.NetworkInfo.CreationTime);
        }
    }

    /// <summary>
    /// Gets or sets the ragdoll's scale.
    /// Scale is set relative to the ragdoll's gameobject size.
    /// </summary>
    public Vector3 Scale
    {
        get => Base.transform.localScale;
        set
        {
            Base.transform.localScale = value;
            Base.NetworkInfo = new RagdollData(Base.NetworkInfo.OwnerHub, Base.NetworkInfo.Handler, Base.NetworkInfo.RoleType, Base.NetworkInfo.StartPosition, Base.NetworkInfo.StartRotation, Vector3.Scale(value, RagdollManager.GetDefaultScale(Role)), Base.NetworkInfo.Nickname, Base.NetworkInfo.CreationTime);
        }
    }

    /// <summary>
    /// Gets or sets whether the corpse is consumed.
    /// </summary>
    public bool IsConsumed
    {
        get => ZombieConsumeAbility.ConsumedRagdolls.Contains(Base);
        set
        {
            if (value)
            {
                ZombieConsumeAbility.ConsumedRagdolls.Add(Base);
                return;
            }

            ZombieConsumeAbility.ConsumedRagdolls.Remove(Base);
        }
    }

    /// <summary>
    /// Gets whether the ragdoll is revivable by SCP-049 player.
    /// </summary>
    /// <param name="scp049">Player who is SCP-049</param>
    /// <returns>True if corpse is revivable. False if it isn't or specified player is not SCP-049.</returns>
    public bool IsRevivableBy(Player scp049)
    {
        if (scp049.RoleBase is not Scp049Role role)
            return false;

        if (!role.SubroutineModule.TryGetSubroutine(out Scp049ResurrectAbility ability))
            return false;

        return ability.CheckRagdoll(Base);
    }

    /// <summary>
    /// Destroys this ragdoll.
    /// </summary>
    public void Destroy()
    {
        NetworkServer.Destroy(Base.gameObject);
    }

    /// <summary>
    /// Forcefully freezes this ragdoll for all clients.
    /// </summary>
    public void Freeze() => Base.ClientFreezeRpc();

    /// <summary>
    /// Unfreezes this ragdoll by spawning a copy of it and destroying the original. Reference to the <see cref="Base"/> changes, but no other action is required if you are referencing this object. <br/>
    /// </summary>
    /// <remarks>
    /// Note that the position and rotation is set to the server one.
    /// </remarks>
    public void UnFreeze()
    {
        RagdollData data = Base.NetworkInfo;

        RagdollManager.OnRagdollSpawned -= RagdollSpawned;
        RagdollManager.OnRagdollRemoved -= RagdollRemoved;

        Destroy();
        Dictionary.Remove(Base);
        Base = RagdollManager.ServerCreateRagdoll(data.RoleType, data.StartPosition, data.StartRotation, data.Handler, data.Nickname, data.Scale, data.Serial);
        Dictionary.TryAdd(Base, this);

        RagdollManager.OnRagdollSpawned += RagdollSpawned;
        RagdollManager.OnRagdollRemoved += RagdollRemoved;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[Ragdoll: Nickname={Nickname}, Role={Role}, DamageHandler={DamageHandler}, Position={Position}, Rotation={Rotation}, Scale={Scale}, IsConsumed={IsConsumed}]";
    }

    /// <summary>
    /// Spawns a new ragdoll based on a specified player and damage handler.
    /// </summary>
    /// <param name="player">Player for ragdoll template.</param>
    /// <param name="handler">Handler that is shown as a death cause.</param>
    /// <returns>New ragdoll.</returns>
    public static Ragdoll? SpawnRagdoll(Player player, DamageHandlerBase handler) => SpawnRagdoll(player.Role, player.Position, player.Rotation, handler, player.DisplayName);

    /// <summary>
    /// Attempts to spawn a ragdoll from specified role. Ragdoll is not created if specified role doesn't have any ragdoll model available.
    /// </summary>
    /// <param name="role">Target role type.</param>
    /// <param name="position">Spawn position.</param>
    /// <param name="rotation">Spawn rotation.</param>
    /// <param name="scale">Spawn scale. Converted to base ragdoll scale if <see langword="null"/>.</param>
    /// <param name="handler">Damage handler of the death cause.</param>
    /// <param name="nickname">Nickname that is visible when hovering over.</param>
    /// <returns>Ragdoll object or <see langword="null"/>.</returns>
    public static Ragdoll? SpawnRagdoll(RoleTypeId role, Vector3 position, Quaternion rotation, DamageHandlerBase handler, string nickname, Vector3? scale = null)
    {
        BasicRagdoll ragdoll = RagdollManager.ServerCreateRagdoll(role, position, rotation, handler, nickname, scale);
        return ragdoll == null ? null : Get(ragdoll);
    }

    /// <summary>
    /// Initializes the <see cref="Ragdoll"/> class to subscribe to <see cref="RagdollManager"/> events and handle the ragdoll caching.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();

        RagdollManager.OnRagdollSpawned += RagdollSpawned;
        RagdollManager.OnRagdollRemoved += RagdollRemoved;
    }

    /// <summary>
    /// Event method for <see cref="RagdollManager.OnRagdollSpawned"/>.
    /// </summary>
    /// <param name="ragdoll">New ragdoll.</param>
    private static void RagdollSpawned(BasicRagdoll ragdoll) => _ = new Ragdoll(ragdoll).Base;

    /// <summary>
    /// Event method for <see cref="RagdollManager.OnRagdollRemoved"/>.
    /// </summary>
    /// <param name="ragdoll">Destroyed ragdoll.</param>
    private static void RagdollRemoved(BasicRagdoll ragdoll) => Dictionary.Remove(ragdoll);

    /// <summary>
    /// Gets the ragdoll wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="ragdoll">The ragdoll.</param>
    /// <returns>The requested ragdoll.</returns>
    public static Ragdoll Get(BasicRagdoll ragdoll) => Dictionary.TryGetValue(ragdoll, out Ragdoll rag) ? rag : new Ragdoll(ragdoll);

}