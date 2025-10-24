using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 has focused.
/// </summary>
public class Scp939FocusedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939FocusedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-939 player instance.</param>
    /// <param name="focusState">A value indicating whether SCP-939 is in the focus state.</param>
    public Scp939FocusedEventArgs(ReferenceHub hub, bool focusState)
    {
        Player = Player.Get(hub);
        FocusState = focusState;
    }

    /// <summary>
    /// Gets the 939 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the current state of the SCP-939 focus ability.
    /// </summary>
    public bool FocusState { get; }
}