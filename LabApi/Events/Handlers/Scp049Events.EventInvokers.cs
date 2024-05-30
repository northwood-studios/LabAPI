using LabApi.Events.Arguments.Scp049Events;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class Scp049Events
{
    /// <summary>
    /// Invokes the <see cref="StartingResurrection"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp049StartingResurrectionEventArgs"/> of the event.</param>
    public static void OnStartingResurrection(Scp049StartingResurrectionEventArgs args) => StartingResurrection.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="ResurrectingBody"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp049ResurrectingBodyEventArgs"/> of the event.</param>
    public static void OnResurrectingBody(Scp049ResurrectingBodyEventArgs args) => ResurrectingBody.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="ResurrectedBody"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp049ResurrectedBodyEventArgs"/> of the event.</param>
    public static void OnResurrectedBody(Scp049ResurrectedBodyEventArgs args) => ResurrectedBody.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="UsingDoctorsCall"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp049UsingDoctorsCallEventArgs"/> of the event.</param>
    public static void OnUsingDoctorsCall(Scp049UsingDoctorsCallEventArgs args) => UsingDoctorsCall.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="UsedDoctorsCall"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp049UsedDoctorsCallEventArgs"/> of the event.</param>
    public static void OnUsedDoctorsCall(Scp049UsedDoctorsCallEventArgs args) => UsedDoctorsCall.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="UsingSense"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp049UsingSenseEventArgs"/> of the event.</param>
    public static void OnUsingSense(Scp049UsingSenseEventArgs args) => UsingSense.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="UsedSense"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp049UsedSenseEventArgs"/> of the event.</param>
    public static void OnUsedSense(Scp049UsedSenseEventArgs args) => UsedSense.InvokeEvent(args);
}