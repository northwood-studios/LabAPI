using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;
using BaseLocker = MapGeneration.Distributors.Locker;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the RifleRack prefab instances.
/// </summary>
public class RifleRackLocker : Locker
{
    /// <summary>
    /// Contains all the cached rifle rack lockers, accessible through their <see cref="BaseLocker"/>.
    /// </summary>
    public static new Dictionary<BaseLocker, RifleRackLocker> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="RifleRackLocker"/> instances.
    /// </summary>
    public static new IReadOnlyCollection<RifleRackLocker> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLocker">The base <see cref="BaseLocker"/> object.</param>
    internal RifleRackLocker(BaseLocker baseLocker)
        : base(baseLocker)
    {
        if (CanCache)
        {
            Dictionary.Add(baseLocker, this);
        }
    }

    /// <summary>
    /// Gets or sets whether the rifle rack is open.
    /// </summary>
    public bool IsOpen
    {
        get => MainChamber.IsOpen;
        set => MainChamber.IsOpen = value;
    }

    /// <summary>
    /// Gets whether the rifle rack can be interacted with by a <see cref="Player"/>.
    /// </summary>
    public bool CanInteract => MainChamber.CanInteract;

    /// <summary>
    /// Gets or sets the <see cref="DoorPermissionFlags"/> required by the player to open/close the rifle rack.
    /// </summary>
    public DoorPermissionFlags RequiredPermissions
    {
        get => MainChamber.RequiredPermissions;
        set => MainChamber.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets or sets the latest cooldown duration on the rifle rack.
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
    /// Gets the chamber used for the E11.
    /// </summary>
    /// <remarks>
    /// This chamber controls the door.
    /// </remarks>
    public LockerChamber MainChamber => Chambers[0];

    /// <summary>
    /// Gets the chamber used for bullet spawn 1.
    /// </summary>
    public LockerChamber Bullet1 => Chambers[1];

    /// <summary>
    /// Gets the chamber used for bullet spawn 2.
    /// </summary>
    public LockerChamber Bullet2 => Chambers[2];

    /// <summary>
    /// Gets the chamber used for bullet spawn 3.
    /// </summary>
    public LockerChamber Bullet3 => Chambers[3];

    /// <summary>
    /// Gets the chamber used for bullet spawn 4.
    /// </summary>
    public LockerChamber Bullet4 => Chambers[4];

    /// <summary>
    /// Gets the chamber used for grenade spawn 1.
    /// </summary>
    public LockerChamber HeGrenade1 => Chambers[5];

    /// <summary>
    /// Gets the chamber used for grenade spawn 2.
    /// </summary>
    public LockerChamber HeGrenade2 => Chambers[6];

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
