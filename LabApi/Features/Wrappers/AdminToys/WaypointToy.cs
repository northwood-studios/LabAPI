using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BaseWaypointToy = AdminToys.WaypointToy;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseWaypointToy"/> class.
/// </summary>
public class WaypointToy : AdminToy
{
    /// <summary>
    /// Max distance in meters a waypoint can encapsulate along any dimension.
    /// </summary>
    public const float MaxBounds = BaseWaypointToy.MaxBounds;

    /// <summary>
    /// Contains all the waypoint toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public static new Dictionary<BaseWaypointToy, WaypointToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="WaypointToy"/>.
    /// </summary>
    public static new IReadOnlyCollection<WaypointToy> List => Dictionary.Values;

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static WaypointToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static WaypointToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static WaypointToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new waypoint toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created waypoint toy.</returns>
    public static WaypointToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        WaypointToy toy = Get(Create<BaseWaypointToy>(position, rotation, scale, parent));

        if (networkSpawn)
        {
            toy.Spawn();
        }

        return toy;
    }

    /// <summary>
    /// Gets the waypoint toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseWaypointToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseWaypointToy">The <see cref="Base"/> of the waypoint toy.</param>
    /// <returns>The requested waypoint toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseWaypointToy))]
    public static WaypointToy? Get(BaseWaypointToy? baseWaypointToy)
    {
        if (baseWaypointToy == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseWaypointToy, out WaypointToy item) ? item : (WaypointToy)CreateAdminToyWrapper(baseWaypointToy);
    }

    /// <summary>
    /// Tries to get the waypoint toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="baseWaypointToy">The <see cref="Base"/> of the waypoint toy.</param>
    /// <param name="waypointToy">The requested waypoint toy.</param>
    /// <returns><see langword="True"/> if the waypoint exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(BaseWaypointToy? baseWaypointToy, [NotNullWhen(true)] out WaypointToy? waypointToy)
    {
        waypointToy = Get(baseWaypointToy);
        return waypointToy != null;
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseWaypointToy">The base <see cref="BaseWaypointToy"/> object.</param>
    internal WaypointToy(BaseWaypointToy baseWaypointToy)
        : base(baseWaypointToy)
    {
        Dictionary.Add(baseWaypointToy, this);
        Base = baseWaypointToy;
    }

    /// <summary>
    /// The <see cref="BaseWaypointToy"/> object.
    /// </summary>
    public new BaseWaypointToy Base { get; }

    /// <inheritdoc />
    public override Vector3 Position
    {
        get => base.Position;
        set
        {
            base.Position = value;
            Base.UpdateWaypointChildren();
        }
    }

    /// <inheritdoc />
    public override Quaternion Rotation
    {
        get => base.Rotation;
        set
        {
            base.Rotation = value;
            Base.UpdateWaypointChildren();
        }
    }

    /// <summary>
    /// Gets or sets the scale on the waypoint toy.
    /// Does not effect the bounds of the waypoint, use <see cref="BoundsSize"/> instead.
    /// </summary>
    /// <remarks>
    /// Scale can cause unindented side effects when used on a waypoint toy.
    /// </remarks>
    public override Vector3 Scale
    {
        get => base.Scale;
        set
        {
            base.Scale = value;

            if (value != Vector3.one)
            {
                Console.Logger.Warn("Setting scale on WaypointToy is not supported and may causes problems.");
            }
        }
    }

    /// <summary>
    /// Bounds the waypoint encapsulates along each dimension in meters.
    /// Bounds is effected by position and rotation of the GameObject but not its scale.
    /// Must not exceed <c>Vector3.one * MaxBounds</c>.
    /// </summary>
    /// <remarks>
    /// When <see cref="AdminToy.IsStatic"/> is <see langword="true"/> rotation and <see cref="BoundsSize"/> is not used, instead the bounds is axis aligned and its size is fixed at <see cref="MaxBounds"/>.
    /// </remarks>
    public Vector3 BoundsSize
    {
        get => Base.BoundsSize;
        set => Base.NetworkBoundsSize = value;
    }

    /// <summary>
    /// Gets or sets whether to visualize the waypoint's maximum bounds.
    /// </summary>
    public bool VisualizeBounds
    {
        get => Base.VisualizeBounds;
        set => Base.NetworkVisualizeBounds = value;
    }

    /// <summary>
    /// Gets or sets how many meters to bias towards this waypoint.
    /// </summary>
    /// <remarks>
    /// The closest waypoint is determined by its square distance.
    /// When set this takes away <c>(Priority * Priority)</c> from the sqr distance.
    /// </remarks>
    public float PriorityBias
    {
        get => Base.Priority;
        set => Base.NetworkPriority = value;
    }

    /// <summary>
    /// Force update all waypoint children to be up to date with the current position and rotation of the waypoint.
    /// Call this when ever the waypoint is moved by a parent object or the waypoint is moved using base game APIs or external APIs.
    /// </summary>
    /// <remarks>
    /// Does not work if the waypoint is <see cref="AdminToy.IsStatic"/>.
    /// </remarks>
    public void UpdateWaypointChildren() => Base.UpdateWaypointChildren();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[WaypointToy: Position{Position}, VisualizeBounds:{VisualizeBounds}, PriorityBias:{PriorityBias}]";
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
