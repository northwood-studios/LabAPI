using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.InteractingWarheadLever"/> event.
/// </summary>
public class PlayerInteractingWarheadLeverEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInteractedWarheadLeverEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is interacting with the lever.</param>
    /// <param name="enabled">Bool whether the warhead should be enabled and can be started.</param>
    public PlayerInteractingWarheadLeverEventArgs(ReferenceHub player, bool enabled)
    {
        Player = Player.Get(player);
        Enabled = enabled;

        IsAllowed = true;
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets whether the warhead should be enabled.
    /// </summary>
    public bool Enabled { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
