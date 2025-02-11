using Generators;
using MapGeneration.Distributors;
using Mirror;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="SpawnableStructure"/> object.
/// </summary>
public class Structure
{
    /// <summary>
    /// Initializes the <see cref="Structure"/> wrapper by subscribing to <see cref="SpawnableStructure"/> events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        SpawnableStructure.OnAdded += OnAdded;
        SpawnableStructure.OnRemoved += OnRemoved;

        Register<SpawnableStructure>(x => x.StructureType == StructureType.Workstation ? new Workstation(x) : null!);
        Register<Scp079Generator>(x => new Generator(x));
        Register<MapGeneration.Distributors.Locker>(x =>
        {
            return x.StructureType switch
            {
                StructureType.SmallWallCabinet => new WallCabinet(x),
                StructureType.StandardLocker => new StandardLocker(x),
                StructureType.LargeGunLocker => x.Chambers.Length > 9 ? new LargeLocker(x) : new RifleRackLocker(x),
                _ => null!,
            };
        });
        Register<PedestalScpLocker>(x => new PedestalLocker(x));
        Register<MapGeneration.Distributors.ExperimentalWeaponLocker>(x => new ExperimentalWeaponLocker(x));
    }

    /// <summary>
    /// Contains all the handlers for constructing wrappers for the associated base game types.
    /// </summary>
    private static readonly Dictionary<Type, Func<SpawnableStructure, Structure>> typeWrappers = [];

    /// <summary>
    /// Contains all the cached structures, accessible through their <see cref="SpawnableStructure"/>.
    /// </summary>
    public static Dictionary<SpawnableStructure, Structure> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Structure"/> instances.
    /// </summary>
    public static IReadOnlyCollection<Structure> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="spawnableStructure">The base <see cref="SpawnableStructure"/> object.</param>
    internal Structure(SpawnableStructure spawnableStructure)
    {
        Dictionary.Add(spawnableStructure, this);
        Base = spawnableStructure;
        StructurePositionSync = Base.gameObject.GetComponent<StructurePositionSync>();
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal virtual void OnRemove()
    {
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The base <see cref="SpawnableStructure"/> object.
    /// </summary>
    public SpawnableStructure Base { get; }

    /// <summary>
    /// The base <see cref="MapGeneration.Distributors.StructurePositionSync"/> object.
    /// </summary>
    public StructurePositionSync StructurePositionSync { get; }

    /// <summary>
    /// Gets the structure's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the structure's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets or sets the structure's position.
    /// </summary>
    /// <remarks>
    /// May not work for all map generated structures.
    /// </remarks>
    public Vector3 Position
    {
        get => Transform.position;
        set
        {
            Transform.position = value;
            StructurePositionSync.Network_position = value;
        }
    }

    /// <summary>
    /// Gets or sets the structure's rotation around the y-axis.
    /// </summary>
    /// <remarks>
    /// May not work for all map generated structures.
    /// </remarks>
    public float RotationY
    {
        get => Transform.rotation.eulerAngles.y;
        set
        {
            Transform.rotation = Quaternion.AngleAxis(value, Vector3.up);
            StructurePositionSync.Network_rotationY = (sbyte)Mathf.RoundToInt(value / StructurePositionSync.ConversionRate);
        }
    }

    /// <summary>
    /// Gets whether the structure was destroyed.
    /// </summary>
    public bool IsDestroyed => GameObject == null;

    /// <summary>
    /// Gets the <see cref="Room"/> based on the structures <see cref="Position"/>.
    /// </summary>
    public Room? Room => Room.GetRoomAtPosition(Position);

    // TODO: implement structure spawning.

    /// <summary>
    /// Spawns the structure.
    /// </summary>
    public void Spawn()
    {
        NetworkServer.Spawn(GameObject);
    }

    /// <summary>
    /// Destroys the structure.
    /// </summary>
    public void Destroy()
    {
        NetworkServer.Destroy(GameObject);
    }

    /// <summary>
    /// Gets the structure wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="SpawnableStructure"/> was not null.
    /// </summary>
    /// <param name="spawnableStructure">The <see cref="Base"/> of the structure.</param>
    /// <returns>The requested structure or null.</returns>
    [return: NotNullIfNotNull(nameof(spawnableStructure))]
    public static Structure? Get(SpawnableStructure? spawnableStructure)
    {
        if (spawnableStructure == null)
            return null;

        return Dictionary.TryGetValue(spawnableStructure, out Structure structure) ? structure : CreateStructureWrapper(spawnableStructure);
    }

    /// <summary>
    /// Tries to get the structure wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="spawnableStructure">The <see cref="Base"/> of the structure.</param>
    /// <param name="structure">The requested structure.</param>
    /// <returns>True of the structure exists, otherwise false.</returns>
    public static bool TryGet(SpawnableStructure? spawnableStructure, [NotNullWhen(true)] out Structure? structure)
    {
        structure = Get(spawnableStructure);
        return structure != null;
    }

    /// <summary>
    /// Creates a new wrapper from the base game object.
    /// </summary>
    /// <param name="structure">The base game structure to wrap.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static Structure CreateStructureWrapper(SpawnableStructure structure)
    {
        if (!typeWrappers.TryGetValue(structure.GetType(), out Func<SpawnableStructure, Structure> handler))
            Console.Logger.Error($"Failed to create structure wrapper. Missing constructor handler for type {structure.GetType()}");

        Structure wrapper = handler.Invoke(structure);
        if (wrapper == null)
            Console.Logger.Error($"Failed to create structure wrapper. A handler returned null for type {structure.GetType()}");

        return wrapper!;
    }

    /// <summary>
    /// Private method to handle the creation of new structures in the server.
    /// </summary>
    /// <param name="structure">The <see cref="SpawnableStructure"/> that was created.</param>
    private static void OnAdded(SpawnableStructure structure)
    {
        if (!Dictionary.ContainsKey(structure))
            _ = CreateStructureWrapper(structure);
    }

    /// <summary>
    /// Private method to handle the removal of structures from the server.
    /// </summary>
    /// <param name="spawnableStructure">The <see cref="SpawnableStructure"/> that was removed.</param>
    private static void OnRemoved(SpawnableStructure spawnableStructure)
    {
        if (Dictionary.TryGetValue(spawnableStructure, out Structure structure))
            structure.OnRemove();
    }

    /// <summary>
    /// A private method to handle the addition of wrapper handlers.
    /// </summary>
    /// <typeparam name="T">The derived base game type to handle.</typeparam>
    /// <param name="constructor">A handler to construct the wrapper with the base game instance.</param>
    private static void Register<T>(Func<T, Structure> constructor) where T : SpawnableStructure
    {
        typeWrappers.Add(typeof(T), x => constructor((T)x));
    }
}
