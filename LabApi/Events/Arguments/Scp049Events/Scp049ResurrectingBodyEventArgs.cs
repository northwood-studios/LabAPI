using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.ResurrectingBody"/> event.
/// </summary>
public class Scp049ResurrectingBodyEventArgs : EventArgs, ICancellableEvent, IPlayerEvent, ITargetEvent, IRagdollEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049ResurrectingBodyEventArgs"/> class.
    /// </summary>
    /// <param name="ragdoll">The ragdoll that SCP-049 is resurrecting.</param>
    /// <param name="target">The player that SCP-049 is resurrecting.</param>
    /// <param name="player">The SCP-049 player instance.</param>
    public Scp049ResurrectingBodyEventArgs(Ragdoll ragdoll, ReferenceHub target, ReferenceHub player)
    {
        Ragdoll = ragdoll;
        Target = Player.Get(target);
        Player = Player.Get(player);
        IsAllowed = true;
    }

    /// <summary>
    /// The ragdoll that SCP-049 is resurrecting.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// The player that SCP-049 is resurrecting.
    /// </summary>
    public Player Target { get; set; }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}