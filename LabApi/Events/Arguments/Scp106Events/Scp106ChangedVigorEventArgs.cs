using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.ChangedVigor"/> event.
/// </summary>
public class Scp106ChangedVigorEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106ChangedVigorEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-106 player instance.</param>
    /// <param name="oldVigor">The previous vigor value.</param>
    /// <param name="newVigor">The new vigor value.</param>
    public Scp106ChangedVigorEventArgs(ReferenceHub hub, float oldVigor, float newVigor)
    {
        Player = Player.Get(hub);
        OldValue = oldVigor;
        Value = newVigor;
    }

    /// <summary>
    /// The previous vigor value.
    /// </summary>
    public float OldValue { get; }

    /// <summary>
    /// The new vigor value.
    /// </summary>
    public float Value { get; }

    /// <summary>
    /// The SCP-106 player instance.
    /// </summary>
    public Player Player { get; }
}