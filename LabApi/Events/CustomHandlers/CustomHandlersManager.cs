using System;
using System.Collections.Generic;
using System.Reflection;
using LabApi.Events.Handlers;

namespace LabApi.Events.CustomHandlers;

/// <summary>
/// Handles the registration of custom event handlers.
/// </summary>
public static class CustomHandlersManager
{
    /// <summary>
    /// Registers all the events from the specified handler.
    /// </summary>
    /// <param name="handler">The handler to register.</param>
    /// <typeparam name="T">The type of the handler.</typeparam>
    public static void RegisterEventsHandler<T>(T handler) where T : CustomEventsHandler
    {
        Type handlerType = handler.GetType();
        RegisterServerEvents(handler, handlerType);
    }
    
    /// <summary>
    /// Unregisters all the events from the specified handler.
    /// </summary>
    /// <param name="handler">The handler to unregister.</param>
    /// <typeparam name="T">The type of the handler.</typeparam>
    public static void UnregisterEventsHandler<T>(T handler) where T : CustomEventsHandler
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
    private static void CheckEvent<T>(T handler, Type handlerType, string methodDelegate, Type eventType, string eventName) where T : CustomEventsHandler
    {
        // We first get the method from the handler.
        MethodInfo method = handlerType.GetMethod(methodDelegate, BindingFlags.Public | BindingFlags.Instance);
        
        // If the method is null or not an override, we return.
        if (method == null || !IsOverride(method)) 
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

    /// <summary>
    /// Registers all <see cref="ServerEvents">Server events</see> from a <see cref="CustomEventsHandler"/>.
    /// </summary>
    private static void RegisterServerEvents<T>(T handler, Type handlerType) where T : CustomEventsHandler
    {
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnWaitingForPlayers), typeof(ServerEvents), nameof(ServerEvents.WaitingForPlayers));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnRoundRestarted), typeof(ServerEvents), nameof(ServerEvents.RoundRestarted));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnRoundEnding), typeof(ServerEvents), nameof(ServerEvents.RoundEnding));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnRoundEnded), typeof(ServerEvents), nameof(ServerEvents.RoundEnded));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnRoundStarting), typeof(ServerEvents), nameof(ServerEvents.RoundStarting));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnRoundStarted), typeof(ServerEvents), nameof(ServerEvents.RoundStarted));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnBanIssuing), typeof(ServerEvents), nameof(ServerEvents.BanIssuing));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnBanIssued), typeof(ServerEvents), nameof(ServerEvents.BanIssued));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnBanRevoking), typeof(ServerEvents), nameof(ServerEvents.BanRevoking));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnBanRevoked), typeof(ServerEvents), nameof(ServerEvents.BanRevoked));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnBanUpdating), typeof(ServerEvents), nameof(ServerEvents.BanUpdating));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnBanUpdated), typeof(ServerEvents), nameof(ServerEvents.BanUpdated));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnCommandExecuting), typeof(ServerEvents), nameof(ServerEvents.CommandExecuting));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnCommandExecuted), typeof(ServerEvents), nameof(ServerEvents.CommandExecuted));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnWaveRespawning), typeof(ServerEvents), nameof(ServerEvents.WaveRespawning));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnWaveRespawned), typeof(ServerEvents), nameof(ServerEvents.WaveRespawned));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnWaveTeamSelecting), typeof(ServerEvents), nameof(ServerEvents.WaveTeamSelecting));
        CheckEvent(handler, handlerType, nameof(CustomEventHandlers.OnWaveTeamSelected), typeof(ServerEvents), nameof(ServerEvents.WaveTeamSelected));
    }
}