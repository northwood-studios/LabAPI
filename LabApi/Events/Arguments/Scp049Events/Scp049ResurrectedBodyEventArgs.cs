using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.ResurrectedBody"/> event.
/// </summary>
public class Scp049ResurrectedBodyEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049ResurrectedBodyEventArgs"/> class.
    /// </summary>
    /// <param name="target">The player that SCP-049 has resurrected.</param>
    /// <param name="player">The SCP-049 player instance.</param>
    public Scp049ResurrectedBodyEventArgs(ReferenceHub target, ReferenceHub player)
    {
        Target = Player.Get(target);
        Player = Player.Get(player);
    }

    /// <summary>
    /// The player that SCP-049 has resurrected.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }
}