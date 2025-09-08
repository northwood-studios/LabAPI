using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Uncuffing"/> event.
/// </summary>
public class PlayerUncuffingEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUncuffingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is uncuffing another player.</param>
    /// <param name="target">The player who is being uncuffed.</param>
    /// <param name="canUnDetainAsScp">Whenever the player can undetain as SCP player</param>
    public PlayerUncuffingEventArgs(ReferenceHub hub, ReferenceHub target, bool canUnDetainAsScp)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Target = Player.Get(target);
        CanUnDetainAsScp = canUnDetainAsScp;
    }

    /// <summary>
    /// Gets the player who is uncuffing another player.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player who is being uncuffed.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// Gets whether the player can undetain as SCP.
    /// </summary>
    public bool CanUnDetainAsScp { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}