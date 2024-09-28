using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DroppingAmmo"/> event.
/// </summary>
public class PlayerDroppingAmmoEventArgs : EventArgs, ICancellableEvent, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDroppingAmmoEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is dropping the ammo.</param>
    /// <param name="type">The type of ammo being dropped.</param>
    /// <param name="amount">The amount of ammo being dropped.</param>
    public PlayerDroppingAmmoEventArgs(ReferenceHub player, ItemType type, int amount)
    {
        Player = Player.Get(player);
        Type = type;
        Amount = amount;
    }

    /// <summary>
    /// Gets or sets the player who is dropping the ammo.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the type of ammo being dropped.
    /// </summary>
    public ItemType Type { get; set; }

    /// <summary>
    /// Gets or sets the amount of ammo being dropped.
    /// </summary>
    public int Amount { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}