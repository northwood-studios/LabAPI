using Generators;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="PocketDimensionTeleport"/> base object.
/// </summary>
public class PocketTeleport
{
    /// <summary>
    /// Initializes the Teleport wrapper by subscribing to the PocketDimensionTeleport events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        PocketDimensionTeleport.OnAdded += OnAdded;
        PocketDimensionTeleport.OnRemoved += OnRemoved;
    }

    /// <summary>
    /// Contains all the cached teleports in the game, accessible through their <see cref="PocketDimensionTeleport"/>.
    /// </summary>
    private static Dictionary<PocketDimensionTeleport, PocketTeleport> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="PocketTeleport"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<PocketTeleport> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="pocketDimensionTeleport">The <see cref="PocketDimensionTeleport"/> of the teleport.</param>
    internal PocketTeleport(PocketDimensionTeleport pocketDimensionTeleport)
    {
        Dictionary.Add(pocketDimensionTeleport, this);
        Base = pocketDimensionTeleport;
        Collider = Base.GetComponent<SphereCollider>();
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public PocketDimensionTeleport Base { get; }

    /// <summary>
    /// The <see cref="UnityEngine.GameObject"/> of the pocket teleport.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// The <see cref="UnityEngine.Transform"/> of the pocket teleport.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// The collider used for player detection.
    /// </summary>
    public SphereCollider Collider { get; }

    /// <summary>
    /// Gets or sets whether the <see cref="PocketTeleport"/> is an exit.
    /// </summary>
    /// <remarks>
    /// Teleports that are not exits, kill the player on entering.
    /// </remarks>
    public bool IsExit
    {
        get => Base.GetTeleportType() == PocketDimensionTeleport.PDTeleportType.Exit;
        set => Base.SetType(value ? PocketDimensionTeleport.PDTeleportType.Exit : PocketDimensionTeleport.PDTeleportType.Killer);
    }

    /// <summary>
    /// Gets or set the local position of the pocket teleport relative to the pocket dimension.
    /// </summary>
    public Vector3 Position
    {
        get => Transform.localPosition;
        set => Transform.localPosition = value;
    }

    /// <summary>
    /// Gets or sets the rotation of the pocket teleport.
    /// </summary>
    public Quaternion Rotation
    {
        get => Transform.rotation;
        set => Transform.rotation = value;
    }

    /// <summary>
    /// Gets or sets the scale of the pocket teleport.
    /// </summary>
    public Vector3 Scale
    {
        get => Transform.localScale;
        set => Transform.localScale = value;
    }

    /// <summary>
    /// Gets or sets the radius of the <see cref="Collider">Sphere Collider</see> used for player detection.
    /// </summary>
    public float Radius
    {
        get => Collider.radius;
        set => Collider.radius = value;
    }

    /// <summary>
    /// Spawns a new pocket teleport.
    /// </summary>
    public PocketTeleport Spawn(Vector3 localPosition)
    {
        GameObject obj = new("Teleport", typeof(PocketDimensionTeleport), typeof(SphereCollider), typeof(NetworkIdentity));
        PocketDimensionTeleport pt = obj.GetComponent<PocketDimensionTeleport>();
        obj.transform.localPosition = localPosition;
        return Get(pt);
    }

    /// <summary>
    /// Destroys the <see cref="PocketTeleport"/> removing it from the server.
    /// </summary>
    public void Destroy()
    {
        Object.Destroy(Base);
    }

    /// <summary>
    /// Gets the wrapper given the base game <see cref="PocketDimensionTeleport"/> instance.
    /// </summary>
    /// <param name="pocketTeleport">The base game object.</param>
    /// <returns>The associated wrapper.</returns>
    public PocketTeleport Get(PocketDimensionTeleport pocketTeleport)
    {
        if (Dictionary.TryGetValue(pocketTeleport, out PocketTeleport pt))
            return pt;

        return new PocketTeleport(pocketTeleport);
    }

    /// <summary>
    /// A private method to handle the addition of <see cref="PocketDimensionTeleport"/> instances.
    /// </summary>
    /// <param name="pocketTeleport">The base game instance.</param>
    private static void OnAdded(PocketDimensionTeleport pocketTeleport)
    {
        if (!Dictionary.ContainsKey(pocketTeleport))
            _ = new PocketTeleport(pocketTeleport);
    }

    /// <summary>
    /// A private method to handle the removal of <see cref="PocketDimensionTeleport"/> instances.
    /// </summary>
    /// <param name="pocketTeleport">The base game instance.</param>
    private static void OnRemoved(PocketDimensionTeleport pocketTeleport)
    {
        Dictionary.Remove(pocketTeleport);
    }
}

