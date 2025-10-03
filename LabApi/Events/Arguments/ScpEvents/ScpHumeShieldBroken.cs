using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.ScpEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ScpEvents.ScpHumeShieldBroken"/> event.
/// </summary>
public class ScpHumeShieldBroken : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScpHumeShieldBroken"/> class.
    /// </summary>
    /// <param name="player">The player SCP whom hume shield broke.</param>
    public ScpHumeShieldBroken(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <inheritdoc/>
    public Player Player { get; }
}