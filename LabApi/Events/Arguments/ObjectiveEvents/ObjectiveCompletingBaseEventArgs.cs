using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.Completing"/> event.
/// </summary>
public abstract class ObjectiveCompletingBaseEventArgs : EventArgs, ICancellableEvent, IPlayerEvent, IObjectiveEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveCompletingBaseEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="faction"/>.</param>
    public ObjectiveCompletingBaseEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant)
    {
        Player = Player.Get(hub);
        Faction = faction;
        InfluenceToGrant = influenceToGrant;
        TimeToGrant = timeToGrant;
        SendToPlayers = true;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets or sets the player who triggered the objective completion.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the <see cref="PlayerRoles.Faction"/> which receives the <see cref="InfluenceToGrant"/> and <see cref="TimeToGrant"/> rewards.
    /// </summary>
    public Faction Faction { get; }

    /// <summary>
    /// Gets or sets the amount of influence granted to the <see cref="Faction"/>.
    /// </summary>
    public float InfluenceToGrant { get; }

    /// <summary>
    /// Gets or sets the amount of time reduced from the <see cref="Faction"/>'s timer.
    /// </summary>
    /// <remarks>
    /// Negative values reduce the timer, positive extends it.
    /// </remarks>
    public float TimeToGrant { get; }

    /// <summary>
    /// Gets or sets whether the objective completion has been sent to players and is visible on their screen.
    /// </summary>
    public bool SendToPlayers { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
