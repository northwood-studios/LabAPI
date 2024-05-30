using System.Collections.Generic;
using PlayerRoles.Ragdolls;

namespace LabApi.Features.Wrappers.Player;

/// <summary>
/// The wrapper representing <see cref="BasicRagdoll">basic ragdolls</see>.
/// </summary>
public class Ragdoll
{
    /// <summary>
    /// Contains all the cached ragdolls in the game, accessible through their <see cref="BasicRagdoll"/>.
    /// </summary>
    public static Dictionary<BasicRagdoll, Ragdoll> Dictionary { get; } = [];
    
    /// <summary>
    /// A reference to all <see cref="Ragdoll"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Ragdoll> List => Dictionary.Values;
    
    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="ragdoll">The ragdoll component.</param>
    private Ragdoll(BasicRagdoll ragdoll)
    {
        Dictionary.Add(ragdoll, this);
        RagdollBase = ragdoll;
    }
    
    /// <summary>
    /// Gets the <see cref="BasicRagdoll"/> of the ragdoll.
    /// </summary>
    public BasicRagdoll RagdollBase { get; }
    
    /// <summary>
    /// Gets the ragdoll wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="ragdoll">The ragdoll.</param>
    /// <returns>The requested ragdoll.</returns>
    public static Ragdoll Get(BasicRagdoll ragdoll) =>
        Dictionary.TryGetValue(ragdoll, out Ragdoll rag) ? rag : new Ragdoll(ragdoll);
}