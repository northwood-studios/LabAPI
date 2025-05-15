using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BaseLightSourceToy = AdminToys.LightSourceToy;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseLightSourceToy"/> class.
/// </summary>
public class LightSourceToy : AdminToy
{
    /// <summary>
    /// Contains all the light source toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<BaseLightSourceToy, LightSourceToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="LightSourceToy"/>.
    /// </summary>
    public new static IReadOnlyCollection<LightSourceToy> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLightSourceToy">The base <see cref="BaseLightSourceToy"/> object.</param>
    internal LightSourceToy(BaseLightSourceToy baseLightSourceToy)
        : base(baseLightSourceToy)
    {
        Dictionary.Add(baseLightSourceToy, this);
        Base = baseLightSourceToy;
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
    /// The <see cref="BaseLightSourceToy"/> object.
    /// </summary>
    public new BaseLightSourceToy Base { get; }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.intensity"/>.
    /// </summary>
    public float Intensity
    {
        get => Base.LightIntensity;
        set => Base.NetworkLightIntensity = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.range"/> in meters.
    /// </summary>
    public float Range
    {
        get => Base.LightRange;
        set => Base.NetworkLightRange = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.color"/>.
    /// </summary>
    public Color Color
    {
        get => Base.LightColor;
        set => Base.NetworkLightColor = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.shadows"/> type.
    /// </summary>
    public LightShadows ShadowType
    {
        get => Base.ShadowType;
        set => Base.NetworkShadowType = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.shadowStrength"/>.
    /// </summary>
    public float ShadowStrength
    {
        get => Base.ShadowStrength;
        set => Base.NetworkShadowStrength = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.type"/>.
    /// </summary>
    public LightType Type
    {
        get => Base.LightType;
        set => Base.NetworkLightType = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.shape"/>.
    /// </summary>
    public LightShape Shape
    {
        get => Base.LightShape;
        set => Base.NetworkLightShape = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.spotAngle"/>.
    /// </summary>
    public float SpotAngle
    {
        get => Base.SpotAngle;
        set => Base.NetworkSpotAngle = value;
    }

    /// <summary>
    /// Gets or sets the lights <see cref="Light.innerSpotAngle"/>.
    /// </summary>
    public float InnerSpotAngle
    {
        get => Base.InnerSpotAngle;
        set => Base.NetworkInnerSpotAngle = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[LightSourceToy: Type={Type}, Shape={Shape}, Intensity={Intensity}, Range={Range}, Color={Color}, ShadowType={ShadowType}, ShadowStrength={ShadowStrength}]";
    }

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static LightSourceToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static LightSourceToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static LightSourceToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new light source toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created light source toy.</returns>
    public static LightSourceToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        LightSourceToy toy = Get(Create<BaseLightSourceToy>(position, rotation, scale, parent));

        if (networkSpawn)
            toy.Spawn();

        return toy;
    }

    /// <summary>
    /// Gets the light source toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseLightSourceToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseLightSourceToy">The <see cref="Base"/> of the light source toy.</param>
    /// <returns>The requested light source toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseLightSourceToy))]
    public static LightSourceToy? Get(BaseLightSourceToy? baseLightSourceToy)
    {
        if (baseLightSourceToy == null)
            return null;

        return Dictionary.TryGetValue(baseLightSourceToy, out LightSourceToy item) ? item : (LightSourceToy)CreateAdminToyWrapper(baseLightSourceToy);
    }

    /// <summary>
    /// Tries to get the light source toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="baseLightSourceToy">The <see cref="Base"/> of the light source toy.</param>
    /// <param name="lightSourceToy">The requested light source toy.</param>
    /// <returns><see langword="True"/> if the light toy exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(BaseLightSourceToy? baseLightSourceToy, [NotNullWhen(true)] out LightSourceToy? lightSourceToy)
    {
        lightSourceToy = Get(baseLightSourceToy);
        return lightSourceToy != null;
    }
}
