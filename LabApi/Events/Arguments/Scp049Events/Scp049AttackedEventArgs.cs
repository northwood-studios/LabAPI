using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.Attacked"/> event.
/// </summary>
public class Scp049AttackedEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049AttackedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-049 player instance.</param>
    /// <param name="target">The player that SCP-049 has attacked.</param>
    /// <param name="instantKill">The attack was an instant kill.</param>
    /// <param name="isSenseTarget">The <paramref name="target"/> was a sense target.</param>
    public Scp049AttackedEventArgs(ReferenceHub hub, ReferenceHub target, bool instantKill, bool isSenseTarget)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
        InstantKill = instantKill;
        IsSenseTarget = isSenseTarget;
    }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The player that SCP-049 has attacked.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets whether the attack was an instant kill.
    /// </summary>
    public bool InstantKill { get; }

    /// <summary>
    /// Gets whether the <see cref="Target"/> is a sense target.
    /// </summary>
    public bool IsSenseTarget { get; }
}