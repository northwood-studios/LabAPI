using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.LczDecontaminationStarting"/> event.
/// </summary>
public class LczDecontaminationStartingEventArgs : EventArgs, ICancellableEvent
{    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
