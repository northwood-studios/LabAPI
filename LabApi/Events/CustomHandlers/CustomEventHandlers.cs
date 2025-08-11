using System;
using System.Collections.Generic;
using System.Reflection;

namespace LabApi.Events.CustomHandlers;

/// <summary>
/// Handles custom logic for any event.
/// </summary>
public abstract partial class CustomEventsHandler
{
    /// <summary>
    /// Internal dictionary to store the registered events and their delegates.
    /// </summary>
    internal Dictionary<EventInfo, Delegate> InternalEvents { get; } = [];
}