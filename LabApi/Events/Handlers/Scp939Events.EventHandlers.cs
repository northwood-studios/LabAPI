using LabApi.Events.Arguments.Scp939Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all the events related to SCP-939.
/// </summary>
public static partial class Scp939Events
{
    /// <summary>
    /// Gets called when SCP-939 is attacking.
    /// </summary>
    public static event LabEventHandler<Scp939AttackingEventArgs>? Attacking;

    /// <summary>
    /// Gets called when SCP-939 has attacked.
    /// </summary>
    public static event LabEventHandler<Scp939AttackedEventArgs>? Attacked;

    /// <summary>
    /// Gets called when SCP-939 is creating an amnestic cloud.
    /// </summary>
    public static event LabEventHandler<Scp939CreatingAmnesticCloudEventArgs>? CreatingAmnesticCloud;

    /// <summary>
    /// Gets called when SCP-939 has created an amnestic cloud.
    /// </summary>
    public static event LabEventHandler<Scp939CreatedAmnesticCloudEventArgs>? CreatedAmnesticCloud;

    /// <summary>
    /// Gets called when SCP-939 is lunging.
    /// </summary>
    public static event LabEventHandler<Scp939LungingEventArgs>? Lunging;

    /// <summary>
    /// Gets called when SCP-939 has lunged.
    /// </summary>
    public static event LabEventHandler<Scp939LungedEventArgs>? Lunged;

    /// <summary>
    /// Gets called when SCP-939 has focused.
    /// </summary>
    public static event LabEventHandler<Scp939FocusedEventArgs>? Focused;

    /// <summary>
    /// Gets called when SCP-939 is mimicking the environment.
    /// </summary>
    public static event LabEventHandler<Scp939MimickingEnvironmentEventArgs>? MimickingEnvironment;

    /// <summary>
    /// Gets called when SCP-939 has mimicked the environment.
    /// </summary>
    public static event LabEventHandler<Scp939MimickedEnvironmentEventArgs>? MimickedEnvironment;
}