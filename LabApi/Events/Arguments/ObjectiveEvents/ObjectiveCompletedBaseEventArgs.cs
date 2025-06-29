using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.Completed"/> event.
/// </summary>
public class ObjectiveCompletedBaseEventArgs : EventArgs, IPlayerEvent, IObjectiveEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveCompletingBaseEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points granted to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time reduced from the <paramref name="faction"/>.</param>
    /// <param name="sendToPlayers">Whether the objective completion has been sent to players.</param>
    public ObjectiveCompletedBaseEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant, bool sendToPlayers)
    {
        Player = Player.Get(hub);
        Faction = faction;
        InfluenceToGrant = influenceToGrant;
        TimeToGrant = timeToGrant;
        SendToPlayers = sendToPlayers;
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <inheritdoc/>
    public Faction Faction { get; }

    /// <inheritdoc/>
    public float InfluenceToGrant { get; }

    /// <inheritdoc/>
    public float TimeToGrant { get; }

    /// <inheritdoc/>
    public bool SendToPlayers { get; }
}
