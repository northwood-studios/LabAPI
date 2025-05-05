using AdminToys;
using Generators;
using Mirror;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="AdminToyBase"/> class.
/// </summary>
public class AdminToy
{
    /// <summary>
    /// Initializes the <see cref="AdminToy"/> class.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        AdminToyBase.OnAdded += AddAdminToy;
        AdminToyBase.OnRemoved += RemoveAdminToy;

        Register<AdminToys.PrimitiveObjectToy>(x => new PrimitiveObjectToy(x));
        Register<AdminToys.LightSourceToy>(x => new LightSourceToy(x));
        Register<ShootingTarget>(x => new ShootingTargetToy(x));
        Register<AdminToys.SpeakerToy>(x => new SpeakerToy(x));
        Register<InvisibleInteractableToy>(x => new InteractableToy(x));
        Register<Scp079CameraToy>(x => new CameraToy(x));
    }

    /// <summary>
    /// Contains all the handlers for constructing wrappers for the associated base game types.
    /// </summary>
    private static readonly Dictionary<Type, Func<AdminToyBase, AdminToy?>> typeWrappers = [];

    /// <summary>
    /// Contains all the cached admin toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public static Dictionary<AdminToyBase, AdminToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="AdminToy"/>.
    /// </summary>
    public static IReadOnlyCollection<AdminToy> List => Dictionary.Values;

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="adminToyBase">The base object.</param>
    protected AdminToy(AdminToyBase adminToyBase)
    {
        Dictionary.Add(adminToyBase, this);
        Base = adminToyBase;
    }
    
    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal virtual void OnRemove()
    {
    }

    /// <summary>
    /// The <see cref="AdminToyBase">base</see> object.
    /// </summary>
    public AdminToyBase Base { get; }

    /// <summary>
    /// The <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// The admin toys <see cref="UnityEngine.Transform"/>.
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
    /// Gets or sets the local position of the admin toy.
    /// Position is relative to its parent if it has one, otherwise its the world position.
    /// </summary>
    /// <remarks>
    /// If <see cref="IsStatic"/> is true client wont update its position.
    /// </remarks>
    public Vector3 Position
    {
        get => Transform.localPosition;
        set => Transform.localPosition = value;
    }

    /// <summary>
    /// Gets or sets the local rotation of the admin toy.
    /// Rotation is relative to its parent if it has one, otherwise its the world rotation.
    /// </summary>
    /// <remarks>
    /// If <see cref="IsStatic"/> is true client wont update its rotation.
    /// </remarks>
    public Quaternion Rotation
    {
        get => Transform.localRotation;
        set => Transform.localRotation = value;
    }

    /// <summary>
    /// Gets or sets the local scale of the admin toy.
    /// Scale is relative to its parent if it has one, otherwise its the world scale.
    /// </summary>
    /// <remarks>
    /// If <see cref="IsStatic"/> is true client wont update its scale.
    /// </remarks>
    public Vector3 Scale
    {
        get => Transform.localScale;
        set => Transform.localScale = value;
    }

    /// <summary>
    /// Gets or sets the parent of the admin toy.
    /// </summary>
    /// <remarks>
    /// If the parent object contains a <see cref="NetworkIdentity"/> component and has been <see cref="NetworkServer.Spawn(GameObject, GameObject)"/> the parent is synced with the client.
    /// <para>
    /// Can be used even if <see cref="IsStatic"/> is true.
    /// When changing parent the toys relative <see cref="Position"/>, <see cref="Rotation"/> and <see cref="Scale"/> are retained.
    /// Note that if the parent has <see cref="NetworkServer.Destroy"/> called on it this object automatically has <see cref="NetworkServer.Destroy"/> called on itself. 
    /// To prevent destruction make sure you unparent it before that happens.
    /// </para>
    /// </remarks>
    public Transform? Parent
    {
        get => Transform.parent;
        set => Transform.SetParent(value, false);
    }

    /// <summary>
    /// Gets or sets the movement smoothing value.
    /// </summary>
    /// <remarks>
    /// Smooths the transitions between positions, rotations and scales.
    /// Higher values means more smoothing.
    /// 0 means no movement smoothing so it will snap between positions, rotations and scales.
    /// Smoothing does not work with parenting changes so changing parent will always cause a teleport.
    /// </remarks>
    public byte MovementSmoothing
    {
        get => (byte)(Base.MovementSmoothing == 0 ? 0 : 256 - Base.MovementSmoothing);
        set => Base.NetworkMovementSmoothing = (byte)(value == 0 ? 0 : 256 - value);
    }

    /// <summary>
    /// Gets or sets whether the admin toy is static.
    /// This should be enabled on as many toys possible to increase performance.
    /// Static is only applies to the local transformations so parenting to something that moves will still causes it to move while retaining the performance boost. 
    /// </summary>
    /// <remarks>
    /// A static admin toy will not process <see cref="Position"/>, <see cref="Rotation"/> or <see cref="Scale"/> on both server and client drastically increasing performance.
    /// <see cref="Parent"/> can still be used even if static is true.
    /// </remarks>
    public bool IsStatic
    {
        get => Base.IsStatic;
        set => Base.NetworkIsStatic = value;
    }

    /// <summary>
    /// Time interval in seconds for sending updated values to the client.
    /// 0 means update every frame while 0.5 means update every 500ms.
    /// Lower values increase network usage but mean the client receives the most up to date state from the server.
    /// </summary>
    public float SyncInterval
    {
        get => Base.syncInterval;
        set => Base.syncInterval = value;
    }

    /// <summary>
    /// Spawns the toy on the client.
    /// </summary>
    /// <remarks>
    /// Spawn wont cascade to children toy objects, so if they are not spawned you have to call spawn on all of them.
    /// </remarks>
    public void Spawn() => NetworkServer.Spawn(GameObject);

    /// <summary>
    /// Destroys the toy on server and client.
    /// </summary>
    /// <remarks>
    /// Cascades to all children toy objects, be sure to unparent children if you don't intend to destroy them.
    /// </remarks>
    public void Destroy() => NetworkServer.Destroy(GameObject);

    /// <summary>
    /// Instantiates a new base game admin toy object.
    /// </summary>
    /// <typeparam name="T">The base game admin toy type.</typeparam>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <returns>The instantiated admin toy.</returns>
    protected static T Create<T>(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent) where T : AdminToyBase
    {
        if (PrefabCache<T>.prefab == null)
        {
            T? found = null;
            foreach (GameObject prefab in NetworkClient.prefabs.Values)
            {
                if (prefab.TryGetComponent(out found))
                    break;
            }

            if (found == null)
                throw new InvalidOperationException($"No prefab in NetworkClient.prefabs has component type {typeof(T)}");

            PrefabCache<T>.prefab = found;
        }

        T instance = UnityEngine.Object.Instantiate(PrefabCache<T>.prefab, parent);
        instance.transform.localPosition = position;
        instance.transform.localRotation = rotation;
        instance.transform.localScale = scale;
        return instance;
    }

    /// <summary>
    /// Gets the admin toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="AdminToyBase"/> was not null.
    /// </summary>
    /// <param name="adminToyBase">The <see cref="Base"/> of the admin toy.</param>
    /// <returns>The requested admin toy or null.</returns>
    [return: NotNullIfNotNull(nameof(adminToyBase))]
    public static AdminToy? Get(AdminToyBase? adminToyBase)
    {
        if (adminToyBase == null)
            return null;

        return Dictionary.TryGetValue(adminToyBase, out AdminToy item) ? item : CreateAdminToyWrapper(adminToyBase);
    }

    /// <summary>
    /// Tries to get the admin toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="adminToyBase">The <see cref="Base"/> of the admin toy.</param>
    /// <param name="adminToy">The requested admin toy.</param>
    /// <returns>True if the admin toy exists, otherwise false.</returns>
    public static bool TryGet(AdminToyBase? adminToyBase, [NotNullWhen(true)] out AdminToy? adminToy)
    {
        adminToy = Get(adminToyBase);
        return adminToy != null;
    }

    /// <summary>
    /// Creates a new wrapper from the base admin toy object.
    /// </summary>
    /// <param name="adminToyBase">The base object.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static AdminToy CreateAdminToyWrapper(AdminToyBase adminToyBase)
    {
        if (!typeWrappers.TryGetValue(adminToyBase.GetType(), out Func<AdminToyBase, AdminToy?> handler))
            Console.Logger.InternalWarn($"Failed to create derived admin toy wrapper. Missing constructor handler for type {adminToyBase.GetType()}");

        AdminToy? wrapper = handler.Invoke(adminToyBase);
        if (wrapper == null)
            Console.Logger.InternalWarn($"Failed to create derived admin toy wrapper. A handler returned null for type {adminToyBase.GetType()}");

        return wrapper ?? new AdminToy(adminToyBase);
    }

    /// <summary>
    /// A private method to handle the creation of new admin toys in the server.
    /// </summary>
    /// <param name="adminToyBase">The created <see cref="AdminToyBase"/> instance.</param>
    private static void AddAdminToy(AdminToyBase adminToyBase)
    {
        try
        {
            if (!Dictionary.ContainsKey(adminToyBase))
                _ = CreateAdminToyWrapper(adminToyBase);
        }
        catch(Exception e)
        {
            Console.Logger.InternalError($"Failed to handle admin toy creation with error: {e}");
        }
    }

    /// <summary>
    /// A private method to handle the removal of admin toys from the server.
    /// </summary>
    /// <param name="adminToyBase">The to be destroyed <see cref="AdminToyBase"/> instance.</param>
    private static void RemoveAdminToy(AdminToyBase adminToyBase)
    {
        try
        {

            if (Dictionary.TryGetValue(adminToyBase, out AdminToy adminToy))
            {
                Dictionary.Remove(adminToyBase);
                adminToy.OnRemove();
            }
        }
        catch (Exception e)
        {
            Console.Logger.InternalError($"Failed to handle admin toy destruction with error: {e}");
        }
    }

    /// <summary>
    /// A private method to handle the addition of wrapper handlers.
    /// </summary>
    /// <typeparam name="T">The derived base game type to handle.</typeparam>
    /// <param name="constructor">A handler to construct the wrapper with the base game instance.</param>
    private static void Register<T>(Func<T, AdminToy?> constructor) where T : AdminToyBase
    {
        typeWrappers.Add(typeof(T), x => constructor((T)x));
    }

    private static class PrefabCache<T> where T : AdminToyBase
    {
        /// <summary>
        /// Cached prefab instance for type T.
        /// </summary>
        public static T? prefab = null;
    }
}
