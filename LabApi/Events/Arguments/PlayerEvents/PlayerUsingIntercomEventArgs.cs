using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Voice;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UsingIntercom"/> event.
/// </summary>
public class PlayerUsingIntercomEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerUsingIntercomEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player that is using the intercom.</param>
    /// <param name="state">State of the intercom.</param>
    // TODO: Add intercom class and ref it docs
    public PlayerUsingIntercomEventArgs(ReferenceHub hub, IntercomState state)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        State = state;
    }

    /// <summary>
    /// Gets the player that is using the intercom.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the state of the intercom.
    /// </summary>
    public IntercomState State { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}