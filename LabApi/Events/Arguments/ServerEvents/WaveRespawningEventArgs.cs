using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.WaveRespawning"/> event.
/// </summary>
public class WaveRespawningEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WaveRespawningEventArgs"/> class.
    /// </summary>
    /// <param name="team">The team that is respawning.</param>
    /// <param name="roles">The players that are respawning and roles they will spawn as.</param>
    public WaveRespawningEventArgs(Team team, Dictionary<ReferenceHub, RoleTypeId> roles)
    {
        IsAllowed = true;
        Team = team;
        _roles = roles;
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// Team that is respawning.
    /// </summary>
    public Team Team { get; }

    /// <summary>
    /// Gets all players that are about to respawn.
    /// </summary>
    public IEnumerable<Player> SpawningPlayers => _roles.Keys.Select(n => Player.Get(n));

    private readonly Dictionary<ReferenceHub, RoleTypeId> _roles;

    /// <summary>
    /// Gets whether is this player spawning.
    /// </summary>
    /// <param name="player">Player to check on.</param>
    /// <returns>Whether the player is spawning the next spawn wave.</returns>
    public bool IsSpawning(Player player) => _roles.ContainsKey(player.ReferenceHub);

    /// <summary>
    /// Removes the player from respawn team.
    /// </summary>
    /// <param name="player">The player to remove.</param>
    /// <returns>Whether the player was removed.</returns>
    public bool Remove(Player player) => _roles.Remove(player.ReferenceHub);

    /// <summary>
    /// Changes the role of player. Can also add a player.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="role"></param>
    public void ChangeRole(Player player, RoleTypeId role) => _roles[player.ReferenceHub] = role;

}