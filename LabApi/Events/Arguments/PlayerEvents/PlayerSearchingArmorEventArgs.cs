using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseBodyArmorPickup = InventorySystem.Items.Armor.BodyArmorPickup;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SearchingArmor"/> event.
/// </summary>
public class PlayerSearchingArmorEventArgs : EventArgs, IPlayerEvent, IBodyArmorPickupEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSearchingArmorEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player searching for armor.</param>
    /// <param name="pickup">The armor pickup.</param>
    public PlayerSearchingArmorEventArgs(ReferenceHub player, BaseBodyArmorPickup pickup)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        BodyArmorPickup = BodyArmorPickup.Get(pickup);
    }

    /// <summary>
    /// Gets the player searching for armor.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the armor pickup.
    /// </summary>
    public BodyArmorPickup BodyArmorPickup { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="BodyArmorPickup"/>
    [Obsolete($"Use {nameof(BodyArmorPickup)} instead")]
    public Pickup Pickup => BodyArmorPickup;
}