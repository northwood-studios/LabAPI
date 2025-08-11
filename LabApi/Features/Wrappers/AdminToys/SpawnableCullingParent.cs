using AdminToys;
using Generators;
using Mirror;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static LabApi.Features.Wrappers.AdminToy;
using BaseCullingParent = AdminToys.SpawnableCullingParent;
using Logger = LabApi.Features.Console.Logger;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for <see cref="BaseCullingParent"/>.<br/>
/// Cullable item that deactivates itself when not being looked at.
/// Can contain children admin toys to cull them.
/// </summary>
/// <remarks>
/// This class is <b>not</b> subclass of <see cref="AdminToy"/>.
/// </remarks>
public class SpawnableCullingParent
{
    /// <summary>
    /// Contains all the cached spawnable culling parents, accessible through their <see cref="Base"/>.
    /// </summary>
    public static Dictionary<BaseCullingParent, SpawnableCullingParent> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="SpawnableCullingParent"/>.
    /// </summary>
    public static IReadOnlyCollection<SpawnableCullingParent> List => Dictionary.Values;

    /// <summary>
    /// Instantiates a new culling parent object.
    /// </summary>
    /// <param name="position">The initial position.</param>
    /// <param name="size">The bounds size.</param>
    /// <param name="networkSpawn">Whether should the game object spawn over network.</param>
    /// <returns>The instantiated culling parent.</returns>
    public static SpawnableCullingParent Create(Vector3 position, Vector3 size, bool networkSpawn = true)
    {
        if (PrefabCache<BaseCullingParent>.Prefab == null)
        {
            BaseCullingParent? found = null;
            foreach (GameObject prefab in NetworkClient.prefabs.Values)
            {
                if (prefab.TryGetComponent(out found))
                {
                    break;
                }
            }

            if (found == null)
            {
                throw new InvalidOperationException($"No prefab in NetworkClient.prefabs has component type {typeof(BaseCullingParent)}");
            }

            PrefabCache<BaseCullingParent>.Prefab = found;
        }

        BaseCullingParent instance = UnityEngine.Object.Instantiate(PrefabCache<BaseCullingParent>.Prefab);
        instance.BoundsPosition = position;
        instance.BoundsSize = size;

        if (networkSpawn)
        {
            NetworkServer.Spawn(instance.gameObject);
        }

        return Get(instance);
    }

    /// <summary>
    /// Gets the cullable parent wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="AdminToyBase"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="cullingBase">The <see cref="Base"/> of the cullable parent.</param>
    /// <returns>The requested culling parent or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(cullingBase))]
    public static SpawnableCullingParent? Get(BaseCullingParent? cullingBase)
    {
        if (cullingBase == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(cullingBase, out SpawnableCullingParent item) ? item : new SpawnableCullingParent(cullingBase);
    }

    /// <summary>
    /// Tries to get the culling parent wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="adminToyBase">The <see cref="Base"/> of the cullable parent.</param>
    /// <param name="adminToy">The requested culling parent.</param>
    /// <returns><see langword="True"/> if the culling parent exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(BaseCullingParent? adminToyBase, [NotNullWhen(true)] out SpawnableCullingParent? adminToy)
    {
        adminToy = Get(adminToyBase);
        return adminToy != null;
    }

    /// <summary>
    /// Initializes the <see cref="SpawnableCullingParent"/> class.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        BaseCullingParent.OnAdded += AddCullableParent;
        BaseCullingParent.OnRemoved += RemoveCullableParent;
    }

    /// <summary>
    /// A private method to handle the creation of new cullable parents on the server.
    /// </summary>
    /// <param name="cullableParent">The created <see cref="BaseCullingParent"/> instance.</param>
    private static void AddCullableParent(BaseCullingParent cullableParent)
    {
        try
        {
            if (!Dictionary.ContainsKey(cullableParent))
            {
                _ = new SpawnableCullingParent(cullableParent);
            }
        }
        catch (Exception e)
        {
            Logger.InternalError($"Failed to handle admin toy creation with error: {e}");
        }
    }

    /// <summary>
    /// A private method to handle the removal of cullable parents from the server.
    /// </summary>
    /// <param name="cullableParent">The to be destroyed <see cref="BaseCullingParent"/> instance.</param>
    private static void RemoveCullableParent(BaseCullingParent cullableParent)
    {
        try
        {
            Dictionary.Remove(cullableParent);
        }
        catch (Exception e)
        {
            Logger.InternalError($"Failed to handle cullable parent destruction with error: {e}");
        }
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="cullingBase">The base object.</param>
    protected SpawnableCullingParent(BaseCullingParent cullingBase)
    {
        Dictionary.Add(cullingBase, this);
        Base = cullingBase;
    }

    /// <summary>
    /// The <see cref="BaseCullingParent">base</see> object.
    /// </summary>
    public BaseCullingParent Base { get; }

    /// <summary>
    /// The <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// The culling parent's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Whether the <see cref="Base"/> was destroyed.
    /// </summary>
    /// <remarks>
    /// A destroyed object may not be used.
    /// </remarks>
    public bool IsDestroyed => Base == null;

    /// <summary>
    /// Gets or sets the position of the culling parent.
    /// </summary>
    public Vector3 Position
    {
        get => Base.BoundsPosition;
        set => Base.BoundsPosition = value;
    }

    /// <summary>
    /// Gets or sets the culling bound size of the culling parent.
    /// </summary>
    public Vector3 Size
    {
        get => Base.BoundsSize;
        set => Base.BoundsSize = value;
    }

    /// <summary>
    /// Spawns the culling parent on client.
    /// </summary>
    public void Spawn() => NetworkServer.Spawn(GameObject);

    /// <summary>
    /// Destroys the culling parent on server and client.
    /// </summary>
    public void Destroy() => NetworkServer.Destroy(GameObject);
}