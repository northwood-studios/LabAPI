using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.DeadmanSequenceActivating"/> event.
/// </summary>
public class DeadmanSequenceActivatingEventArgs : EventArgs, ICancellableEvent
{
    /// <inheritdoc />
    /// <remarks>Will reset Deadman Sequence timer back to 0 if <see langword="false"/>.</remarks>
    public bool IsAllowed { get; set; } = true;
}