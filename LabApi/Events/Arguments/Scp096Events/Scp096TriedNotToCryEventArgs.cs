using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.TriedNotToCry"/> event.
/// </summary>
public class Scp096TriedNotToCryEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096TriedNotToCryEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-096 player instance.</param>
    public Scp096TriedNotToCryEventArgs(ReferenceHub hub)
    {
        Player = Player.Get(hub);
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }
}
