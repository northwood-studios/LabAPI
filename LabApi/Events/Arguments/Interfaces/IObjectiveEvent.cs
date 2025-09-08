using LabApi.Features.Wrappers;
using PlayerRoles;
using Respawning.Objectives;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves <see cref="FactionObjectiveBase"/>.
/// </summary>
public interface IObjectiveEvent
{
    /// <summary>
    /// Gets the player who triggered the objective completion.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the <see cref="PlayerRoles.Faction"/> which will receive the <see cref="InfluenceToGrant"/> and <see cref="TimeToGrant"/> rewards.
    /// </summary>
    public Faction Faction { get; }

    /// <summary>
    /// Gets the amount of influence to grant to the <see cref="Faction"/>.
    /// </summary>
    public float InfluenceToGrant { get; }

    /// <summary>
    /// Gets the amount of time to be reduced from the <see cref="Faction"/>'s timer.
    /// </summary>
    /// <remarks>
    /// Negative values reduce the timer, positive extends it.
    /// </remarks>
    public float TimeToGrant { get; }

    /// <summary>
    /// Gets whether the objective completion should be sent to players and visible on their screen.
    /// </summary>
    public bool SendToPlayers { get; }

}
