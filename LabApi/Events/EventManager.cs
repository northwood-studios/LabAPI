using LabApi.Features.Console;
using NorthwoodLib.Pools;
using System;
using System.Reflection;
using System.Text;

namespace LabApi.Events;

/// <summary>
/// LabAPI's event manager.
/// Responsible for handling all events inside the API.
/// </summary>
public static class EventManager
{
    /// <summary>
    /// Invokes a <see cref="LabEventHandler"/> event and logs any errors that occur.
    /// </summary>
    /// <param name="eventHandler">The <see cref="LabEventHandler"/> to invoke.</param>
    public static void InvokeEvent(this LabEventHandler? eventHandler)
    {
        // We check if the event handler is null
        if (eventHandler is null)
            return;

#if DEBUG
        // In DEBUG mode we add some useful logs about the event.
        Logger.Debug("Invoking event " + eventHandler.FormatToString());
#endif

        // We iterate through all the subscribers of the event and invoke them.
        foreach (Delegate sub in eventHandler.GetInvocationList())
        {
            try
            {
                // We invoke the subscriber as a lab event handler.
                if (sub is LabEventHandler labEventHandler)
                    labEventHandler.Invoke();
            }
            catch (Exception e)
            {
                Logger.Error(FormatErrorMessage(eventHandler, e));
            }
        }
    }

    /// <summary>
    /// Invokes a <see cref="LabEventHandler{TEventArgs}"/> event and logs any errors that occur.
    /// </summary>
    /// <param name="eventHandler">The <see cref="LabEventHandler{TEventArgs}"/> to invoke.</param>
    /// <param name="args">The <see cref="EventArgs"/> of the event.</param>
    /// <typeparam name="TEventArgs">The type of the <see cref="EventArgs"/> of the event.</typeparam>
    public static void InvokeEvent<TEventArgs>(this LabEventHandler<TEventArgs>? eventHandler, TEventArgs args)
        where TEventArgs : EventArgs
    {
        // We check if the event handler is null
        if (eventHandler is null)
            return;

#if DEBUG
        // In DEBUG mode we add some useful logs about the event.
        Logger.Debug("Invoking event " + eventHandler.FormatToString(args));
#endif

        // We iterate through all the subscribers of the event and invoke them.
        foreach (Delegate sub in eventHandler.GetInvocationList())
        {
            try
            {
                // We invoke the subscriber as a lab event handler.
                if (sub is LabEventHandler<TEventArgs> labEventHandler)
                    labEventHandler.Invoke(args);
            }
            catch (Exception e)
            {
                Logger.Error(FormatErrorMessage(eventHandler, e));
            }
        }
    }

    /// <summary>
    /// Formats the <see cref="LabEventHandler"/> to a string.
    /// </summary>
    /// <param name="eventHandler">The <see cref="LabEventHandler"/> to format.</param>
    /// <returns>A formatted string of the <see cref="LabEventHandler"/>.</returns>
    public static string FormatToString(this LabEventHandler eventHandler)
    {
        // As this one doesn't have parameters, we can just return the method name.
        return eventHandler.Method.Name;
    }

    /// <summary>
    /// Formats the <see cref="LabEventHandler{T}"/> to a string.
    /// </summary>
    /// <param name="eventHandler">The <see cref="LabEventHandler{T}"/> to format.</param>
    /// <param name="args">The <see cref="EventArgs"/> of the event.</param>
    /// <typeparam name="TEventArgs">The type of the <see cref="EventArgs"/> of the event.</typeparam>
    /// <returns>A formatted string of the <see cref="LabEventHandler{T}"/>.</returns>
    public static string FormatToString<TEventArgs>(this LabEventHandler<TEventArgs> eventHandler, TEventArgs args)
        where TEventArgs : EventArgs
    {
        // We rent a StringBuilder from the pool to avoid creating a new one.
        StringBuilder stringBuilder = StringBuilderPool.Shared.Rent(eventHandler.Method.Name + ":");

        // We iterate through all the properties of the EventArgs and append them to the StringBuilder.
        foreach (PropertyInfo property in typeof(TEventArgs).GetProperties())
        {
            stringBuilder.AppendLine("\t- " + property.Name + ": " + property.GetValue(args));
        }

        // As this one has parameters, we can return the method name and the parameters' types.
        return StringBuilderPool.Shared.ToStringReturn(stringBuilder);
    }

    /// <summary>
    /// Formats an error message for a <see cref="LabEventHandler"/>.
    /// </summary>
    /// <param name="eventHandler">The <see cref="LabEventHandler"/> that caused the error.</param>
    /// <param name="exception">The <see cref="Exception"/> that occurred.</param>
    /// <returns></returns>
    public static string FormatErrorMessage(Delegate eventHandler, Exception exception)
        => $"'{exception.GetType().Name}' occured while invoking '{eventHandler.Method.Name}' on '{eventHandler.Target.GetType().FullName}': '{exception.Message}', stack trace:\n{exception.StackTrace}";
}