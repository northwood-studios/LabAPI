using AdminToys;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="ShootingTarget"/> class.
/// </summary>
public class ShootingTargetToy : AdminToy
{
    /// <summary>
    /// Contains all the shooting target toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<ShootingTarget, ShootingTargetToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ShootingTargetToy"/>.
    /// </summary>
    public new static IReadOnlyCollection<ShootingTargetToy> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="shootingTarget">The base <see cref="ShootingTarget"/> object.</param>
    internal ShootingTargetToy(ShootingTarget shootingTarget)
        : base(shootingTarget)
    {
        Dictionary.Add(shootingTarget, this);
        Base = shootingTarget;
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
    /// The <see cref="ShootingTarget"/> object.
    /// </summary>
    public new ShootingTarget Base { get; }

    /// <summary>
    /// Gets or sets whether other players can see your interactions.
    /// </summary>
    public bool IsGlobal
    {
        get => Base.Network_syncMode;
        set => Base.Network_syncMode = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[ShootingTargetToy: IsGlobal={IsGlobal}]";
    }

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    // BUG: you can only spawn one of the shooting target types and you dont get to choose which. 
    public static ShootingTargetToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    // BUG: you can only spawn one of the shooting target types and you dont get to choose which.
    public static ShootingTargetToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    // BUG: you can only spawn one of the shooting target types and you dont get to choose which.
    public static ShootingTargetToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new shooting target toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created shooting target toy.</returns>
    // BUG: you can only spawn one of the shooting target types and you dont get to choose which.
    public static ShootingTargetToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        ShootingTargetToy toy = Get(Create<ShootingTarget>(position, rotation, scale, parent));

        if (networkSpawn)
            toy.Spawn();

        return toy;
    }

    /// <summary>
    /// Gets the shooting target toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="ShootingTarget"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="shootingTarget">The <see cref="Base"/> of the shooting target toy.</param>
    /// <returns>The requested shooting target toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(shootingTarget))]
    public static ShootingTargetToy? Get(ShootingTarget? shootingTarget)
    {
        if (shootingTarget == null)
            return null;

        return Dictionary.TryGetValue(shootingTarget, out ShootingTargetToy toy) ? toy : (ShootingTargetToy)CreateAdminToyWrapper(shootingTarget);
    }

    /// <summary>
    /// Tries to get the shooting target toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="shootingTarget">The <see cref="Base"/> of the shooting target toy.</param>
    /// <param name="shootingTargetToy">The requested shooting target toy.</param>
    /// <returns><see langword="True"/> if the shooting target exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(ShootingTarget? shootingTarget, [NotNullWhen(true)] out ShootingTargetToy? shootingTargetToy)
    {
        shootingTargetToy = Get(shootingTarget);
        return shootingTargetToy != null;
    }
}