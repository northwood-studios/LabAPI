using System.Collections.Generic;
using BaseLocker = MapGeneration.Distributors.Locker;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the LargeLocker prefab instances.
/// </summary>
public class LargeLocker : Locker
{
    /// <summary>
    /// Contains all the cached large lockers, accessible through their <see cref="BaseLocker"/>.
    /// </summary>
    public static new Dictionary<BaseLocker, LargeLocker> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="LargeLocker"/> instances.
    /// </summary>
    public static new IReadOnlyCollection<LargeLocker> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLocker">The base <see cref="BaseLocker"/> object.</param>
    internal LargeLocker(BaseLocker baseLocker)
        : base(baseLocker)
    {
        if (CanCache)
        {
            Dictionary.Add(baseLocker, this);
        }
    }

    /// <summary>
    /// Gets the large chamber that is at the top left.
    /// </summary>
    public LockerChamber TopLeft => Chambers[0];

    /// <summary>
    /// Gets the large chamber that is at the top middle.
    /// </summary>
    public LockerChamber TopMiddle => Chambers[1];

    /// <summary>
    /// Gets the large chamber that is at the top right.
    /// </summary>
    public LockerChamber TopRight => Chambers[2];

    /// <summary>
    /// Gets the large chamber that is at the bottom left.
    /// </summary>
    public LockerChamber BottomLeft => Chambers[3];

    /// <summary>
    /// Gets the large chamber that is at the bottom right.
    /// </summary>
    public LockerChamber BottomRight => Chambers[4];

    /// <summary>
    /// Gets the small chamber that is at the middle left.
    /// </summary>
    public LockerChamber MiddleLeft => Chambers[5];

    /// <summary>
    /// Gets the small chamber that is at the center.
    /// </summary>
    public LockerChamber Center => Chambers[6];

    /// <summary>
    /// Gets the small chamber that is at the middle right.
    /// </summary>
    public LockerChamber MiddleRight => Chambers[7];

    /// <summary>
    /// Gets the small chamber that is at the bottom middle.
    /// </summary>
    public LockerChamber BottomMiddle => Chambers[8];

    /// <summary>
    /// Gets the drawer chamber that is at the bottom.
    /// </summary>
    public LockerChamber Drawer => Chambers[9];

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
