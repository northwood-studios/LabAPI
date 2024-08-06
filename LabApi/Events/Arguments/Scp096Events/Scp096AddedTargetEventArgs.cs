using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.AddedTarget"/> event.
/// </summary>
public class Scp096AddedTargetEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096AddedTargetEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    /// <param name="target">The target player instance.</param>
    /// <param name="look">Whether the target looked at SCP-096.</param>
    public Scp096AddedTargetEventArgs(Player player, Player target, bool look)
    {
        Player = player;
        Target = target;
        Look = look;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target player instance.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Whether the target looked at SCP-096.
    /// </summary>
    public bool Look { get; }
}
