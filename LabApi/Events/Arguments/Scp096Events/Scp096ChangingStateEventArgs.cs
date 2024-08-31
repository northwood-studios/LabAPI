using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp096;
using System;

namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.ChangingState"/> event.
/// </summary>
public class Scp096ChangingStateEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096ChangingStateEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    /// <param name="state">The SCP-096's new rage state.</param>
    public Scp096ChangingStateEventArgs(Player player, Scp096RageState state)
    {
        Player = player;
        State = state;
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The SCP-096's new rage state.
    /// </summary>
    public Scp096RageState State { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
