using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.ChangedCamera"/> event.
/// </summary>
public class Scp079ChangedCameraEventArgs : EventArgs, IPlayerEvent, ICameraEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079ChangedCameraEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-079 player instance.</param>
    /// <param name="camera">The affected camera instance.</param>
    public Scp079ChangedCameraEventArgs(ReferenceHub hub, Scp079Camera camera)
    {
        Player = Player.Get(hub);
        Camera = Camera.Get(camera);
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected camera instance.
    /// </summary>
    public Camera Camera { get; set; }
}
