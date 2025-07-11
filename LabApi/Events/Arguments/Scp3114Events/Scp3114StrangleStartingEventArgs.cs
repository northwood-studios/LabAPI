using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp3114Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp3114Events.StrangleStarting"/> events.
/// </summary>
public class Scp3114StrangleStartingEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp3114StrangleStartingEventArgs"/> class.
    /// </summary>
    /// <param name="scp3114Hub">The <see cref="ReferenceHub"/> component of the SCP-3114 player.</param>
    /// <param name="targetHub">The <see cref="ReferenceHub"/> component of the target player.</param>
    public Scp3114StrangleStartingEventArgs(ReferenceHub scp3114Hub, ReferenceHub targetHub)
    {
        Player = Player.Get(scp3114Hub);
        Target = Player.Get(targetHub);
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-3114 player.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The strangle target.
    /// </summary>
    public Player Target { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
