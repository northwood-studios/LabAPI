using LabApi.Events.Arguments.Scp173Events;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class Scp173Events
{
    /// <summary>
    /// Invokes the <see cref="BreakneckSpeedChanging"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173BreakneckSpeedChangingEventArgs"/> of the event.</param>
    public static void OnBreakneckSpeedChanging(Scp173BreakneckSpeedChangingEventArgs ev) => BreakneckSpeedChanging.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="BreakneckSpeedChanged"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173BreakneckSpeedChangedEventArgs"/> of the event.</param>
    public static void OnBreakneckSpeedChanged(Scp173BreakneckSpeedChangedEventArgs ev) => BreakneckSpeedChanged.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="AddingObserver"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173AddingObserverEventArgs"/> of the event.</param>
    public static void OnAddingObserver(Scp173AddingObserverEventArgs ev) => AddingObserver.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="AddedObserver"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173AddedObserverEventArgs"/> of the event.</param>
    public static void OnAddedObserver(Scp173AddedObserverEventArgs ev) => AddedObserver.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="RemovingObserver"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173RemovingObserverEventArgs"/> of the event.</param>
    public static void OnRemovingObserver(Scp173RemovingObserverEventArgs ev) => RemovingObserver.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="RemovedObserver"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173RemovedObserverEventArgs"/> of the event.</param>
    public static void OnRemovedObserver(Scp173RemovedObserverEventArgs ev) => RemovedObserver.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="CreatingTantrum"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173CreatingTantrumEventArgs"/> of the event.</param>
    public static void OnCreatingTantrum(Scp173CreatingTantrumEventArgs ev) => CreatingTantrum.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="CreatedTantrum"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173CreatedTantrumEventArgs"/> of the event.</param>
    public static void OnCreatedTantrum(Scp173CreatedTantrumEventArgs ev) => CreatedTantrum.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="PlayingSound"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173PlayingSoundEventArgs"/> of the event.</param>
    public static void OnPlayingSound(Scp173PlayingSoundEventArgs ev) => PlayingSound.InvokeEvent(ev);

    /// <summary>
    /// Invokes the <see cref="PlayedSound"/> event.
    /// </summary>
    /// <param name="ev">The <see cref="Scp173PlayedSoundEventArgs"/> of the event.</param>
    public static void OnPlayedSound(Scp173PlayedSoundEventArgs ev) => PlayedSound.InvokeEvent(ev);
}