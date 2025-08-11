using Hazards;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.EnteringHazard"/> event.
/// </summary>
public class PlayerEnteringHazardEventArgs : EventArgs, IPlayerEvent, IHazardEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEnteringPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who entered the hazard.</param>
    /// <param name="hazard">The hazard.</param>
    public PlayerEnteringHazardEventArgs(ReferenceHub hub, EnvironmentalHazard hazard)
    {
        Player = Player.Get(hub);
        Hazard = Hazard.Get(hazard);

        IsAllowed = true;
    }

    /// <summary>
    /// The player which is entering the hazard.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target hazard.
    /// </summary>
    public Hazard Hazard { get; }

    /// <summary>
    /// Gets or sets whether the event is allowed. Not allowing this event will cause the player to not be affected by the hazard.
    /// </summary>
    public bool IsAllowed { get; set; }
}
