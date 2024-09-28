using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangedSpectator"/> event.
/// </summary>
public class PlayerChangedSpectatorEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerChangedSpectatorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who changed spectator.</param>
    /// <param name="oldTarget">Old target that was spectated previously.</param>
    /// <param name="newTarget">New target that was spectating changed to.</param>
    public PlayerChangedSpectatorEventArgs(ReferenceHub player, ReferenceHub oldTarget, ReferenceHub newTarget)
    {
        Player = Player.Get(player);
        OldTarget = Player.Get(oldTarget);
        NewTarget = Player.Get(newTarget);
    }

    /// <summary>
    /// Gets the player who changed spectator.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets old target that was spectated previously.
    /// </summary>
    public Player OldTarget { get; }

    /// <summary>
    /// Gets the new target that was spectating changed to.
    /// </summary>
    public Player NewTarget { get; }
}