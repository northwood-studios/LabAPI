using Generators;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Scp079Camera">SCP-079 cameras</see>.
/// </summary>
public class Camera
{
    /// <summary>
    /// Contains all the cached <see cref="Camera">cameras</see> in the game, accessible through their base <see cref="Scp079Camera"/>.
    /// </summary>
    public static Dictionary<Scp079Camera, Camera> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Camera"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Camera> List => Dictionary.Values;

    /// <summary>
    /// Gets the <see cref="Camera"/> associated with the <see cref="Scp079Camera"/>.
    /// </summary>
    /// <param name="camera">The <see cref="Scp079Camera"/> to get the camera from.</param>
    /// <returns>The <see cref="Camera"/> associated with the <see cref="Scp079Camera"/> or <see langword="null"/> if it doesn't exist.</returns>
    [return: NotNullIfNotNull(nameof(camera))]
    public static Camera? Get(Scp079Camera? camera)
    {
        if (camera == null)
        {
            return null;
        }

        return TryGet(camera, out Camera? cam) ? cam : new Camera(camera);
    }

    /// <summary>
    /// Tries to get the <see cref="Camera"/> associated with the <see cref="Scp079Camera"/>.
    /// </summary>
    /// <param name="camera">The <see cref="Scp079Camera"/> to get the camera from.</param>
    /// <param name="wrapper">The <see cref="Camera"/> associated with the <see cref="Scp079Camera"/> or <see langword="null"/> if it doesn't exist.</param>
    /// <returns>Whether the camera was successfully retrieved.</returns>
    public static bool TryGet(Scp079Camera camera, [NotNullWhen(true)] out Camera? wrapper)
    {
        wrapper = null;
        return camera != null && Dictionary.TryGetValue(camera, out wrapper);
    }

    /// <summary>
    /// Initializes the <see cref="Camera"/> class to subscribe to.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();

        Scp079Camera.OnInstanceCreated += (camera) => new Camera(camera);
        Scp079Camera.OnInstanceRemoved += (camera) => Remove(camera);
    }

    /// <summary>
    /// Handles the removal of a camera from the dictionary.
    /// </summary>
    /// <param name="camera">The camera to remove.</param>
    private static void Remove(Scp079Camera camera)
    {
        Dictionary.Remove(camera);
    }

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="camera">The <see cref="Scp079Camera"/>.</param>
    private Camera(Scp079Camera camera)
    {
        Dictionary.Add(camera, this);
        Base = camera;
    }

    /// <summary>
    /// The base <see cref="Scp079Camera"/>.
    /// </summary>
    public Scp079Camera Base { get; }

    /// <summary>
    /// Gets the camera's <see cref="GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the camera's position.
    /// </summary>
    public Vector3 Position => Base.Position;

    /// <summary>
    /// Gets or sets the camera's rotation.
    /// </summary>
    public Quaternion Rotation
    {
        get => Base.CameraAnchor.rotation;
        set => Base.CameraAnchor.rotation = value;
    }

    /// <summary>
    /// Gets the <see cref="Room"/> the camera is in.
    /// </summary>
    public Room Room => Room.Get(Base.Room);

    /// <summary>
    /// Gets or sets the zoom of the camera.
    /// </summary>
    public float Zoom => Base.ZoomAxis.CurrentZoom;

    /// <summary>
    /// Gets or sets a value indicating whether the camera is currently active.
    /// </summary>
    public bool IsBeingUsed
    {
        get => Base.IsActive;
        set => Base.IsActive = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[Camera: Position={Position}, Rotation={Rotation}, Room={Room}, Zoom={Zoom}, IsBeingUsed={IsBeingUsed}]";
    }
}