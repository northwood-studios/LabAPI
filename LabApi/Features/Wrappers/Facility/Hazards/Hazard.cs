using Hazards;
using InventorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Generators;
using UnityEngine;
using Mirror;
using System.Diagnostics.CodeAnalysis;
using PlayerRoles.PlayableScps.Scp939;
using Logger = LabApi.Features.Console.Logger;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing all static and temporary <see cref="EnvironmentalHazard"/>.
/// </summary>
public class Hazard
{
    /// <summary>
    /// Contains all the handlers for constructing wrappers for the associated base game types.
    /// </summary>
    private static readonly Dictionary<Type, Func<EnvironmentalHazard, Hazard>> typeWrappers = [];

    /// <summary>
    /// Contains all the cached items, accessible through their <see cref="Base"/>.
    /// </summary>
    public static Dictionary<EnvironmentalHazard, Hazard> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Hazard"/> or its subclasses.
    /// </summary>
    public static IReadOnlyCollection<Hazard> List => Dictionary.Values;

    /// <summary>
    /// Prefab used to spawn the hazard.
    /// </summary>
    protected static EnvironmentalHazard? BasePrefab;

    /// <summary>
    /// Base game object.
    /// </summary>
    public EnvironmentalHazard Base { get; private set; }

    /// <summary>
    /// Gets all affected players by this hazard.
    /// </summary>
    public IEnumerable<Player> AffectedPlayers => Base.AffectedPlayers.Select(n => Player.Get(n));

    /// <summary>
    /// Gets or sets the maximum distance players have to be at, for this hazard to affect them.
    /// </summary>
    public float MaxDistance
    {
        get => Base.MaxDistance;
        set => Base.MaxDistance = value;
    }

    /// <summary>
    /// Gets or sets the maximum height players have to be at, for this hazard to affect them.
    /// </summary>
    public float MaxHeightDistance
    {
        get => Base.MaxHeightDistance;
        set => Base.MaxHeightDistance = value;
    }

    /// <summary>
    /// Gets or sets the offset applied to the <see cref="SourcePosition"/>.
    /// </summary>
    public virtual Vector3 SourceOffset
    {
        get => Base.SourceOffset;
        set => Base.SourceOffset = value;
    }

    /// <summary>
    /// Gets whether this environmental hazard and it's effects is enabled. 
    /// Setting to false also stops the decay of temporary hazards.
    /// </summary>
    public virtual bool IsActive
    {
        get => Base.IsActive;
        set => Base.IsActive = value;
    }

    /// <summary>
    /// Gets or sets the origin point from which the AoE effect will start.
    /// </summary>
    public virtual Vector3 SourcePosition
    {
        get => Base.SourcePosition;
        set => Base.SourcePosition = value;
    }

    /// <summary>
    /// Gets whether the hazard is destroyed.
    /// </summary>
    public bool IsDestroyed => Base.gameObject != null;

    /// <summary>
    /// Gets the room in which this hazard is in.
    /// </summary>
    public Room? Room => Room.GetRoomAtPosition(SourcePosition);

    /// <summary>
    /// Initializes the <see cref="Hazard"/> class to subscribe to <see cref="EnvironmentalHazard"/> events and handle the item caching.
    /// </summary>
    protected Hazard(EnvironmentalHazard hazard)
    {
        Base = hazard;
        Dictionary.Add(hazard, this);
    }

    /// <summary>
    /// Initializes the <see cref="Item"/> class to subscribe to <see cref="InventoryExtensions"/> events and handle the item caching.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();

        EnvironmentalHazard.OnAdded += AddHazard;
        EnvironmentalHazard.OnRemoved += RemoveHazard;

        Register<SinkholeEnvironmentalHazard>(x => new SinkholeHazard(x));
        Register<TantrumEnvironmentalHazard>(x => new TantrumHazard(x));
        Register<Scp939AmnesticCloudInstance>(x => new AmnesticCloudHazard(x));
    }

    /// <summary>
    /// An internal virtual method to signal to derived implementations to uncache when the base object is destroyed.
    /// </summary>
    internal virtual void OnRemove()
    {
    }

    /// <summary>
    /// Spawns a <see cref="Hazard"/> at specified position with specified rotation and scale.
    /// </summary>
    /// <param name="prefab">The target prefab.</param>
    /// <param name="position">The target position to spawn this hazard at.</param>
    /// <param name="rotation">The target rotation to spawn this hazard with.</param>
    /// <param name="scale">The target scale to spawn with.</param>
    /// <returns>A new hazard.</returns>
    public static Hazard Spawn(EnvironmentalHazard prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        EnvironmentalHazard hazard = GameObject.Instantiate(prefab);
        hazard.transform.SetPositionAndRotation(position, rotation);
        hazard.transform.localScale = scale;

        return Get(hazard);
    }

    /// <summary>
    /// Attempts to get the prefab from <see cref="NetworkClient.prefabs"/>.
    /// </summary>
    /// <typeparam name="T">Type of the hazard.</typeparam>
    /// <returns>Prefab <see cref="GameObject"/> if it was found. Otherwise <see langword="null"/></returns>
    protected static T? GetPrefab<T>() where T : EnvironmentalHazard
    {
        foreach (GameObject prefab in NetworkClient.prefabs.Values)
        {
            if (!prefab.TryGetComponent(out T hazard))
                continue;

            return hazard;
        }

        return null;
    }

    /// <summary>
    /// Gets whether the player is in the hazard area.
    /// </summary>
    /// <param name="player">Target player to check on.</param>
    /// <returns>Whether the player is within hazard area.</returns>
    public bool IsInArea(Player player) => Base.IsInArea(SourcePosition, player.Position);

    /// <summary>
    /// Destroys this hazard.
    /// </summary>
    public virtual void Destroy()
    {
        if (Base.gameObject != null)
            NetworkServer.Destroy(Base.gameObject);
    }

    /// <summary>
    /// A private method to handle the creation of new hazards in the server.
    /// </summary>
    /// <param name="hazard">The created <see cref="EnvironmentalHazard"/> instance.</param>
    private static void AddHazard(EnvironmentalHazard hazard)
    {
        if (!Dictionary.ContainsKey(hazard))
            _ = CreateItemWrapper(hazard);
    }

    /// <summary>
    /// A private method to handle the removal of hazards from the server.
    /// </summary>
    /// <param name="hazard">The to be destroyed <see cref="EnvironmentalHazard"/> instance.</param>
    private static void RemoveHazard(EnvironmentalHazard hazard)
    {
        if (Dictionary.Remove(hazard, out Hazard item))
        {
            item.OnRemove();
        }
    }

    /// <summary>
    /// A private method to handle the addition of wrapper handlers.
    /// </summary>
    /// <typeparam name="T">The derived base game type to handle.</typeparam>
    /// <param name="constructor">A handler to construct the wrapper with the base game instance.</param>
    private static void Register<T>(Func<T, Hazard> constructor) where T : EnvironmentalHazard
    {
        typeWrappers.Add(typeof(T), x => constructor((T)x));
    }

    /// <summary>
    /// Creates a new wrapper from the base envronental hazard object.
    /// </summary>
    /// <param name="hazard">The base object.</param>
    /// <returns>The newly created wrapper.</returns>
    protected static Hazard? CreateItemWrapper(EnvironmentalHazard hazard)
    {
        Type targetType = hazard.GetType();
        if (!typeWrappers.TryGetValue(targetType, out Func<EnvironmentalHazard, Hazard> ctorFunc))
        {
#if DEBUG
            Logger.Warn($"Unable to find {nameof(Hazard)} wrapper for {targetType.Name}, backup up to base constructor!");
#endif
            return new Hazard(hazard);
        }

        return ctorFunc.Invoke(hazard);
    }

    /// <summary>
    /// Gets the hazard wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="EnvironmentalHazard"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="hazard">The <see cref="Base"/> of the hazard.</param>
    /// <returns>The requested hazard or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(hazard))]
    public static Hazard? Get(EnvironmentalHazard? hazard)
    {
        if (hazard == null)
            return null;

        return Dictionary.TryGetValue(hazard, out Hazard wrapper) ? wrapper : CreateItemWrapper(hazard);
    }


    /// <summary>
    /// Tries to get the hazard wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="envHazard">The <see cref="Base"/> of the hazard.</param>
    /// <param name="wrapper">The requested hazard.</param>
    /// <returns><see langword="true"/> if the item exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(EnvironmentalHazard? envHazard, [NotNullWhen(true)] out Hazard? wrapper)
    {
        wrapper = Get(envHazard);
        return wrapper != null;
    }

    /// <summary>
    /// Gets all hazards in a specified room.
    /// </summary>
    /// <param name="room">The target room to check on.</param>
    /// <returns>Hazards in specified room.</returns>
    public static IEnumerable<Hazard> Get(Room? room)
    {
        if (room == null)
            yield break;

        foreach (Hazard hazard in List)
        {
            if (hazard.Room == room)
                yield return hazard;
        }
    }
}