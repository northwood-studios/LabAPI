using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp939;
using System;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 has lunged.
/// </summary>
public class Scp939LungedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939LungedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-939 player instance.</param>
    /// <param name="lungeState">The SCP-939 lunge state.</param>
    public Scp939LungedEventArgs(ReferenceHub hub, Scp939LungeState lungeState)
    {
        Player = Player.Get(hub);
        LungeState = lungeState;
    }

    /// <summary>
    /// The 939 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the current state of the SCP-939 lunge ability.
    /// </summary>
    public Scp939LungeState LungeState { get; }
}