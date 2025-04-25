using Hazards;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.LeftHazard"/> event.
/// </summary>
public class PlayerLeftHazardEventArgs : EventArgs, IPlayerEvent, IHazardEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeftHazardEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who left the hazard.</param>
    /// <param name="hazard">The hazard that the player left.</param>
    public PlayerLeftHazardEventArgs(ReferenceHub player, EnvironmentalHazard hazard)
    {
        Player = Player.Get(player);
        Hazard = Hazard.Get(hazard);
    }

    /// <summary>
    /// Gets the player who left the hazard.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the hazard that the player has left.
    /// </summary>
    public Hazard Hazard { get; }
}
