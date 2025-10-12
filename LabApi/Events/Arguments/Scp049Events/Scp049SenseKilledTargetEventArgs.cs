using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.SenseKilledTarget"/> event.
/// </summary>
public class Scp049SenseKilledTargetEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049SenseKilledTargetEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-049 player instance.</param>
    /// <param name="target">The player that SCP-049 killed.</param>
    public Scp049SenseKilledTargetEventArgs(ReferenceHub hub, ReferenceHub target)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
    }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The player that SCP-049 has killed.
    /// </summary>
    public Player Target { get; }
}