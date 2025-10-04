using LabApi.Events.Arguments.Scp173Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all the events related to SCP-173.
/// </summary>
public static partial class Scp173Events
{
    /// <summary>
    /// Gets called when the breakneck speed of SCP-173 is changing.
    /// </summary>
    public static event LabEventHandler<Scp173BreakneckSpeedChangingEventArgs>? BreakneckSpeedChanging;

    /// <summary>
    /// Gets called when the breakneck speed of SCP-173 has changed.
    /// </summary>
    public static event LabEventHandler<Scp173BreakneckSpeedChangedEventArgs>? BreakneckSpeedChanged;

    /// <summary>
    /// Gets called when a new observer is being added to SCP-173.
    /// </summary>
    public static event LabEventHandler<Scp173AddingObserverEventArgs>? AddingObserver;

    /// <summary>
    /// Gets called when a new observer has been added to SCP-173.
    /// </summary>
    public static event LabEventHandler<Scp173AddedObserverEventArgs>? AddedObserver;

    /// <summary>
    /// Gets called when an observer is being removed from SCP-173.
    /// </summary>
    public static event LabEventHandler<Scp173RemovingObserverEventArgs>? RemovingObserver;

    /// <summary>
    /// Gets called when an observer has been removed from SCP-173.
    /// </summary>
    public static event LabEventHandler<Scp173RemovedObserverEventArgs>? RemovedObserver;

    /// <summary>
    /// Gets called when SCP-173 is creating a tantrum.
    /// </summary>
    public static event LabEventHandler<Scp173CreatingTantrumEventArgs>? CreatingTantrum;

    /// <summary>
    /// Gets called when SCP-173 has created a tantrum.
    /// </summary>
    public static event LabEventHandler<Scp173CreatedTantrumEventArgs>? CreatedTantrum;

    /// <summary>
    /// Gets called when SCP-173 is playing a sound.
    /// </summary>
    public static event LabEventHandler<Scp173PlayingSoundEventArgs>? PlayingSound;

    /// <summary>
    /// Gets called when SCP-173 has played a sound.
    /// </summary>
    public static event LabEventHandler<Scp173PlayedSoundEventArgs>? PlayedSound;

    /// <summary>
    /// Gets called when SCP-173 is teleporting.
    /// </summary>
    public static event LabEventHandler<Scp173TeleportingEventArgs>? Teleporting;

    /// <summary>
    /// Gets called when SCP-173 has teleported.
    /// </summary>
    public static event LabEventHandler<Scp173TeleportedEventArgs>? Teleported;

    /// <summary>
    /// Gets called when SCP-173 is attempting to snap a target.
    /// </summary>
    public static event LabEventHandler<Scp173SnappingEventArgs>? Snapping;

    /// <summary>
    /// Gets called when SCP-173 has snapped a target.
    /// </summary>
    public static event LabEventHandler<Scp173SnappedEventArgs>? Snapped;
}