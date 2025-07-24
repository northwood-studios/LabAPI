using Hazards;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.EnteredHazard"/> event.
/// </summary>
public class PlayerEnteredHazardEventArgs : EventArgs, IPlayerEvent, IHazardEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEnteringPocketDimensionEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who entered the hazard.</param>
    /// <param name="hazard">The hazard.</param>
    public PlayerEnteredHazardEventArgs(ReferenceHub hub, EnvironmentalHazard hazard)
    {
        Player = Player.Get(hub);
        Hazard = Hazard.Get(hazard);
    }

    /// <summary>
    /// The player which entered the hazard.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The target hazard.
    /// </summary>
    public Hazard Hazard { get; }
}
