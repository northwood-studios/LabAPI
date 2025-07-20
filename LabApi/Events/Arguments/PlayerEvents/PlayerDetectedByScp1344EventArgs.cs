using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DetectedByScp1344"/> event.
/// </summary>
public class PlayerDetectedByScp1344EventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDetectedByScp1344EventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who has SCP-1344.</param>
    /// <param name="target">The player who has been detected.</param>
    public PlayerDetectedByScp1344EventArgs(ReferenceHub player, ReferenceHub target)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <inheritdoc/>
    public Player Target { get; set; }
}
