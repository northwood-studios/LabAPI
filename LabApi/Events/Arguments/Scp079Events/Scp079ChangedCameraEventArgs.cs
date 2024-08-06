using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp079.Cameras;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.ChangedCamera"/> event.
/// </summary>
public class Scp079ChangedCameraEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079ChangedCameraEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="camera">The affected camera instance.</param>
    public Scp079ChangedCameraEventArgs(Player player, Scp079Camera camera)
    {
        Player = player;
        Camera = camera;
    }
    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected camera instance.
    /// </summary>
    public Scp079Camera Camera { get; }
}
