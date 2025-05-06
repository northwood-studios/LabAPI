using LabApi.Features.Wrappers;
using System;

namespace LabApi.Features.Enums;

/// <summary>
/// Flags used to choose which players to return from <see cref="Player.GetAll(PlayerSearchFlags)"/>.
/// </summary>
[Flags]
public enum PlayerSearchFlags
{
    /// <summary>
    /// No search flags, includes no players.
    /// </summary>
    None = 0,

    /// <summary>
    /// Includes all authenticated real players, see <see cref="Player.IsPlayer"/> and <see cref="Player.IsReady"/>.
    /// </summary>
    /// <remarks>
    /// Same filtering used for <see cref="Player.AuthenticatedList"/>.
    /// </remarks>
    AuthenticatedPlayers = 1,

    /// <summary>
    /// Includes all unauthenticated real players, see <see cref="Player.IsPlayer"/> and <see cref="Player.IsReady"/>.
    /// </summary>
    /// <remarks>
    /// Same filtering used for <see cref="Player.UnauthenticatedList"/>.
    /// </remarks>
    UnthenticatedPlayers = 2,

    /// <summary>
    /// Includes all dummy NPCs, see <see cref="Player.IsNpc"/> and <see cref="Player.IsDummy"/>.
    /// </summary>
    /// <remarks>
    /// Same filtering used for <see cref="Player.DummyList"/>.
    /// </remarks>
    DummyNpcs = 4,

    /// <summary>
    /// Includes all non dummy NPCs, see <see cref="Player.IsNpc"/> and <see cref="Player.IsDummy"/>.
    /// </summary>
    /// <remarks>
    /// Same filtering used for <see cref="Player.RegularNpcList"/>.
    /// </remarks>
    RegularNpcs = 8,

    /// <summary>
    /// Includes the host player, see <see cref="Player.IsHost"/>.
    /// </summary>
    Host = 16,

    /// <summary>
    /// Includes all authenticated real players and all dummy NPCs, see <see cref="AuthenticatedPlayers"/> and <see cref="DummyNpcs"/>.
    /// </summary>
    /// <remarks>
    /// Same filtering used for <see cref="Player.ReadyList"/>.
    /// </remarks>
    AuthenticatedAndDummy = AuthenticatedPlayers | DummyNpcs,
}
