using LabApi.Features.Wrappers;
using PlayerRoles;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.KillingEnemyCompleting"/> event.
/// </summary>
public class EnemyKillingObjectiveEventArgs : ObjectiveCompletingBaseEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyKillingObjectiveEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="faction"/>.</param>
    /// <param name="targetHub">The player that has been killed.</param>
    public EnemyKillingObjectiveEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant, ReferenceHub targetHub) : base(hub, faction, influenceToGrant, timeToGrant)
    {
        Target = Player.Get(targetHub);
    }

    /// <summary>
    /// Gets the player that has been killed.
    /// </summary>
    public Player Target { get; }
}
