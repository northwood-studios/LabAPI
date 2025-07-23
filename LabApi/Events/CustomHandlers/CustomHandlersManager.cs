using System;
using System.Collections.Generic;
using System.Reflection;

namespace LabApi.Events.CustomHandlers;

/// <summary>
/// Handles the registration of custom event handlers.
/// </summary>
public static partial class CustomHandlersManager
{
    /// <summary>
    /// Registers all the events from the specified handler.
    /// </summary>
    /// <param name="handler">The handler to register.</param>
    /// <typeparam name="T">The type of the handler.</typeparam>
    public static void RegisterEventsHandler<T>(T handler)
        where T : CustomEventsHandler
    {
        Type handlerType = handler.GetType();
        RegisterEvents(handler, handlerType);
    }

    /// <summary>
    /// Unregisters all the events from the specified handler.
    /// </summary>
    /// <param name="handler">The handler to unregister.</param>
    /// <typeparam name="T">The type of the handler.</typeparam>
    public static void UnregisterEventsHandler<T>(T handler)
        where T : CustomEventsHandler
    {
        // We can simply iterate through the internal dictionary and remove the events.
        foreach (KeyValuePair<EventInfo, Delegate> pair in handler.InternalEvents)
        {
            EventInfo eventInfo = pair.Key;
            Delegate del = pair.Value;

            // We simply remove the event from the handler.
            eventInfo.RemoveEventHandler(null, del);
        }

        // And we clear the internal dictionary.
        handler.InternalEvents.Clear();
    }

    /// <summary>
    /// Checks if the event is overriden and subscribes the handler to the event if it is.
    /// </summary>
    public static void CheckEvent<T>(T handler, Type handlerType, string methodDelegate, Type eventType, string eventName)
        where T : CustomEventsHandler
    {
        // We first get the method from the handler, there can be custom methods names as the original events but with different overloads, so we filter them.
        MethodInfo[] candidates = handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

        MethodInfo method = null;
        foreach (MethodInfo candidate in candidates)
        {
            if (candidate.Name == methodDelegate && IsOverride(candidate))
            {
                method = candidate;
                break;
            }
        }

        if (method == null)
            return;

        // We get the event from the event type.
        EventInfo eventInfo = eventType.GetEvent(eventName);

        // Then we create a delegate from the method and subscribe it to the event.
        Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, handler, method);

        // We add the event and the delegate to the internal dictionary.
        eventInfo.AddEventHandler(null, del);
        handler.InternalEvents.Add(eventInfo, del);
    }

    /// <summary>
    /// Whether the method is an override or the base definition.
    /// </summary>
    private static bool IsOverride(MethodInfo method)
    {
        // Based on the definition of the method, we check if the declaring type is different.
        return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
    }

    static partial void RegisterEvents<T>(T handler, Type handlerType)
        where T : CustomEventsHandler;
}