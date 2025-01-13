using Interactables.Interobjects;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing the <see cref="BasicNonInteractableDoor"/>.
/// </summary>
public class NonInteractableDoor : Door
{
    /// <summary>
    /// Contains all the cached <see cref="NonInteractableDoor"/> instances, accessible through their <see cref="BasicNonInteractableDoor"/>.
    /// </summary>
    public new static Dictionary<BasicNonInteractableDoor, NonInteractableDoor> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="NonInteractableDoor"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<NonInteractableDoor> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="basicNonInteractableDoor">The base <see cref="BasicNonInteractableDoor"/> object.</param>
    internal NonInteractableDoor(BasicNonInteractableDoor basicNonInteractableDoor)
        :base(basicNonInteractableDoor)
    {
        Dictionary.Add(basicNonInteractableDoor, this);
        Base = basicNonInteractableDoor;
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
    /// The base <see cref="BasicNonInteractableDoor"/> object.
    /// </summary>
    public new BasicNonInteractableDoor Base { get; }

    /// <summary>
    /// Gets or sets whether or not SCP-106 can pass through the door when its not closed and locked.
    /// </summary>
    public bool Is106Passable
    {
        get => Base.IsScp106Passable;
        set => Base.IsScp106Passable = value;
    }

    /// <summary>
    /// Gets the <see cref="NonInteractableDoor"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="basicNonInteractableDoor">The <see cref="BasicNonInteractableDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(basicNonInteractableDoor))]
    public static NonInteractableDoor? Get(BasicNonInteractableDoor? basicNonInteractableDoor)
    {
        if (basicNonInteractableDoor == null)
            return null;

        if (Dictionary.TryGetValue(basicNonInteractableDoor, out NonInteractableDoor door))
            return door;

        return (NonInteractableDoor)CreateDoorWrapper(basicNonInteractableDoor);
    }
}
