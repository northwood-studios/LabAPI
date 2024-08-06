using LabApi.Events.Arguments.Scp096Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCP-096.
/// </summary>
public static partial class Scp096Events
{
    /// <summary>
    /// Gets called when SCP-096 is getting a target added.
    /// </summary>
    public static event LabEventHandler<Scp096AddingTargetEventArgs> AddingTarget;

    /// <summary>
    /// Gets called when SCP-096 has got a target added.
    /// </summary>
    public static event LabEventHandler<Scp096AddedTargetEventArgs> AddedTarget;

    /// <summary>
    /// Gets called when SCP-096 is changing a state.
    /// </summary>
    public static event LabEventHandler<Scp096ChangingStateEventArgs> ChangingState;

    /// <summary>
    /// Gets called when SCP-096 has changed a state.
    /// </summary>
    public static event LabEventHandler<Scp096ChangedStateEventArgs> ChangedState;

    /// <summary>
    /// Gets called when SCP-096 is charging.
    /// </summary>
    public static event LabEventHandler<Scp096ChargingEventArgs> Charging;

    /// <summary>
    /// Gets called when SCP-096 has charged.
    /// </summary>
    public static event LabEventHandler<Scp096ChargedEventArgs> Charged;

    /// <summary>
    /// Gets called when SCP-096 is enraging.
    /// </summary>
    public static event LabEventHandler<Scp096EnragingEventArgs> Enraging;

    /// <summary>
    /// Gets called when SCP-096 has enraged.
    /// </summary>
    public static event LabEventHandler<Scp096EnragedEventArgs> Enraged;

    /// <summary>
    /// Gets called when SCP-096 is prying a gate.
    /// </summary>
    public static event LabEventHandler<Scp096PryingGateEventArgs> PryingGate;

    /// <summary>
    /// Gets called when SCP-096 has pried a gate.
    /// </summary>
    public static event LabEventHandler<Scp096PriedGateEventArgs> PriedGate;

    /// <summary>
    /// Gets called when SCP-096 starts crying.
    /// </summary>
    public static event LabEventHandler<Scp096StartCryingEventArgs> StartCrying;

    /// <summary>
    /// Gets called when SCP-096 has started crying.
    /// </summary>
    public static event LabEventHandler<Scp096StartedCryingEventArgs> StartedCrying;

    /// <summary>
    /// Gets called when SCP-096 is trying not to cry.
    /// </summary>
    public static event LabEventHandler<Scp096TryingNotToCryEventArgs> TryingNotToCry;

    /// <summary>
    /// Gets called when SCP-096 has tried not to cry.
    /// </summary>
    public static event LabEventHandler<Scp096TriedNotToCryEventArgs> TriedNotToCry;
}
