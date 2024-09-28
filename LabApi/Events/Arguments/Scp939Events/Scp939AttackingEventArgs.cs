using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 is attacking a player.
/// </summary>
public class Scp939AttackingEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939AttackingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-939 player instance.</param>
    /// <param name="target">The destructible that is being attacked.</param>
    public Scp939AttackingEventArgs(ReferenceHub player, IDestructible target)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Target = target;
    }

    /// <summary>
    /// The destructible that is being attacked.
    /// </summary>
    public IDestructible Target { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// The SCP-939 player instance.
    /// </summary>
    public Player Player { get; }
}