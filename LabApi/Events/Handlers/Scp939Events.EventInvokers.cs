using LabApi.Events.Arguments.Scp939Events;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class Scp939Events
{
    /// <summary>
    /// Invokes the <see cref="Attacking"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp939AttackingEventArgs"/> of the event.</param>
    public static void OnAttacking(Scp939AttackingEventArgs args) => Attacking.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Attacked"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp939AttackedEventArgs"/> of the event.</param>
    public static void OnAttacked(Scp939AttackedEventArgs args) => Attacked.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CreatingAmnesticCloud"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp939CreatingAmnesticCloudEventArgs"/> of the event.</param>
    public static void OnCreatingAmnesticCloud(Scp939CreatingAmnesticCloudEventArgs args) => CreatingAmnesticCloud.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CreatedAmnesticCloud"/> event.
    /// </summary>
    /// <param name="args"The <see cref="Scp939CreatedAmnesticCloudEventArgs"/> of the event.</param>
    public static void OnCreatedAmnesticCloud(Scp939CreatedAmnesticCloudEventArgs args) => CreatedAmnesticCloud.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Lunging"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp939LungingEventArgs"/> of the event.</param>
    public static void OnLunging(Scp939LungingEventArgs args) => Lunging.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Lunged"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp939LungedEventArgs"/> of the event.</param>
    public static void OnLunged(Scp939LungedEventArgs args) => Lunged.InvokeEvent(args);
}