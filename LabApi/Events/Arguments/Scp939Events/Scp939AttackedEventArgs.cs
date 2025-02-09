using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 has attacked a player.
/// </summary>
public class Scp939AttackedEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939AttackedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-939 player instance.</param>
    /// <param name="target">The destructible that was attacked.</param>
    /// <param name="damage">The damage dealt.</param>
    public Scp939AttackedEventArgs(ReferenceHub player, ReferenceHub target, float damage)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
        Damage = damage;
    }

    /// <summary>
    /// The SCP-939 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The destructible that is being attacked.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// The damage dealt.
    /// </summary>
    public float Damage { get; set; }
}