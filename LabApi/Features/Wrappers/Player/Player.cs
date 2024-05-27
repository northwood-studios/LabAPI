using System.Collections.Generic;
using System.Linq;
using NorthwoodLib.Pools;

namespace LabApi.Features.Wrappers.Player;

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
    /// The <see cref="Player"/> representing the host or server.
    /// </summary>
    public static Player Host { get; internal set; } // TODO: Implement this when we generate the map. Add it with Doors Cache, Rooms Cache, etc.
    
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
    /// Whether the player is the host or server.
    /// </summary>
    public bool IsHost => ReferenceHub.IsHost;
    
    /// <summary>
    /// Gets the player wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="referenceHub">The reference hub of the player.</param>
    /// <returns>The requested player.</returns>
    public static Player Get(ReferenceHub referenceHub) =>
        Dictionary.TryGetValue(referenceHub, out Player player) ? player : new Player(referenceHub);

    /// <summary>
    /// Gets a list of players from a list of reference hubs.
    /// </summary>
    /// <param name="referenceHubs">The reference hubs of the players.</param>
    /// <returns>A list of players.</returns>
    public static List<Player> Get(IEnumerable<ReferenceHub> referenceHubs)
    {
        // We rent a list from the pool to avoid unnecessary allocations.
        // We don't care if the developer forgets to return the list to the pool
        // as at least it will be more efficient than always allocating a new list.
        List<Player> list = ListPool<Player>.Shared.Rent();
        return GetNonAlloc(referenceHubs, list);
    }
    
    /// <summary>
    /// Gets a list of players from a list of reference hubs without allocating a new list.
    /// </summary>
    /// <param name="referenceHubs">The reference hubs of the players.</param>
    /// <param name="list">A reference to the list to add the players to.</param>
    public static List<Player> GetNonAlloc(IEnumerable<ReferenceHub> referenceHubs, List<Player> list)
    {
        // We clear the list to avoid any previous data.
        list.Clear();
        // And then we add all the players to the list.
        list.AddRange(referenceHubs.Select(Get));
        // We finally return the list.
        return list;
    }
}