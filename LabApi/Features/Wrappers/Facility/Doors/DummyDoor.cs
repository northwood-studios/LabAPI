using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseDummyDoor = Interactables.Interobjects.DummyDoor;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseDummyDoor">dummy doors</see>, a fake door used for decoration only.
/// </summary>
public class DummyDoor : Door
{
    /// <summary>
    /// Contains all the cached <see cref="DummyDoor"/> instances, accessible through their <see cref="BaseDummyDoor"/>.
    /// </summary>
    public new static Dictionary<BaseDummyDoor, DummyDoor> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="DummyDoor"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<DummyDoor> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseDummyDoor">The base <see cref="BaseDummyDoor"/> object.</param>
    internal DummyDoor(BaseDummyDoor baseDummyDoor)
        : base(baseDummyDoor)
    {
        Dictionary.Add(baseDummyDoor, this);
        Base = baseDummyDoor;
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
    /// The base <see cref="BaseDummyDoor"/> object.
    /// </summary>
    public new BaseDummyDoor Base { get; }

    /// <summary>
    /// Gets the <see cref="DummyDoor"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="baseDummyDoor">The <see cref="BaseDummyDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(baseDummyDoor))]
    public static DummyDoor? Get(BaseDummyDoor? baseDummyDoor)
    {
        if (baseDummyDoor == null)
            return null;

        if (Dictionary.TryGetValue(baseDummyDoor, out DummyDoor door))
            return door;

        return (DummyDoor)CreateDoorWrapper(baseDummyDoor);
    }
}
