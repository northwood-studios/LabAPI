using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Uncuffed"/> event.
/// </summary>
public class PlayerUncuffedEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUncuffedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who uncuffed target player.</param>
    /// <param name="target">The player who was uncuffed.</param>
    /// <param name="canUnDetainAsScp">Whenever the player can undetain as SCP player.</param>
    public PlayerUncuffedEventArgs(ReferenceHub hub, ReferenceHub target, bool canUnDetainAsScp)
    {
        Player = Player.Get(hub);
        Target = Player.Get(target);
        CanUnDetainAsScp = canUnDetainAsScp;
    }

    /// <summary>
    /// Gets the player who uncuffed target player.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who was uncuffed.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets whether the player can undetain as SCP player.
    /// </summary>
    public bool CanUnDetainAsScp { get; }
}