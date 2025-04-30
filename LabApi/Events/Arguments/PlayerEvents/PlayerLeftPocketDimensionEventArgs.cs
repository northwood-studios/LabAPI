using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.LeftPocketDimension"/> event.
/// </summary>
public class PlayerLeftPocketDimensionEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeftPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who tried to left pocket dimension.</param>
    /// <param name="teleport">The teleport the player collided with.</param>
    /// <param name="isSuccessful">Whether the escape was successful.</param>
    public PlayerLeftPocketDimensionEventArgs(ReferenceHub player, PocketDimensionTeleport teleport, bool isSuccessful)
    {
        Player = Player.Get(player);
        Teleport = PocketTeleport.Get(teleport);
        IsSuccessful = isSuccessful;
    }

    /// <summary>
    /// Gets the player who tried to left pocket dimension.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the teleport the player collided with.
    /// </summary>
    /// <remarks>
    /// Can be null if exit was forced by a plugin.
    /// </remarks>
    public PocketTeleport? Teleport { get; }

    /// <summary>
    /// Gets whether the escape was successful.
    /// </summary>
    public bool IsSuccessful { get; }
}