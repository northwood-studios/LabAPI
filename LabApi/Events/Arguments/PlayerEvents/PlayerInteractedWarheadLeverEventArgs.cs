using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractedWarheadLever"/> event.
/// </summary>
public class PlayerInteractedWarheadLeverEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedWarheadLeverEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who interacted with the lever.</param>
    /// <param name="enabled">Bool whether the warhead is enabled and can be started.</param>
    public PlayerInteractedWarheadLeverEventArgs(ReferenceHub player, bool enabled)
    {
        Player = Player.Get(player);
        Enabled = enabled;
    }

    /// <inheritdoc />
    public Player Player { get; }

    /// <summary>
    /// Gets whether the warhead is now enabled.
    /// </summary>
    public bool Enabled { get; }
}
