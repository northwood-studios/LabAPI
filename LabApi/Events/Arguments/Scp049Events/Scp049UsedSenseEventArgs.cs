using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.UsedSense"/> event.
/// </summary>
public class Scp049UsedSenseEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049UsedSenseEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-049 player instance.</param>
    /// <param name="target">The player that SCP-049 has used sense on.</param>
    public Scp049UsedSenseEventArgs(ReferenceHub hub, ReferenceHub target)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
    }

    /// <summary>
    /// The player that SCP-049 has used sense on.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }
}