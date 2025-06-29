using LabApi.Events.Arguments.ObjectiveEvents;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to objectives.
/// </summary>
public static partial class ObjectiveEvents
{
    /// <summary>
    /// Gets called when <b>any</b> objective is being completed.<br/>
    /// Use specific events or the subclasses of the <see cref="ObjectiveCompletingBaseEventArgs"/> to determine which kind of the objective is being completed.
    /// </summary>
    /// <remarks>
    /// This event is called <b>after</b> the very specific objective event has been triggered.
    /// </remarks>
    public static event LabEventHandler<ObjectiveCompletingBaseEventArgs>? Completing;

    /// <summary>
    /// Gets called when <b>any</b> objective has been completed.<br/>
    /// Use specific events or the subclasses of the <see cref="ObjectiveCompletedBaseEventArgs"/> to determine which kind of the objective has been completed.
    /// </summary>
    /// <remarks>
    /// This event is called <b>after</b> the very specific objective event has been triggered.
    /// </remarks>
    public static event LabEventHandler<ObjectiveCompletedBaseEventArgs>? Completed;

    /// <summary>
    /// Gets called when the enemy kill objective is being completed.
    /// </summary>
    public static event LabEventHandler<EnemyKillingObjectiveEventArgs>? KillingEnemyCompleting;

    /// <summary>
    /// Gets called when the enemy kill objective has been completed.
    /// </summary>
    public static event LabEventHandler<EnemyKilledObjectiveEventArgs>? KilledEnemyCompleted;

    /// <summary>
    /// Gets called when the player escape objective is being completed.
    /// </summary>
    public static event LabEventHandler<EscapingObjectiveEventArgs>? EscapingCompleting;

    /// <summary>
    /// Gets called when the player escape objective has been completed.
    /// </summary>
    public static event LabEventHandler<EscapedObjectiveEventArgs>? EscapedCompleted;

    /// <summary>
    /// Gets called when the generator activated objective is being completed.
    /// </summary>
    public static event LabEventHandler<GeneratorActivatingObjectiveEventArgs>? ActivatingGeneratorCompleting;

    /// <summary>
    /// Gets called when the generator activated objective has been completed.
    /// </summary>
    public static event LabEventHandler<GeneratorActivatedObjectiveEventArgs>? ActivatedGeneratorCompleted;

    /// <summary>
    /// Gets called when the scp damage objective is being completed.
    /// </summary>
    public static event LabEventHandler<ScpDamagingObjectiveEventArgs>? DamagingScpCompleting;

    /// <summary>
    /// Gets called when the scp damage objective has been completed.
    /// </summary>
    public static event LabEventHandler<ScpDamagedObjectiveEventArgs>? DamagedScpCompleted;

    /// <summary>
    /// Gets called when the scp item pickup objective is being completed.
    /// </summary>
    public static event LabEventHandler<ScpItemPickingObjectiveEventArgs>? PickingScpItemCompleting;

    /// <summary>
    /// Gets called when the scp item pickup objective has been completed.
    /// </summary>
    public static event LabEventHandler<ScpItemPickedObjectiveEventArgs>? PickedScpItemCompleted;
}
