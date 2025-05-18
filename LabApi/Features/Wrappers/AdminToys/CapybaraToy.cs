using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BaseCapybaraToy = AdminToys.CapybaraToy;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseCapybaraToy"/> class.
/// </summary>
public class CapybaraToy : AdminToy
{
    /// <summary>
    /// Contains all the capybara toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<BaseCapybaraToy, CapybaraToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="CapybaraToy"/>.
    /// </summary>
    public new static IReadOnlyCollection<CapybaraToy> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseCapybaraToy">The base <see cref="BaseCapybaraToy"/> object.</param>
    internal CapybaraToy(BaseCapybaraToy baseCapybaraToy) : base(baseCapybaraToy)
    {
        Dictionary.Add(baseCapybaraToy, this);
        Base = baseCapybaraToy;
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
    /// The <see cref="BaseCapybaraToy"/> object.
    /// </summary>
    public new BaseCapybaraToy Base { get; }

    /// <summary>
    /// Gets or sets whether the capybara has enabled colliders.
    /// </summary>
    public bool CollidersEnabled
    {
        get => Base.CollisionsEnabled;
        set => Base.CollisionsEnabled = value;
    }

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static CapybaraToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static CapybaraToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static CapybaraToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new capybara toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created capybara toy.</returns>
    public static CapybaraToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        CapybaraToy toy = Get(Create<BaseCapybaraToy>(position, rotation, scale, parent));

        if (networkSpawn)
            toy.Spawn();

        return toy;
    }

    /// <summary>
    /// Gets the capybara toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseCapybaraToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseCapybaraToy">The <see cref="Base"/> of the speaker toy.</param>
    /// <returns>The requested capybara toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseCapybaraToy))]
    public static CapybaraToy? Get(BaseCapybaraToy? baseCapybaraToy)
    {
        if (baseCapybaraToy == null)
            return null;

        return Dictionary.TryGetValue(baseCapybaraToy, out CapybaraToy toy) ? toy : (CapybaraToy)CreateAdminToyWrapper(baseCapybaraToy);
    }
}