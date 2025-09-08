using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using PlayerRoles;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.ActivatedGeneratorCompleted"/> event.
/// </summary>
public class GeneratorActivatedObjectiveEventArgs : ObjectiveCompletedBaseEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneratorActivatedObjectiveEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="faction"/>.</param>
    /// <param name="sendToPlayers">Whether the objective completion has been sent to players.</param>
    /// <param name="generator">The generator that has been activated.</param>
    public GeneratorActivatedObjectiveEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant, bool sendToPlayers, Scp079Generator generator) : base(hub, faction, influenceToGrant, timeToGrant, sendToPlayers)
    {
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the generator that has been activated.
    /// </summary>
    public Generator Generator { get; }
}