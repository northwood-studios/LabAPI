using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using BaseScp1509Item = InventorySystem.Items.Scp1509.Scp1509Item;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the event arguments for when SCP-1509 is resurrecting.
/// </summary>
public class PlayerScp1509ResurrectingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerScp1509ResurrectingEventArgs"/> class.
    /// </summary>
    /// <param name="owner">The SCP-1509 owner instance.</param>
    /// <param name="scp1509Item">The SCP-1509 item.</param>
    /// <param name="killedPlayer">The player who is about to be killed.</param>
    /// <param name="revivePlayer">The player who was selected to be revived.</param>
    /// <param name="respawnRole">The role the player will spawn as.</param>
    public PlayerScp1509ResurrectingEventArgs(ReferenceHub owner, BaseScp1509Item scp1509Item, ReferenceHub killedPlayer, ReferenceHub revivePlayer, RoleTypeId respawnRole)
    {
        Player = Player.Get(owner);
        Item = Scp1509Item.Get(scp1509Item);
        KilledPlayer = Player.Get(killedPlayer);
        RevivedPlayer = Player.Get(revivePlayer);
        RespawnRole = respawnRole;

        IsAllowed = true;
    }

    /// <inheritdoc />
    public Player Player { get; }

    /// <summary>
    /// Gets the SCP-1509 item that is being used.
    /// </summary>
    public Scp1509Item Item { get; }

    /// <summary>
    /// Gets the player who is about to be killed.
    /// </summary>
    public Player KilledPlayer { get; }

    /// <summary>
    /// Gets or sets the player to revive as <see cref="RespawnRole"/>.
    /// </summary>
    public Player? RevivedPlayer { get; set; }

    /// <summary>
    /// Gets or sets the role to respawn the <see cref="RevivedPlayer"/> to.
    /// </summary>
    public RoleTypeId RespawnRole { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
