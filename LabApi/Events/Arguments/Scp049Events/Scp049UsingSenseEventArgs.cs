using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.UsingSense"/> event.
/// </summary>
public class Scp049UsingSenseEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049UsingSenseEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-049 player instance.</param>
    /// <param name="target">The player that SCP-049 is using sense on.</param>
    public Scp049UsingSenseEventArgs(ReferenceHub player, ReferenceHub target)
    {
        Player = Player.Get(player);
        Target = Player.Get(target);
        IsAllowed = true;
    }

    /// <summary>
    /// Gets or sets the player that SCP-049 is using sense on.
    /// </summary>
    public Player Target { get; set; }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}