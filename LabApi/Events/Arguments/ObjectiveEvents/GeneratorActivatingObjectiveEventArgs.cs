using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using PlayerRoles;

namespace LabApi.Events.Arguments.ObjectiveEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ObjectiveEvents.ActivatingGeneratorCompleting"/> event.
/// </summary>
public class GeneratorActivatingObjectiveEventArgs : ObjectiveCompletingBaseEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneratorActivatingObjectiveEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player hub who triggered the objective.</param>
    /// <param name="faction">The Faction to grant the influence to.</param>
    /// <param name="influenceToGrant">The influence points to grant to the <paramref name="faction"/>.</param>
    /// <param name="timeToGrant">The time to reduce from the <paramref name="faction"/>.</param>
    /// <param name="generator">The generator that has been activated.</param>
    public GeneratorActivatingObjectiveEventArgs(ReferenceHub hub, Faction faction, float influenceToGrant, float timeToGrant, Scp079Generator generator) : base(hub, faction, influenceToGrant, timeToGrant)
    {
        Generator = Generator.Get(generator);
    }

    /// <summary>
    /// Gets the generator that has been activated.
    /// </summary>
    public Generator Generator { get; }
}
