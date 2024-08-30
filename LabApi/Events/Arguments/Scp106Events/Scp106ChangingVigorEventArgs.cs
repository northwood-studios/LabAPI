using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.ChangingVigor"/> event.
/// </summary>
public class Scp106ChangingVigorEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106ChangingVigorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-106 player instance.</param>
    /// <param name="oldValue">The previous vigor value.</param>
    /// <param name="value">The new vigor value.</param>
    public Scp106ChangingVigorEventArgs(Player player, float oldValue, float value)
    {
        Player = player;
        OldValue = oldValue;
        Value = value;
        IsAllowed = true;
    }

    /// <summary>
    /// The previous vigor value.
    /// </summary>
    public float OldValue { get; }

    /// <summary>
    /// The new vigor value.
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// The SCP-106 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}