using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Ragdolls;
using System;
namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.ResurrectedBody"/> event.
/// </summary>
public class Scp049StartingResurrectionEventArgs : EventArgs, ICancellableEvent, IPlayerEvent, ITargetEvent, IRagdollEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049StartingResurrectionEventArgs"/> class.
    /// </summary>
    /// <param name="canResurrect">Whether SCP-049 can resurrect the ragdoll.</param>
    /// <param name="ragdoll">The ragdoll that SCP-049 is resurrecting.</param>
    /// <param name="target">The owner of the ragdoll that SCP-049 is resurrecting.</param>
    /// <param name="player">The SCP-049 player instance.</param>
    public Scp049StartingResurrectionEventArgs(bool canResurrect, BasicRagdoll ragdoll, ReferenceHub? target, ReferenceHub player)
    {
        CanResurrect = canResurrect;
        Ragdoll = Ragdoll.Get(ragdoll);
        Target = Player.Get(target);
        Player = Player.Get(player);
        IsAllowed = true;
    }

    /// <summary>
    /// Whether SCP-049 can resurrect the ragdoll.
    /// </summary>
    public bool CanResurrect { get; set; }

    /// <summary>
    /// The ragdoll that SCP-049 is resurrecting.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// The owner of the ragdoll that SCP-049 is resurrecting.
    /// </summary>
    public Player? Target { get; }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}