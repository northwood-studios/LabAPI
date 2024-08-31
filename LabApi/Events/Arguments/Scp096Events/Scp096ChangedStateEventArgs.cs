using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp096;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.ChangedState"/> event.
/// </summary>
public class Scp096ChangedStateEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096ChangedStateEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    /// <param name="state">The SCP-096's new rage state.</param>
    public Scp096ChangedStateEventArgs(Player player, Scp096RageState state)
    {
        Player = player;
        State = state;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The SCP-096's new rage state.
    /// </summary>
    public Scp096RageState State { get; }
}
