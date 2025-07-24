using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.Recontaining"/> event.
/// </summary>
public class Scp079RecontainingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079RecontainingEventArgs"/> class.
    /// </summary>
    /// <param name="hub">THe SCP-079 player instance.</param>
    /// <param name="activator">The player who activated the recontainment procedure.</param>
    public Scp079RecontainingEventArgs(ReferenceHub hub, ReferenceHub? activator)
    {
        IsAllowed = true;
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

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
