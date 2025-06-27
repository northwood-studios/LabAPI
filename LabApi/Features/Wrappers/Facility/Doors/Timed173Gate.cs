using Interactables.Interobjects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing the <see cref="Timed173PryableDoor"/>.
/// </summary>
/// <remarks>
/// Used by old 173s containment to open the door after a short delay after round start.
/// </remarks>
public class Timed173Gate : Gate
{
    /// <summary>
    /// Contains all the cached <see cref="Timed173Gate"/> instances, accessible through their <see cref="Timed173PryableDoor"/>.
    /// </summary>
    public new static Dictionary<Timed173PryableDoor, Timed173Gate> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Timed173Gate"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<Timed173Gate> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="timed173PryableDoor">The base <see cref="Timed173PryableDoor"/> object.</param>
    internal Timed173Gate(Timed173PryableDoor timed173PryableDoor)
        : base(timed173PryableDoor)
    {
        Dictionary.Add(timed173PryableDoor, this);
        Base = timed173PryableDoor;
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The base <see cref="Timed173PryableDoor"/> object.
    /// </summary>
    public new Timed173PryableDoor Base { get; }

    /// <summary>
    /// Gets the current stopwatch used to time to gate opening.
    /// </summary>
    public Stopwatch Stopwatch => Base.Stopwatch;

    /// <summary>
    /// Gets or sets whether the gate will open if an SCP-173 is present.
    /// </summary>
    /// <remarks>
    /// The gate is still unlocked after the specified time regardless of this setting.
    /// </remarks>
    public bool SmartOpen
    {
        get => Base.SmartOpen;
        set => Base.SmartOpen = value;
    }

    /// <summary>
    /// Gets or sets the delay in seconds after round start to unlock and/or open the gate.
    /// </summary>
    public float Delay
    {
        get => Base.TimeMark;
        set => Base.TimeMark = value;
    }

    /// <summary>
    /// Gets the <see cref="Timed173Gate"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="timed173PryableDoor">The <see cref="Timed173PryableDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(timed173PryableDoor))]
    public static Timed173Gate? Get(Timed173PryableDoor? timed173PryableDoor)
    {
        if (timed173PryableDoor == null)
            return null;

        if (Dictionary.TryGetValue(timed173PryableDoor, out Timed173Gate door))
            return door;

        return (Timed173Gate)CreateDoorWrapper(timed173PryableDoor);
    }
}