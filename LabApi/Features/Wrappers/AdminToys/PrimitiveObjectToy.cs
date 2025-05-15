using AdminToys;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BasePrimitiveObjectToy = AdminToys.PrimitiveObjectToy;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BasePrimitiveObjectToy"/> class.
/// </summary>
public class PrimitiveObjectToy : AdminToy
{
    /// <summary>
    /// Contains all the primitive object toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<BasePrimitiveObjectToy, PrimitiveObjectToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="PrimitiveObjectToy"/>.
    /// </summary>
    public new static IReadOnlyCollection<PrimitiveObjectToy> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="basePrimitiveObjectToy">The base <see cref="BasePrimitiveObjectToy"/> object.</param>
    internal PrimitiveObjectToy(BasePrimitiveObjectToy basePrimitiveObjectToy)
        : base(basePrimitiveObjectToy)
    {
        Dictionary.Add(basePrimitiveObjectToy, this);
        Base = basePrimitiveObjectToy;
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
    /// The <see cref="BasePrimitiveObjectToy"/> object.
    /// </summary>
    public new BasePrimitiveObjectToy Base { get; }

    /// <summary>
    /// Gets or sets the <see cref="PrimitiveType"/>.
    /// </summary>
    public PrimitiveType Type
    {
        get => Base.PrimitiveType;
        set => Base.NetworkPrimitiveType = value;
    }

    /// <summary>
    /// Gets or sets the material <see cref="UnityEngine.Color"/>.
    /// </summary>
    public Color Color
    {
        get => Base.MaterialColor;
        set => Base.NetworkMaterialColor = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="PrimitiveFlags"/>.
    /// </summary>
    /// <remarks>
    /// Setting flags to <see cref="PrimitiveFlags.None"/> is similar to having an empty object which is useful as a root object other toys parent to.
    /// </remarks>
    public PrimitiveFlags Flags
    {
        get => Base.PrimitiveFlags;
        set => Base.NetworkPrimitiveFlags = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[PrimitiveObjectToy: Type={Type}, Color={Color}, Flags={Flags}]";
    }

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static PrimitiveObjectToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static PrimitiveObjectToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static PrimitiveObjectToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new primitive object toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created primitive object toy.</returns>
    public static PrimitiveObjectToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        PrimitiveObjectToy toy = Get(Create<BasePrimitiveObjectToy>(position, rotation, scale, parent));

        if (networkSpawn)
            toy.Spawn();

        return toy;
    }

    /// <summary>
    /// Gets the primitive object toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BasePrimitiveObjectToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="primitiveObjectToy">The <see cref="Base"/> of the primitive object toy.</param>
    /// <returns>The requested primitive object toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(primitiveObjectToy))]
    public static PrimitiveObjectToy? Get(BasePrimitiveObjectToy? primitiveObjectToy)
    {
        if (primitiveObjectToy == null)
            return null;

        return Dictionary.TryGetValue(primitiveObjectToy, out PrimitiveObjectToy item) ? item : (PrimitiveObjectToy)CreateAdminToyWrapper(primitiveObjectToy);
    }

    /// <summary>
    /// Tries to get the primitive object toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="basePrimitiveObjectToy">The <see cref="Base"/> of the primitive object toy.</param>
    /// <param name="primitiveObjectToy">The requested primitive object toy.</param>
    /// <returns><see langword="True"/> if the primitive object exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(BasePrimitiveObjectToy? basePrimitiveObjectToy, [NotNullWhen(true)] out PrimitiveObjectToy? primitiveObjectToy)
    {
        primitiveObjectToy = Get(basePrimitiveObjectToy);
        return primitiveObjectToy != null;
    }
}
