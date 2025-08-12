using InventorySystem.Items;
using LabApi.Features.Wrappers;
using PlayerRoles;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.PickedScpItemCompleted"/> event.
/// </summary>
public class ScpItemPickedObjectiveEventArgs : ObjectiveCompletedBaseEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScpItemPickedObjectiveEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="faction"/>.</param>
    /// <param name="sendToPlayers">Whether the objective completion has been sent to players.</param>
    /// <param name="item">The item that has been picked up.</param>
    public ScpItemPickedObjectiveEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant, bool sendToPlayers, ItemBase item)
        : base(hub, faction, influenceToGrant, timeToGrant, sendToPlayers)
    {
        Item = Item.Get(item);
    }

    /// <summary>
    /// Gets the item that has been picked up.
    /// </summary>
    public Item Item { get; }
}
