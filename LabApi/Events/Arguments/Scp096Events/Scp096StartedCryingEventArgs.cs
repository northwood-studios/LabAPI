using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.StartedCrying"/> event.
/// </summary>
public class Scp096StartedCryingEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096StartedCryingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    public Scp096StartedCryingEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }
}
