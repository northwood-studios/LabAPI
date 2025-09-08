using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.TryingNotToCry"/> event.
/// </summary>
public class Scp096TryingNotToCryEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096TryingNotToCryEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-096 player instance.</param>
    public Scp096TryingNotToCryEventArgs(ReferenceHub hub)
    {
        Player = Player.Get(hub);
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
