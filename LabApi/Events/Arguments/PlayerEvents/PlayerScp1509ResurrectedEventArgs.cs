using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using BaseScp1509Item = InventorySystem.Items.Scp1509.Scp1509Item;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the event arguments for when SCP-1509 is resurrecting.
/// </summary>
public class PlayerScp1509ResurrectedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerScp1509ResurrectedEventArgs"/> class.
    /// </summary>
    /// <param name="owner">The SCP-1509 owner instance.</param>
    /// <param name="scp1509Item">The SCP-1509 item.</param>
    /// <param name="killedPlayer">The player who got killed.</param>
    /// <param name="revivePlayer">The player who got revived.</param>
    /// <param name="respawnRole">The role the player spawned as.</param>
    public PlayerScp1509ResurrectedEventArgs(ReferenceHub owner, BaseScp1509Item scp1509Item, ReferenceHub killedPlayer, ReferenceHub revivePlayer, RoleTypeId respawnRole)
    {
        Player = Player.Get(owner);
        Item = Scp1509Item.Get(scp1509Item);
        KilledPlayer = Player.Get(killedPlayer);
        RevivedPlayer = Player.Get(revivePlayer);
        RespawnRole = respawnRole;
    }

    /// <inheritdoc />
    public Player Player { get; }

    /// <summary>
    /// Gets the SCP-1509 item that was used.
    /// </summary>
    public Scp1509Item Item { get; }

    /// <summary>
    /// Gets the player who died by <see cref="Item"/>.
    /// </summary>
    public Player KilledPlayer { get; }

    /// <summary>
    /// Gets the player that got revived.
    /// </summary>
    public Player RevivedPlayer { get; }

    /// <summary>
    /// Gets the role of the revived player.
    /// </summary>
    public RoleTypeId RespawnRole { get; }
}
