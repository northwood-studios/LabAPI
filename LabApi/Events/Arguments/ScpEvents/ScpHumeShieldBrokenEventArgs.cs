using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ScpEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ScpEvents.HumeShieldBroken"/> event.
/// </summary>
public class ScpHumeShieldBrokenEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScpHumeShieldBrokenEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player SCP whom hume shield broke.</param>
    public ScpHumeShieldBrokenEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <inheritdoc/>
    public Player Player { get; }
}