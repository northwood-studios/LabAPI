using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.FirstPersonControl;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.MovementStateChanged"/> event.
/// </summary>
public class PlayerMovementStateChangedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMovementStateChangedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player whose movement state has changed.</param>
    /// <param name="oldState">Old movement state of the player./param>
    /// <param name="newState">New movement state of the player.</param>
    public PlayerMovementStateChangedEventArgs(ReferenceHub hub, PlayerMovementState oldState, PlayerMovementState newState)
    {
        Player = Player.Get(hub);
        OldState = oldState;
        NewState = newState;
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <summary>
    /// Gets the old movement state.
    /// </summary>
    public PlayerMovementState OldState { get; }

    /// <summary>
    /// Gets the new movement state.
    /// </summary>
    public PlayerMovementState NewState { get; }
}
