using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Jumped"/> event.
/// </summary>
public class PlayerJumpedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerJumpedEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who jumped.</param>
    /// <param name="jumpStrength">Strength of the jump.</param>
    public PlayerJumpedEventArgs(ReferenceHub hub, float jumpStrength)
    {
        Player = Player.Get(hub);
        JumpStrength = jumpStrength;
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <summary>
    /// Gets the strength of the jump.
    /// </summary>
    public float JumpStrength { get; }
}
