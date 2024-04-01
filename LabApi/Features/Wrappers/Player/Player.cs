using System.Collections.Generic;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ReferenceHub">reference hubs</see>, the in-game players.
/// </summary>
public class Player
{
    /// <summary>
    /// Contains all the cached players in the game, accessible through their <see cref="ReferenceHub"/>.
    /// </summary>
    public static Dictionary<ReferenceHub, Player> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Player"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Player> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    private Player(ReferenceHub referenceHub)
    {
        Dictionary.Add(referenceHub, this);
        ReferenceHub = referenceHub;
    }
    
    /// <summary>
    /// The <see cref="ReferenceHub">reference hub</see> of the player.
    /// </summary>
    public ReferenceHub ReferenceHub { get; }
    
    /// <summary>
    /// Gets the player wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    /// <returns>The requested player.</returns>
    public static Player Get(ReferenceHub referenceHub) =>
        Dictionary.TryGetValue(referenceHub, out Player player) ? player : new Player(referenceHub);
}