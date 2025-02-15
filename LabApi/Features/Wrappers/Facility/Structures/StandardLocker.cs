using System.Collections.Generic;
using BaseLocker = MapGeneration.Distributors.Locker;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the StandardLocker prefab instance.
/// </summary>
public class StandardLocker : Locker
{
    /// <summary>
    /// Contains all the cached standard lockers, accessible through their <see cref="BaseLocker"/>.
    /// </summary>
    public new static Dictionary<BaseLocker, StandardLocker> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="StandardLocker"/> instances.
    /// </summary>
    public new static IReadOnlyCollection<StandardLocker> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLocker">The base <see cref="BaseLocker"/> object.</param>
    internal StandardLocker(BaseLocker baseLocker)
        : base(baseLocker)
    {
        Dictionary.Add(baseLocker, this);
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the abase object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// Gets the main left chamber.
    /// </summary>
    /// <remarks>
    /// This chamber controls the left door.
    /// </remarks>
    public LockerChamber MainLeft => Chambers[0];

    /// <summary>
    /// Gets the main middle chamber.
    /// </summary>
    /// <remarks>
    /// This chamber controls the middle door.
    /// </remarks>
    public LockerChamber MainMiddle => Chambers[1];

    /// <summary>
    /// Gets the main right chamber.
    /// </summary>
    /// <remarks>
    /// This chamber controls the right door.
    /// </remarks>
    public LockerChamber MainRight => Chambers[2];

    /// <summary>
    /// Gets the bottom left chamber.
    /// </summary>
    /// <remarks>
    /// This chamber does not control any doors.
    /// </remarks>
    public LockerChamber BottomLeft => Chambers[5];

    /// <summary>
    /// Gets the bottom middle chamber.
    /// </summary>
    /// <remarks>
    /// This chamber does not control any doors.
    /// </remarks>
    public LockerChamber BottomMiddle => Chambers[4];

    /// <summary>
    /// Gets the bottom right chamber.
    /// </summary>
    /// <remarks>
    /// This chamber does not control any doors.
    /// </remarks>
    public LockerChamber BottomRight => Chambers[3];
}
