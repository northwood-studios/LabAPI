using LabApi.Events.Arguments.Scp3114Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCP-3114.
/// </summary>
public static partial class Scp3114Events
{
    /// <summary>
    /// Gets called when SCP-3114 is starting to strangle a player.
    /// </summary>
    public static event LabEventHandler<Scp3114StrangleStartingEventArgs>? StrangleStarting;

    /// <summary>
    /// Gets called when SCP-3114 has started to strangle a player.
    /// </summary>
    public static event LabEventHandler<Scp3114StrangleStartedEventArgs>? StrangleStarted;

    /// <summary>
    /// Gets called when SCP-3114 is aborting their strangle on the player.
    /// </summary>
    public static event LabEventHandler<Scp3114StrangleAbortingEventArgs>? StrangleAborting;

    /// <summary>
    /// Gets called when SCP-3114 has aborted their strangle on the player.
    /// </summary>
    public static event LabEventHandler<Scp3114StrangleAbortedEventArgs>? StrangleAborted;
}