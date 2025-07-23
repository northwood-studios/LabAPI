using Generators;
using MapGeneration.Distributors;
using PlayerStatsSystem;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="BreakableWindow"/> object.
/// </summary>
public class Window
{
    /// <summary>
    /// Initializes the <see cref="Window"/> wrapper by subscribing to <see cref="BreakableWindow"/> events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        BreakableWindow.OnAdded += OnAdded;
        BreakableWindow.OnDestroyed += OnRemoved;
    }

    /// <summary>
    /// Contains all the cached structures, accessible through their <see cref="BreakableWindow"/>.
    /// </summary>
    public static Dictionary<BreakableWindow, Window> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Structure"/> instances.
    /// </summary>
    public static IReadOnlyCollection<Window> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="breakableWindow">The base <see cref="BreakableWindow"/> object.</param>
    internal Window(BreakableWindow breakableWindow)
    {
        Base = breakableWindow;

        if (CanCache)
            Dictionary.Add(breakableWindow, this);
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
    public BreakableWindow Base { get; }

    /// <summary>
    /// Gets the structure's <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the structure's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Whether to cache the wrapper.
    /// </summary>
    protected internal bool CanCache => !IsDestroyed && Base.isActiveAndEnabled;

    /// <summary>
    /// Gets whether the window gameobject was destroyed.
    /// </summary>
    public bool IsDestroyed => Base == null || GameObject == null;

    /// <summary>
    /// Gets whether the window is broken.
    /// </summary>
    public bool IsBroken => Base.NetworkIsBroken;

    /// <summary>
    /// Gets or sets window's health.
    /// </summary>
    public float Health
    {
        get => Base.Health;
        set
        {
            Base.Damage(Base.Health - value, new CustomReasonDamageHandler(string.Empty), Vector3.zero);
        }
    }

    /// <summary>
    /// Gets the window's position.
    /// </summary>
    public Vector3 Position
    {
        get => Transform.position;
    }

    /// <summary>
    /// Gets the window's rotation.
    /// </summary>
    public Quaternion Rotation
    {
        get => Transform.rotation;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[{GetType().Name}: Position={Position}, Rotation={Rotation} IsDestroyed={IsDestroyed}]";
    }

    /// <summary>
    /// Gets the structure wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BreakableWindow"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="breakableWindow">The <see cref="Base"/> of the window.</param>
    /// <returns>The requested structure or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(breakableWindow))]
    public static Window? Get(BreakableWindow? breakableWindow)
    {
        if (breakableWindow == null)
            return null;

        return Dictionary.TryGetValue(breakableWindow, out Window structure) ? structure : new Window(breakableWindow);
    }

    /// <summary>
    /// Tries to get the window wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="breakableWindow">The <see cref="Base"/> of the window.</param>
    /// <param name="window">The requested window.</param>
    /// <returns><see langword="True"/> of the structure exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(BreakableWindow? breakableWindow, [NotNullWhen(true)] out Window? window)
    {
        window = Get(breakableWindow);
        return window != null;
    }

    /// <summary>
    /// Private method to handle the creation of new windows in the server.
    /// </summary>
    /// <param name="structure">The <see cref="BreakableWindow"/> that was created.</param>
    private static void OnAdded(BreakableWindow structure)
    {
        if (!Dictionary.ContainsKey(structure))
            _ = new Window(structure);
    }

    /// <summary>
    /// Private method to handle the removal of windows from the server.
    /// </summary>
    /// <param name="spawnableStructure">The <see cref="BreakableWindow"/> that was removed.</param>
    private static void OnRemoved(BreakableWindow spawnableStructure)
    {
        if (Dictionary.TryGetValue(spawnableStructure, out Window structure))
            structure.OnRemove();
    }


}
