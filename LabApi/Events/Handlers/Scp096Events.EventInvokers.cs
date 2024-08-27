using LabApi.Events.Arguments.Scp096Events;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class Scp096Events
{
    /// <summary>
    /// Invokes the <see cref="AddingTarget"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096AddingTargetEventArgs"/> of the event.</param>
    public static void OnAddingTarget(Scp096AddingTargetEventArgs args) => AddingTarget.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="AddedTarget"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096AddedTargetEventArgs"/> of the event.</param>
    public static void OnAddedTarget(Scp096AddedTargetEventArgs args) => AddedTarget.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangingState"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096ChangingStateEventArgs"/> of the event.</param>
    public static void OnChangingState(Scp096ChangingStateEventArgs args) => ChangingState.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedState"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096ChangedStateEventArgs"/> of the event.</param>
    public static void OnChangedState(Scp096ChangedStateEventArgs args) => ChangedState.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Charging"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096ChargingEventArgs"/> of the event.</param>
    public static void OnCharging(Scp096ChargingEventArgs args) => Charging.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Charged"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096ChargedEventArgs"/> of the event.</param>
    public static void OnCharged(Scp096ChargedEventArgs args) => Charged.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Enraging"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096EnragingEventArgs"/> of the event.</param>
    public static void OnEnraging(Scp096EnragingEventArgs args) => Enraging.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Enraged"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096EnragedEventArgs"/> of the event.</param>
    public static void OnEnraged(Scp096EnragedEventArgs args) => Enraged.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PryingGate"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096PryingGateEventArgs"/> of the event.</param>
    public static void OnPryingGate(Scp096PryingGateEventArgs args) => PryingGate.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PriedGate"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096PriedGateEventArgs"/> of the event.</param>
    public static void OnPriedGate(Scp096PriedGateEventArgs args) => PriedGate.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="StartCrying"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096StartCryingEventArgs"/> of the event.</param>
    public static void OnStartCrying(Scp096StartCryingEventArgs args) => StartCrying.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="StartedCrying"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096StartedCryingEventArgs"/> of the event.</param>
    public static void OnStartedCrying(Scp096StartedCryingEventArgs args) => StartedCrying.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="TryingNotToCry"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096TryingNotToCryEventArgs"/> of the event.</param>
    public static void OnTryingNotToCry(Scp096TryingNotToCryEventArgs args) => TryingNotToCry.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="TriedNotToCry"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp096TriedNotToCryEventArgs"/> of the event.</param>
    public static void OnTriedNotToCry(Scp096TriedNotToCryEventArgs args) => TriedNotToCry.InvokeEvent(args);
}