using AdminToys;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper for the <see cref="Scp079CameraToy"/> class.
/// </summary>
public class CameraToy : AdminToy
{
    /// <summary>
    /// Contains all the camera toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public static new Dictionary<Scp079CameraToy, CameraToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="CameraToy"/>.
    /// </summary>
    public static new IReadOnlyCollection<CameraToy> List => Dictionary.Values;

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static CameraToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static CameraToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static CameraToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new camera toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created camera toy.</returns>
    public static CameraToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        CameraToy toy = Get(Create<Scp079CameraToy>(position, rotation, scale, parent));

        if (networkSpawn)
        {
            toy.Spawn();
        }

        return toy;
    }

    /// <summary>
    /// Gets the camera toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Scp079CameraToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseCameraToy">The <see cref="Base"/> of the camera toy.</param>
    /// <returns>The requested camera toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseCameraToy))]
    public static CameraToy? Get(Scp079CameraToy? baseCameraToy)
    {
        if (baseCameraToy == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseCameraToy, out CameraToy item) ? item : (CameraToy)CreateAdminToyWrapper(baseCameraToy);
    }

    /// <summary>
    /// Tries to get the camera toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="baseCameraToy">The <see cref="Base"/> of the camera toy.</param>
    /// <param name="cameraToy">The requested camera toy.</param>
    /// <returns>True if the camera toy exists, otherwise false.</returns>
    public static bool TryGet(Scp079CameraToy? baseCameraToy, [NotNullWhen(true)] out CameraToy? cameraToy)
    {
        cameraToy = Get(baseCameraToy);
        return cameraToy != null;
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseCameraToy">The base <see cref="Scp079CameraToy"/> object.</param>
    internal CameraToy(Scp079CameraToy baseCameraToy)
        : base(baseCameraToy)
    {
        Base = baseCameraToy;

        if (CanCache)
        {
            Dictionary.Add(baseCameraToy, this);
        }
    }

    /// <summary>
    /// The <see cref="Scp079CameraToy"/> object.
    /// </summary>
    public new Scp079CameraToy Base { get; }

    /// <summary>
    /// The camera instance associated with this toy.
    /// </summary>
    public Camera Camera => Camera.Get(Base.Camera);

    /// <summary>
    /// Gets or sets the label of the camera displayed to SCP-079 on HUD.
    /// </summary>
    public string Label
    {
        get => Base.Label;
        set => Base.NetworkLabel = value;
    }

    /// <summary>
    /// Gets or sets the room associated with this camera.
    /// </summary>
    /// <remarks>
    /// Room will never be <see langword="null"/>.
    /// This determines what cameras are visible to SCP-079 for what room.
    /// </remarks>
    public Room Room
    {
        get => Room.Get(Base.Room);
        set => Base.NetworkRoom = value.Base;
    }

    /// <summary>
    /// Gets or sets how high and low the camera can move from its initial rotation in degrees.
    /// </summary>
    /// <remarks>
    /// X should be less than or equal to y. e.g. <c>Vector2(-10, 30)</c> means you can look up 10 degrees and down 30.
    /// </remarks>
    public Vector2 VerticalConstraints
    {
        get => Base.VerticalConstraint;
        set => Base.NetworkVerticalConstraint = value;
    }

    /// <summary>
    /// Gets or sets how left and right the camera can move from its initial rotation in degrees.
    /// </summary>
    /// <remarks>
    /// X should be less than or equal to y. e.g. <c>Vector2(-10, 30)</c> means you can look left 10 degrees and right 30.
    /// </remarks>
    public Vector2 HorizontalConstraint
    {
        get => Base.HorizontalConstraint;
        set => Base.NetworkHorizontalConstraint = value;
    }

    /// <summary>
    /// Gets or set the min and max zoom level of the camera.
    /// </summary>
    /// <remarks>
    /// Values range from 0.0 to 1.0, with zero being the minimum zoom, and 1 being the maximum zoom.
    /// X should be less than or equal to y.
    /// </remarks>
    public Vector2 ZoomConstraints
    {
        get => Base.ZoomConstraint;
        set => Base.NetworkZoomConstraint = value;
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
