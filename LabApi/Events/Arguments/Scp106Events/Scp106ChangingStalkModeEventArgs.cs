using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.ChangingStalkMode"/> event.
/// </summary>
public class Scp106ChangingStalkModeEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106ChangingStalkModeEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-106 player instance.</param>
    /// <param name="active">Whether the ability is being activated or deactivated.</param>
    public Scp106ChangingStalkModeEventArgs(ReferenceHub player, bool active)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        IsStalkActive = active;
    }

    /// <summary>
    /// Whether the ability is being activated or deactivated.
    /// </summary>
    public bool IsStalkActive { get; }

    /// <summary>
    /// The SCP-106 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}