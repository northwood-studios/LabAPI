using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.Recontained"/> event.
/// </summary>
public class Scp079RecontainedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079RecontainedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-079 player instance.</param>
    /// <param name="activator">The player who activated the recontainment procedure.</param>
    public Scp079RecontainedEventArgs(ReferenceHub hub, ReferenceHub? activator)
    {
        Player = Player.Get(hub);
        Activator = Player.Get(activator);
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The player who activated the recontainment procedure.
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, the SCP-079 was recontained automatically.
    /// </remarks>
    public Player? Activator { get; }
}
