using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.LeftPocketDimension"/> event.
/// </summary>
public class PlayerLeftPocketDimensionEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeftPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who tried to left pocket dimension.</param>
    /// <param name="isSuccessful">Whenever the escape was successful.</param>
    public PlayerLeftPocketDimensionEventArgs(ReferenceHub player, bool isSuccessful)
    {
        Player = Player.Get(player);
        IsSuccessful = isSuccessful;
    }

    /// <summary>
    /// Gets the player who tried to left pocket dimension.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets whether the escape was successful.
    /// </summary>
    public bool IsSuccessful { get; }
}