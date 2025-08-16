using Hazards;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.LeavingHazard"/> event.
/// </summary>
public class PlayerLeavingHazardEventArgs : EventArgs, IPlayerEvent, IHazardEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeavingHazardEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is leaving.</param>
    /// <param name="hazard">The hazard that the player is leaving.</param>
    public PlayerLeavingHazardEventArgs(ReferenceHub hub, EnvironmentalHazard hazard)
    {
        Player = Player.Get(hub);
        Hazard = Hazard.Get(hazard);

        IsAllowed = true;
    }

    /// <summary>
    /// Gets the player who is attempting to leave the hazard.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the hazard the player is attempting to leave.
    /// </summary>
    public Hazard Hazard { get; }

    /// <summary>
    /// Gets or sets whether the player is allowed to leave the hazard.
    /// Setting this to false will result in player keeping the hazard effects and the event will be fired again until it is allowed or the player is back within the hazard's range.
    /// </summary>
    public bool IsAllowed { get; set; }
}
