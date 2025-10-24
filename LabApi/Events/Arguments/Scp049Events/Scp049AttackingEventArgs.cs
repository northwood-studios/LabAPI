using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.Attacking"/> event.
/// </summary>
public class Scp049AttackingEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049AttackingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-049 player instance.</param>
    /// <param name="target">The player that SCP-049 is attacking.</param>
    /// <param name="instantKill">The attack is an instant kill.</param>
    /// <param name="isSenseTarget">The <paramref name="target"/> is a sense target.</param>
    /// <param name="cooldownTime">The attack cooldown.</param>
    public Scp049AttackingEventArgs(ReferenceHub hub, ReferenceHub target, bool instantKill, bool isSenseTarget, float cooldownTime)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
        InstantKill = instantKill;
        IsSenseTarget = isSenseTarget;
        CooldownTime = cooldownTime;

        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The player that SCP-049 is attacking.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets or sets whether the attack is an instant kill.
    /// </summary>
    public bool InstantKill { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="Target"/> is a sense target.
    /// </summary>
    public bool IsSenseTarget { get; set; }

    /// <summary>
    /// Gets or sets the cooldown for the attack ability.
    /// </summary>
    public float CooldownTime { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}