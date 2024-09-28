using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 has attacked a player.
/// </summary>
public class Scp939AttackedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939AttackedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-939 player instance.</param>
    /// <param name="target">The destructible that was attacked.</param>
    public Scp939AttackedEventArgs(ReferenceHub player, IDestructible target)
    {
        Player = Player.Get(player);
        Target = target;
    }

    /// <summary>
    /// The destructible that was attacked.
    /// </summary>
    public IDestructible Target { get; }

    /// <summary>
    /// The SCP-939 player instance.
    /// </summary>
    public Player Player { get; }
}