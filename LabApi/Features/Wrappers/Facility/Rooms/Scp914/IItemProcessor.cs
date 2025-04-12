using Scp914;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// An interface for creating custom SCP-914 Item upgrade processors.
/// </summary>
public interface IItemProcessor
{
    /// <summary>
    /// Called for each players items in the intake chamber of SCP-914 if the <see cref="Scp914.Mode"/> allows so.
    /// </summary>
    /// <param name="setting">The <see cref="Scp914KnobSetting"/> used for this upgrade.</param>
    /// <param name="player">The <see cref="Player"/> that owns this item.</param>
    /// <param name="item">The <see cref="Item"/> to upgraded.</param>
    /// <returns>The upgraded <see cref="Item"/> or null if it was destroyed.</returns>
    /// <remarks>
    /// Use <see cref="Player.RemoveItem(Item)"/> to remove items and <see cref="Player.AddItem(ItemType, InventorySystem.Items.ItemAddReason)"/> to add items.
    /// If adding extra Items over the limit of the inventory consider dropping them as pickups at the output.
    /// </remarks>
    public Item? UpgradeItem(Scp914KnobSetting setting, Player player, Item item);

    /// <summary>
    /// Called for each pickup in the intake chamber if the <see cref="Scp914.Mode"/> allows so.
    /// </summary>
    /// <param name="setting">The <see cref="Scp914KnobSetting"/> used for this upgrade.</param>
    /// <param name="pickup">The <see cref="Pickup"/> to upgrade.</param>
    /// <param name="newPosition">The position to teleport the upgraded pickups to.</param>
    /// <returns>The upgraded <see cref="Pickup"/> or null if it was destroyed.</returns>
    /// <remarks>
    /// Use <see cref="Pickup.Destroy()"/> to remove pickups and <see cref="Pickup.Create(ItemType, Vector3)"/> to add pickups.
    /// You can create more than one pickup during a single upgrade similar to items.
    /// </remarks>
    public Pickup? UpgradePickup(Scp914KnobSetting setting, Pickup pickup, Vector3 newPosition);
}
