using System;
using LabApi.Events.Arguments.Interfaces;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.RoundStarting"/> event.
/// </summary>
public class RoundStartingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoundStartingEventArgs"/> class.
    /// </summary>
    public RoundStartingEventArgs()
    {
        IsAllowed = true;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}