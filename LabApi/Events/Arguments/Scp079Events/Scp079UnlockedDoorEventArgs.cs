using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.UnlockedDoor"/> event.
/// </summary>
public class Scp079UnlockedDoorEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079UnlockedDoorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="door">The affected door instance.</param>
    public Scp079UnlockedDoorEventArgs(Player player, Door door)
    {
        Player = player;
        Door = door;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The affected door instance.
    /// </summary>
    public Door Door { get; }
}
