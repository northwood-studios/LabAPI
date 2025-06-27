using PlayerRoles;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.EscapedCompleted"/> event.
/// </summary>
public class EscapedObjectiveEventArgs : ObjectiveCompletedBaseEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EscapedObjectiveEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="faction"/>.</param>
    /// <param name="newRole">The new role the player gets after escaping.</param>
    /// <param name="sendToPlayers">Whether the objective completion has been sent to players.</param>
    public EscapedObjectiveEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant, bool sendToPlayers, RoleTypeId newRole) : base(hub, faction, influenceToGrant, timeToGrant, sendToPlayers)
    {
        NewRole = newRole;
    }

    /// <summary>
    /// Gets the new role the player is getting.
    /// </summary>
    public RoleTypeId NewRole { get; }
}
