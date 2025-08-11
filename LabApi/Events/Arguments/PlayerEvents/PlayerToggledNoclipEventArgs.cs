using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ToggledNoclip"/> event.
/// </summary>
public class PlayerToggledNoclipEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerToggledNoclipEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who has toggled the noclip.</param>
    /// <param name="isNoclipping">The new state of the noclip.</param>
    public PlayerToggledNoclipEventArgs(ReferenceHub hub, bool isNoclipping)
    {
        Player = Player.Get(hub);
        IsNoclipping = isNoclipping;
    }

    /// <summary>
    /// Gets the player who toggled noclip.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets whether the player now has noclip enabled or not.
    /// </summary>
    public bool IsNoclipping { get; }
}
