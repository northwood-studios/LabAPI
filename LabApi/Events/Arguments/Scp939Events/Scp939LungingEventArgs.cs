using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 is lunging.
/// </summary>
public class Scp939LungingEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939LungingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-939 player instance.</param>
    /// <param name="lungeState">The SCP-939 lunge state.</param>
    public Scp939LungingEventArgs(Player player, Scp939LungeState lungeState)
    {
        IsAllowed = true;
        Player = player;
        LungeState = lungeState;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// The SCP-939 player instance.
    /// </summary>
    public Player Player { get; }
    
    /// <summary>
    /// Gets or sets the current state of the SCP-939 lunge ability.
    /// </summary>
    public Scp939LungeState LungeState { get; set; }
}