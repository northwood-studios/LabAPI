using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.Enraged"/> event.
/// </summary>
public class Scp096EnragedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096ChargingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    /// <param name="initialDuration">The initial duration of the rage.</param>
    public Scp096EnragedEventArgs(Player player, float initialDuration)
    {
        Player = player;
        InitialDuration = initialDuration;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The initial duration of the rage.
    /// </summary>
    public float InitialDuration { get; }
}
