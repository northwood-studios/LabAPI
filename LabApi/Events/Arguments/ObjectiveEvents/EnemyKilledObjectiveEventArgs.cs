using LabApi.Features.Wrappers;
using PlayerRoles;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.KilledEnemyCompleted"/> event.
/// </summary>
public class EnemyKilledObjectiveEventArgs : ObjectiveCompletedBaseEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyKilledObjectiveEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="faction"/>.</param>
    /// <param name="sendToPlayers">Whether the objective completion has been sent to players.</param>
    /// <param name="targetHub">The player that has been killed.</param>
    public EnemyKilledObjectiveEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant, bool sendToPlayers, ReferenceHub targetHub) : base(hub, faction, influenceToGrant, timeToGrant, sendToPlayers)
    {
        Target = Player.Get(targetHub);
    }

    /// <summary>
    /// Gets the player that has been killed.
    /// </summary>
    public Player Target { get; }
}
