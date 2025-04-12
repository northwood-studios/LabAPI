using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.UsingDoctorsCall"/> event.
/// </summary>
public class Scp049UsingDoctorsCallEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049UsingDoctorsCallEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-049 player instance.</param>
    public Scp049UsingDoctorsCallEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}