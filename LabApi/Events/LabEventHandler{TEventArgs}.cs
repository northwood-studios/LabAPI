using System;

namespace LabApi.Events;

/// <summary>
/// LabAPI's event handler with a <see cref="EventArgs"/> parameter.
/// Called when an event is with the specified <see cref="EventArgs"/> triggered .
/// </summary>
/// <param name="ev">The event arg instance.</param>
/// <typeparam name="TEventArgs">The type of the <see cref="EventArgs"/> of the event.</typeparam>
public delegate void LabEventHandler<in TEventArgs>(TEventArgs ev)
    where TEventArgs : EventArgs;