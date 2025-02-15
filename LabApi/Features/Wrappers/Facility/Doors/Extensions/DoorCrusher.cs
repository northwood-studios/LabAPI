using Interactables.Interobjects.DoorUtils;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing the <see cref="DoorCrusherExtension"/>.
/// </summary>
public class DoorCrusher
{
    /// <summary>
    /// Creates a new wrapper for the <see cref="DoorCrusherExtension"/> component.
    /// </summary>
    /// <param name="doorCrusher">The base <see cref="DoorCrusherExtension"/> component.</param>
    public DoorCrusher(DoorCrusherExtension doorCrusher)
    {
        Base = doorCrusher;
    }

    /// <summary>
    /// The base <see cref="DoorCrusherExtension"/> component.
    /// </summary>
    public DoorCrusherExtension Base { get; }

    /// <summary>
    /// Gets or sets the max value <see cref="Door.ExactState"/> that crushing can occur at.
    /// </summary>
    public float MaxThreshold
    {
        get => Base.MaxCrushThreshold;
        set => Base.MaxCrushThreshold = value;
    }

    /// <summary>
    /// Gets or sets the min value <see cref="Door.ExactState"/> that crushing can occur at.
    /// </summary>
    public float MinThreshold
    {
        get => Base.MinCrushThreshold;
        set => Base.MinCrushThreshold = value;
    }

    /// <summary>
    /// Gets the collider used for determining if a player is in the crush zone.
    /// </summary>
    public Collider DeathCollider => Base.DeathCollider;

    /// <summary>
    /// Gets or sets the damage done to SCPs if <see cref="IgnoreScps"/> is false.
    /// </summary>
    public float ScpDamage
    {
        get => Base.ScpCrushDamage;
        set => Base.ScpCrushDamage = value;
    }

    /// <summary>
    /// Gets or sets whether SCPs should be excluded from taking damage from the crusher.
    /// </summary>
    public bool IgnoreScps
    {
        get => Base.IgnoreScps;
        set => Base.IgnoreScps = value;
    }
}
