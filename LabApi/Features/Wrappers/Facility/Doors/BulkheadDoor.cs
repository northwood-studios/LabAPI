using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing the <see cref="PryableDoor"/> used for the bulkhead prefab instance.
/// </summary>
public class BulkheadDoor : Gate
{
    /// <summary>
    /// Contains all the cached <see cref="BulkheadDoor"/> instances, accessible through their <see cref="PryableDoor"/>.
    /// </summary>
    public new static Dictionary<PryableDoor, BulkheadDoor> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="BulkheadDoor"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<BulkheadDoor> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="pryableDoor">The base <see cref="PryableDoor"/> object.</param>
    internal BulkheadDoor(PryableDoor pryableDoor)
        : base(pryableDoor)
    {
        Dictionary.Add(pryableDoor, this);
        Base = pryableDoor;
        DoorCrusherExtension extension = pryableDoor.gameObject.GetComponent<DoorCrusherExtension>();
        if(extension != null)
            Crusher = new DoorCrusher(extension);
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
    /// The base <see cref="PryableDoor"/> object.
    /// </summary>
    public new PryableDoor Base { get; }

    /// <summary>
    /// The base <see cref="DoorCrusherExtension"/> component.
    /// </summary>
    /// <remarks>
    /// Can be null if bulkhead door crushing was disabled in the config.
    /// </remarks>
    public DoorCrusher? Crusher { get; }

    /// <summary>
    /// Gets the <see cref="BulkheadDoor"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="pryableDoor">The <see cref="PryableDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(pryableDoor))]
    public static BulkheadDoor? Get(PryableDoor? pryableDoor)
    {
        if (pryableDoor == null)
            return null;

        if (Dictionary.TryGetValue(pryableDoor, out BulkheadDoor door))
            return door;

        return (BulkheadDoor)CreateDoorWrapper(pryableDoor);
    }
}
