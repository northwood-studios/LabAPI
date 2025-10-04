using LabApi.Events.Arguments.ScpEvents;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCPs.
/// </summary>
public static partial class ScpEvents
{
    /// <summary>
    /// Gets called when an SCP has their hume shield broken.
    /// </summary>
    public static event LabEventHandler<ScpHumeShieldBrokenEventArgs>? HumeShieldBroken;
}