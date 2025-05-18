using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;
using BaseLocker = MapGeneration.Distributors.Locker;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the WallCabinet prefab instances.
/// </summary>
public class WallCabinet : Locker
{
    /// <summary>
    /// Contains all the cached wall cabinets, accessible through their <see cref="BaseLocker"/>.
    /// </summary>
    public new static Dictionary<BaseLocker, WallCabinet> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="WallCabinet"/> instances.
    /// </summary>
    public new static IReadOnlyCollection<WallCabinet> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLocker">The base <see cref="BaseLocker"/> object.</param>
    internal WallCabinet(BaseLocker baseLocker)
        : base(baseLocker)
    {
        Dictionary.Add(baseLocker, this);
    }

    /// <summary>
    /// An internal method to remove itself form the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// Gets or sets whether or not the wall cabinet is open.
    /// </summary>
    public bool IsOpen
    {
        get => MainChamber.IsOpen;
        set => MainChamber.IsOpen = value;
    }

    /// <summary>
    /// Gets or sets whether the wall cabinet can be interacted with by a <see cref="Player"/>.
    /// </summary>
    public bool CanInteract => MainChamber.CanInteract;

    /// <summary>
    /// Gets or sets the <see cref="DoorPermissionFlags"/> required by a the <see cref="Player"/> to open/close the wall cabinet.
    /// </summary>
    public DoorPermissionFlags RequiredPermissions
    {
        get => MainChamber.RequiredPermissions;
        set => MainChamber.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets or sets the latest cooldown duration on the wall cabinet.
    /// </summary>
    /// <remarks>
    /// Cooldown for door open/close and denied interactions.
    /// </remarks>
    public float TargetCooldown
    {
        get => MainChamber.TargetCooldown;
        set => MainChamber.TargetCooldown = value;
    }

    /// <summary>
    /// Gets the large bottom main chamber of the wall cabinet.
    /// </summary>
    /// <remarks>
    /// This chamber controls the door of cabinet.
    /// </remarks>
    public LockerChamber MainChamber => Chambers[0];

    /// <summary>
    /// Gets the lower shelf chamber of the wall cabinet.
    /// </summary>
    /// <remarks>
    /// This chamber does not control any doors.
    /// </remarks>
    public LockerChamber LowerShelf => Chambers[1];

    /// <summary>
    /// Gets the upper shelf chamber of the wall cabinet.
    /// </summary>
    /// <remarks>
    /// This chamber does not control any doors.
    /// </remarks>
    public LockerChamber UpperShelf => Chambers[2];
}