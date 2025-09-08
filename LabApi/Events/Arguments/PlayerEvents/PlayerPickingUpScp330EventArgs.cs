using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseScp330Pickup = InventorySystem.Items.Usables.Scp330.Scp330Pickup;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PickingUpScp330"/> event.
/// </summary>
public class PlayerPickingUpScp330EventArgs : EventArgs, IPlayerEvent, ICandyPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPickingUpScp330EventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is picking up SCP-330.</param>
    /// <param name="pickup">The SCP-330 pickup.</param>
    public PlayerPickingUpScp330EventArgs(ReferenceHub hub, BaseScp330Pickup pickup)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        CandyPickup = Scp330Pickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player who is picking up SCP-330.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the SCP-330 pickup.
    /// </summary>
    public Scp330Pickup CandyPickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="CandyPickup"/>
    [Obsolete($"Use {nameof(CandyPickup)} instead")]
    public Pickup Pickup => CandyPickup;
}