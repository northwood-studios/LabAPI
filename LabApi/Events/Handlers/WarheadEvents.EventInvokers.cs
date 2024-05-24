using LabApi.Events.Arguments.WarheadEvents;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class WarheadEvents
{
    /// <summary>
    /// Invokes the <see cref="Starting"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WarheadStartingEventArgs"/> of the event.</param>
    public static void OnStarting(WarheadStartingEventArgs args) => Starting.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="Started"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WarheadStartedEventArgs"/> of the event.</param>
    public static void OnStarted(WarheadStartedEventArgs args) => Started.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="Stopping"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WarheadStoppingEventArgs"/> of the event.</param>
    public static void OnStopping(WarheadStoppingEventArgs args) => Stopping.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="Stopped"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WarheadStoppedEventArgs"/> of the event.</param>
    public static void OnStopped(WarheadStoppedEventArgs args) => Stopped.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="Detonating"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WarheadDetonatingEventArgs"/> of the event.</param>
    public static void OnDetonating(WarheadDetonatingEventArgs args) => Detonating.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="Detonated"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WarheadDetonatedEventArgs"/> of the event.</param>
    public static void OnDetonated(WarheadDetonatedEventArgs args) => Detonated.InvokeEvent(args);
}