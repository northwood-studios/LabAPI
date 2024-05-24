using LabApi.Events.Arguments.WarheadEvents;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all the events related to the warhead.
/// </summary>
public static partial class WarheadEvents
{
    /// <summary>
    /// Gets called when the warhead countdown is starting.
    /// </summary>
    public static event LabEventHandler<WarheadStartingEventArgs> Starting;
    
    /// <summary>
    /// Gets called when the warhead countdown has started.
    /// </summary>
    public static event LabEventHandler<WarheadStartedEventArgs> Started;

    /// <summary>
    /// Gets called when the warhead countdown is stopping.
    /// </summary>
    public static event LabEventHandler<WarheadStoppingEventArgs> Stopping;
    
    /// <summary>
    /// Gets called when the warhead countdown has stopped.
    /// </summary>
    public static event LabEventHandler<WarheadStoppedEventArgs> Stopped;
    
    /// <summary>
    /// Gets called when the warhead is being detonated.
    /// </summary>
    public static event LabEventHandler<WarheadDetonatingEventArgs> Detonating;
    
    /// <summary>
    /// Gets called when the warhead has been detonated.
    /// </summary>
    public static event LabEventHandler<WarheadDetonatedEventArgs> Detonated;
}