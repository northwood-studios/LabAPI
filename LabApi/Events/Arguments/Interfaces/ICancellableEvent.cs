namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that can be cancelled.
/// </summary>
public interface ICancellableEvent
{
    /// <summary>
    /// Whether the event is allowed to run.
    /// </summary>
    public bool IsAllowed { get; set; }
}