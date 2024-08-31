using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp096Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp096Events.StartCrying"/> event.
/// </summary>
public class Scp096StartCryingEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp096StartCryingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-096 player instance.</param>
    public Scp096StartCryingEventArgs(Player player)
    {
        Player = player;
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-096 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
