using LabApi.Events.Arguments.Scp049Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCP-049.
/// </summary>
public static partial class Scp049Events
{
    /// <summary>
    /// Gets called when SCP-049 is starting a resurrection.
    /// </summary>
    public static event LabEventHandler<Scp049StartingResurrectionEventArgs>? StartingResurrection;

    /// <summary>
    /// Gets called when SCP-049 has finished resurrecting and is about to resurrect a body.
    /// </summary>
    public static event LabEventHandler<Scp049ResurrectingBodyEventArgs>? ResurrectingBody;

    /// <summary>
    /// Gets called when SCP-049 has resurrected a body.
    /// </summary>
    public static event LabEventHandler<Scp049ResurrectedBodyEventArgs>? ResurrectedBody;

    /// <summary>
    /// Gets called when SCP-049 is using its doctors call ability.
    /// </summary>
    public static event LabEventHandler<Scp049UsingDoctorsCallEventArgs>? UsingDoctorsCall;

    /// <summary>
    /// Gets called when SCP-049 has used its doctors call ability.
    /// </summary>
    public static event LabEventHandler<Scp049UsedDoctorsCallEventArgs>? UsedDoctorsCall;

    /// <summary>
    /// Gets called when SCP-049 is using its sense ability.
    /// </summary>
    public static event LabEventHandler<Scp049UsingSenseEventArgs>? UsingSense;

    /// <summary>
    /// Gets called when SCP-049 has used its sense ability.
    /// </summary>
    public static event LabEventHandler<Scp049UsedSenseEventArgs>? UsedSense;

    /// <summary>
    /// Gets called when SCP-049 is using its attack ability.
    /// </summary>
    public static event LabEventHandler<Scp049AttackingEventArgs>? Attacking;

    /// <summary>
    /// Gets called when SCP-049 has used its attack ability.
    /// </summary>
    public static event LabEventHandler<Scp049AttackedEventArgs>? Attacked;

    /// <summary>
    /// Gets called when SCP-049 has lost a target.
    /// </summary>
    public static event LabEventHandler<Scp049SenseLostTargetEventArgs>? SenseLostTarget;

    /// <summary>
    /// Gets called when SCP-049 has killed a target.
    /// </summary>
    public static event LabEventHandler<Scp049SenseKilledTargetEventArgs>? SenseKilledTarget;
}