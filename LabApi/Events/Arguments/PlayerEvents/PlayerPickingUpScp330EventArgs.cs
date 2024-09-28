using InventorySystem.Items.Usables.Scp330;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickingUpScp330"/> event.
/// </summary>
public class PlayerPickingUpScp330EventArgs : EventArgs, IPlayerEvent, IPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickingUpScp330EventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is picking up SCP-330.</param>
    /// <param name="pickup">The SCP-330 pickup.</param>
    public PlayerPickingUpScp330EventArgs(ReferenceHub player, Scp330Pickup pickup)
    {
        Player = Player.Get(player);
        Pickup = Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who is picking up SCP-330.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the SCP-330 pickup.
    /// </summary>
    public Pickup Pickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}