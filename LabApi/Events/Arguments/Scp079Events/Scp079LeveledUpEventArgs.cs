using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.LeveledUp"/> event.
/// </summary>
public class Scp079LeveledUpEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079LeveledUpEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-079 player instance.</param>
    /// <param name="tier">The new SCP-079's tier.</param>
    public Scp079LeveledUpEventArgs(ReferenceHub hub, int tier)
    {
        Player = Player.Get(hub);
        Tier = tier;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The new SCP-079's tier.
    /// </summary>
    public int Tier { get; }
}
