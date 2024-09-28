using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp049Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp049Events.ResurrectedBody"/> event.
/// </summary>
public class Scp049UsedDoctorsCallEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp049UsedDoctorsCallEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-049 player instance.</param>
    public Scp049UsedDoctorsCallEventArgs(ReferenceHub player)
    {
        Player = Player.Get(player);
    }

    /// <summary>
    /// The SCP-049 player instance.
    /// </summary>
    public Player Player { get; }
}