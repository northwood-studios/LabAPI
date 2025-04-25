using Interactables.Interobjects;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BaseElevatorDoor = Interactables.Interobjects.ElevatorDoor;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseElevatorDoor">elevator doors</see>.
/// </summary>
public class ElevatorDoor : Door
{
    /// <summary>
    /// Contains all the cached <see cref="ElevatorDoor"/> instances, accessible through their <see cref="BaseElevatorDoor"/>.
    /// </summary>
    public new static Dictionary<BaseElevatorDoor, ElevatorDoor> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="ElevatorDoor"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<ElevatorDoor> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseElevatorDoor">The base <see cref="BaseElevatorDoor"/> object.</param>
    internal ElevatorDoor(BaseElevatorDoor baseElevatorDoor)
        : base(baseElevatorDoor)
    {
        Dictionary.Add(baseElevatorDoor, this);
        Base = baseElevatorDoor;
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
    /// The base <see cref="BaseElevatorDoor"/> object.
    /// </summary>
    public new BaseElevatorDoor Base { get; }

    /// <summary>
    /// Gets the <see cref="Wrappers.Elevator"/> this door belongs to.
    /// </summary>
    public Elevator? Elevator => Elevator.GetByGroup(Base.Group).FirstOrDefault();

    /// <summary>
    /// Gets the <see cref="ElevatorGroup"/> this door belongs to.
    /// </summary>
    public ElevatorGroup Group => Base.Group;

    /// <summary>
    /// Gets the <see cref="ElevatorDoor"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="baseElevatorDoor">The <see cref="BaseElevatorDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(baseElevatorDoor))]
    public static ElevatorDoor? Get(BaseElevatorDoor? baseElevatorDoor)
    {
        if (baseElevatorDoor == null)
            return null;

        if (Dictionary.TryGetValue(baseElevatorDoor, out ElevatorDoor door))
            return door;

        return (ElevatorDoor)CreateDoorWrapper(baseElevatorDoor);
    }
}
