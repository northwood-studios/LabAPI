using Hazards;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.StayingInHazard"/> event.
/// </summary>
public class PlayersStayingInHazardEventArgs : EventArgs, IHazardEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayersStayingInHazardEventArgs"/> class.
    /// </summary>
    /// <param name="affectedPlayers">The list of affected players.</param>
    /// <param name="hazard">The hazard they are affected with.</param>
    public PlayersStayingInHazardEventArgs(List<ReferenceHub> affectedPlayers, EnvironmentalHazard hazard)
    {
        Hazard = Hazard.Get(hazard);

        List<Player> players = ListPool<Player>.Shared.Rent();
        AffectedPlayers = players;

        foreach (ReferenceHub referenceHub in affectedPlayers)
        {
            AffectedPlayers.Add(Player.Get(referenceHub));
        }
    }

    /// <summary>
    /// The affected players. Note that this list is pooled. Please copy the contents if you wish to do anything with it after the event finishes.
    /// </summary>
    public List<Player> AffectedPlayers { get; }

    /// <summary>
    /// The hazard they are affected with.
    /// </summary>
    public Hazard Hazard { get; }
}