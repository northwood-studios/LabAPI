using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.LeavingPocketDimension"/> event.
/// </summary>
public class PlayerLeavingPocketDimensionEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeavingPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is trying to leave from pocket dimension.</param>
    /// <param name="teleport">The teleport the player collided with.</param>
    /// <param name="isSuccessful">Whether it is gonna be success.</param>
    public PlayerLeavingPocketDimensionEventArgs(ReferenceHub hub, PocketDimensionTeleport teleport, bool isSuccessful)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Teleport = PocketTeleport.Get(teleport);
        IsSuccessful = isSuccessful;
    }

    /// <summary>
    /// Gets the player who is trying to leave from pocket dimension.
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
    /// Gets or sets whether it is going to be success.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}