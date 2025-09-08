using LabApi.Features.Wrappers;
using PlayerRoles;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.DamagedScpCompleted"/> event.
/// </summary>
public class ScpDamagedObjectiveEventArgs : ObjectiveCompletedBaseEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScpDamagedObjectiveEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="Faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="Faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="Faction"/>.</param>
    /// <param name="sendToPlayers">Whether the objective completion has been sent to players.</param>
    /// <param name="targetHub">The hub of the player SCP that has been damaged.</param>
    public ScpDamagedObjectiveEventArgs(ReferenceHub hub, Faction Faction, float influenceToGrant, float timeToGrant, bool sendToPlayers, ReferenceHub targetHub) : base(hub, Faction, influenceToGrant, timeToGrant, sendToPlayers)
    {
        Target = Player.Get(targetHub);
    }

    /// <summary>
    /// Gets the SCP player that has been damaged.
    /// </summary>
    public Player Target { get; }
}
